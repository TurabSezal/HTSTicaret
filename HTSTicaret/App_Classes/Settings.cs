using System;
using System.Configuration;
using System.Drawing;

namespace HTSTicaret.WebUI.App_Classes
{
    public class Settings
    {
        public static Size SliderResimBoyut
        {
            get
            {
                Size sz = new Size();
                sz.Width = Convert.ToInt32(ConfigurationManager.AppSettings["SliderWidth"]);
               
                sz.Height = Convert.ToInt32(ConfigurationManager.AppSettings["SliderHeight"]);
                return sz;
            }
        }
    }
}