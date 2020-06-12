using System;

namespace PandemicWeb.Models
{

    public static class Extensoes
    {
        public static string ToShortForm(TimeSpan t)
        {
            string shortForm = "";

            if(t.TotalMinutes % 60 == 0)
                shortForm = string.Format("{0}h", t.Hours.ToString());
            else
            {
                if (t.Hours > 0)
                {
                    shortForm += string.Format("{0}h", t.Hours.ToString());
                }
                if (t.Minutes > 0)
                {
                    shortForm += string.Format("{0}m", t.Minutes.ToString());
                }
            }           

            return shortForm;
        } 
    }

}