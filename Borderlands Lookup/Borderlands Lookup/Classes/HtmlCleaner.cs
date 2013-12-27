using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Lookup.Classes
{
    class HtmlCleaner
    {
        // this class will essentially get rid of any html tags that may be contained in a passed string
        // TODO: add option to parse out attributes for applying attributes to the string
        // TODO: create structure for recognizing tags for decorating text (such as bold, italic, etc...) 
        // TODO: add support for specifying number of tags to be removed using a two dimensional array
        public HtmlCleaner() { }

        public string CleanAllTags(string s)
        {
            while (s.Contains("<"))
            {
                int start = s.IndexOf("<");
                int end = 0;
                start = s.IndexOf(">", start);
                end = s.IndexOf("<", start);
                s = s.Substring(start, end - start);
            }
            return s;
        }

        public string CleanTags(string s, string[] tags)
        {
            // removes a set of given tags from a string
            return "";
        }

        public string CleanTag(string s, string tag)
        {
            // cleans a specific tag from a string
            return "";
        }
    }
}
