using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Borderlands_Lookup.Classes;
using System.Net;

namespace Borderlands_Lookup.Classes
{
    class ItemDataParser : Control
    {
        #region Properties
        public string Data { get; set; }
        #endregion
        #region Methods

        public ItemDataParser() { }

        private bool CheckDisambiguation(string testData)
        {
            if (testData.ToLower().Contains("disambiguation"))
                return true;
            else
                return false;
        }

        public string GetItemData(string ItemName)
        {
            if (ItemName != null)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.borderlands.wikia.com/wiki/" + ItemName);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string sResponseData = "";
                using (System.IO.StreamReader srResponse = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    sResponseData = srResponse.ReadToEnd();
                    this.Data = sResponseData;
                }
                if (sResponseData != "")
                {
                    this.GetManufacturer(sResponseData);
                    //this.GetWeaponType(sResponseData);
                    //this.GetWeaponModel(sResponseData);
                    //this.GetElementalType(sResponseData);
                    //this.GetLocation(sResponseData);
                    //this.GetSpecialEffects(sResponseData);
                }
            }
            else
                return null;
            return "Item not found";
        }

        private string GetManufacturer(string ResponseData)
        {
            // grab data from web and parse it out
            // will set the fields at the end of each of the corresponding methods
            int iStart = ResponseData.ToLower().IndexOf("manufactured by") + 15;
            int iEnd = ResponseData.ToLower().IndexOf(".", iStart);
            string s_Manufacturer = ResponseData.Substring(iStart, iEnd - iStart);
            if (s_Manufacturer != "")
                return s_Manufacturer;
            else
                return "Not found";
        }

        //private string GetWeaponType(string ResponseData)
        //{

        //}

        //private string GetWeaponModel(string ResponseData)
        //{

        //}

        //private string GetElementalType(string ResponseData)
        //{

        //}

        //private string GetWeaponVariants(string ResponseData)
        //{

        //}

        //private string GetLocation(string ResponseData)
        //{

        //}

        //private string GetSpecialEffects(string ResponseData)
        //{

        //}
        #endregion
    }
}
