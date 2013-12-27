using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Net;
using System.ComponentModel;

namespace Borderlands_Lookup.Classes
{
    class LegendaryDataParser : Control
    {
        private List<string> _legendaryItemList = new List<string>(); // list of legendaries for autocomplete
        private List<BorderlandsItem> _legendaryItems = new List<BorderlandsItem>(); // list of legendaries for quick access
        public event ProgressChangedEventHandler ProgressChanged;

        public static readonly DependencyProperty LegendaryItemListProperty = DependencyProperty.Register("LegendaryItemList",
            typeof(List<string>), typeof(LegendaryDataParser));
        public List<BorderlandsItem> LegendaryItems
        {
            get { return this._legendaryItems; }
        }
        public List<string> LegendaryItemList
        {
            get { return (List<string>)GetValue(LegendaryItemListProperty); }
        }

        public LegendaryDataParser() { }

        public virtual void OnProgressChanged(int progress)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, new ProgressChangedEventArgs(progress, null));
            }
        }

        private string Clean(string s)
        {
            if (s != "")
            {
                if (s.Contains("<span"))
                {
                    int iStart = s.IndexOf(">") +1;
                    int iEnd = s.IndexOf("</span>");
                    s = s.Substring(iStart, iEnd - iStart);
                }
                if (s.Contains("<a"))
                {
                    while (s.Contains("<a"))
                    {
                        int iStart = s.IndexOf("<a");
                        int iEnd = s.IndexOf("</a>", iStart);
                        iStart = s.IndexOf(">", iStart) + 1;
                        s = s.Substring(iStart, iEnd - iStart);
                    }
                }
                if (s.Contains("&amp;"))
                {
                    s = s.Replace("&amp;", "and");
                }
            }
            return s;
        }

        // TODO: new method for parsing each row and then grabbing each cell from the row
        // This way I can avoid having to do specific checks 

        public void ParseLegendaryItems()
        {
            // grabs legendary items and populates both lists
            // two separate loops: one for populating the string list (initial loop) and one for populating the list of BorderlandsItems
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.borderlands.wikia.com/wiki/Legendary");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (System.IO.StreamReader sResponse = new System.IO.StreamReader(response.GetResponseStream()))
            {
                string sData = sResponse.ReadToEnd();
                string _start = "list_of_legendary_items_.28borderlands_2.29";
                int iStart = sData.ToLower().IndexOf(_start), iLoopStart;
                int iEnd = 0, iLoopEnd = 0;
                string _manufacturer, _type, _model, _boss, _location;
                iStart = sData.ToLower().IndexOf(_start, iStart + 1);
                iStart = sData.ToLower().IndexOf("<tr>", iStart + 1);
                iStart = sData.ToLower().IndexOf("<tr>", iStart) + 4;
                iEnd = iLoopStart = iStart;
                iLoopEnd = sData.ToLower().IndexOf("</table>", iStart);
                while (iStart <= iLoopEnd && iStart >= iLoopStart)
                {
                    // get the list of legendary items, then grab whatever data i can from the page
                    iStart = sData.ToLower().IndexOf("<td>", iEnd) + 4;
                    iEnd = sData.ToLower().IndexOf("</td>", iStart);
                    _manufacturer = Clean(sData.Substring(iStart, iEnd - iStart));

                    iStart = sData.ToLower().IndexOf("<td>", iEnd) + 4;
                    iEnd = sData.ToLower().IndexOf("</td>", iStart);
                    _type = Clean(sData.Substring(iStart, iEnd - iStart));

                    iStart = sData.ToLower().IndexOf("<td>", iEnd) + 4;
                    iEnd = sData.ToLower().IndexOf("</td>", iStart);
                    _model = Clean(sData.Substring(iStart, iEnd - iStart));

                    iStart = sData.ToLower().IndexOf("<td>", iEnd) + 4;
                    iEnd = sData.ToLower().IndexOf("</td>", iStart);
                    _boss = Clean(sData.Substring(iStart, iEnd - iStart));

                    iStart = sData.ToLower().IndexOf("<td>", iEnd) + 4;
                    iEnd = sData.ToLower().IndexOf("</td>", iStart);
                    _location = Clean(sData.Substring(iStart, iEnd - iStart));

                    iStart = sData.ToLower().IndexOf("<tr>", iStart); // set the next starting point to avoid grabbing extra data at the end

                    BorderlandsItem legendary = new BorderlandsItem(_manufacturer, _type, _model, "", _boss,  _location, "");
                    this._legendaryItemList.Add(_model);
                    this._legendaryItems.Add(legendary);
                }
                SetValue(LegendaryItemListProperty, this._legendaryItemList);
            }
        }

        // TODO: report progress from background worker - this method (and previous method) will need to be loaded in the backgeound
        // and a progress bar will need to be displayed to indicated when it is finished. I anticipate this method will take a large amount of
        // time.

        public void GetAllLegendaryData()
        {
            if (this._legendaryItems.Count > 0)
            {
                foreach (BorderlandsItem item in this._legendaryItems)
                {
                    if (item.WeaponType.ToLower() == "class mod")
                    {
                        item.ElementalType = "None";
                        if (item.WeaponModel.ToLower().Contains("slayer") == true)
                        {
                            item.SpecialEffects = "None";
                        }
                    }
                    if (item.WeaponModel.ToLower().Contains("slayer of"))
                    {
                        item.ElementalType = "None";
                        item.SpecialEffects = "None";
                    }
                    else
                    {
                        // this method gets the elemental type and the special weapon effects for each legendary weapon
                        // note: if a weapon does not have an elemental tpye, the Wiki indicates this by "None", so there is no need to handle this case
                        string preppedModel = PrepForURL(item.WeaponModel); // replaces the spaces with underscores for use in the URL
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.borderlands.wikia.com/wiki/" + preppedModel);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        using (System.IO.StreamReader sReader = new System.IO.StreamReader(response.GetResponseStream()))
                        {
                            string sData = sReader.ReadToEnd();
                            int start = sData.ToLower().IndexOf("element:");
                            int end = start;
                            HtmlCleaner cleaner = new HtmlCleaner();
                            string elementalType = "";
                            bool _getElementalType = true;
                            if (item.WeaponType.ToLower() == "class mod")
                            {
                                if (item.WeaponModel.ToLower().Contains("legendary"))
                                {
                                    item.ElementalType = "None";
                                    _getElementalType = false;
                                }
                            }
                            if (sData.ToLower().Contains("disambiguation"))
                            {
                                // if a disambguation is found, then we need to redirect to the appropriate page
                                string data = "";
                                if (preppedModel.ToLower() == "longbow")
                                {
                                    request = (HttpWebRequest)WebRequest.Create("http://www.borderlands.wikia.com/wiki/" + preppedModel +
                                        "_(sniper_rifle)");
                                    response = (HttpWebResponse)request.GetResponse();
                                    using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream()))
                                    {
                                        data = reader.ReadToEnd();
                                        item.ElementalType = ParseElementalType(data);
                                        item.SpecialEffects = ParseSpecialEffect(data);
                                    }
                                }
                                else
                                {
                                    request = (HttpWebRequest)WebRequest.Create("http://www.borderlands.wikia.com/wiki/" + preppedModel +
                                        "_(Borderlands_2)");
                                    response = (HttpWebResponse)request.GetResponse();
                                    using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream()))
                                    {
                                        data = reader.ReadToEnd();
                                        item.ElementalType = ParseElementalType(data);
                                        item.SpecialEffects = ParseSpecialEffect(data);
                                    }
                                }
                            }
                            else
                            {
                                if (_getElementalType)
                                    item.ElementalType = ParseElementalType(sData);
                                item.SpecialEffects = ParseSpecialEffect(sData);
                            }
                        }
                    }
                }
            }
        }

        private string ParseElementalType(string data)
        {
            string elementalType = "";
            int start = 0, end = 0;
            HtmlCleaner cleaner = new HtmlCleaner();
            start = data.ToLower().IndexOf("<td", start);
            end = data.ToLower().IndexOf("</td>", start);
            elementalType = cleaner.CleanAllTags(elementalType);
            return elementalType;
        }

        private string ParseSpecialEffect(string data)
        {
            if (data.ToLower().Contains("id=\"special"))
            {
                string specialEffect = "";
                int start = 0, end = 0;
                HtmlCleaner cleaner = new HtmlCleaner();
                start = data.ToLower().IndexOf("id=\"special");
                start = data.ToLower().IndexOf("</span>", start);
                start = data.ToLower().IndexOf(" ", start) + 1;
                end = data.ToLower().IndexOf("</p>", start);
                specialEffect = data.Substring(start, end - start);
                return specialEffect;
            }
            return "None";
        }
        // no spaces allowed in URL, so replace it with an underscore
        private string PrepForURL(string s)
        {
            if (s.ToLower().Contains(" "))
                s = s.Replace(" ", "_");
            return s;
        }

    }
}
