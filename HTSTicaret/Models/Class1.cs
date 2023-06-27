using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Runtime.CompilerServices;

namespace HTSTicaret.WebUI.Models
{
    public class Class1
    {
        public class WebServiceProxy
        {
            private string _webServiceUrl;


            public WebServiceProxy()
            {
                _webServiceUrl = ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString;

            }

            private string _property;

            public string GetData(string v)
            {
                return _property;
            }
        }
    }
    }