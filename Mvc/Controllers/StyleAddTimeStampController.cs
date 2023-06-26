using SitefinityWebApp.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers
{
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "StyleAddTimeStamp", Title = "Style Add Time Stamp", SectionName = "Custom Widget")]
    public class StyleAddTimeStampController : Controller
    {

        public string StyleFilePath { get; set; }

        public ActionResult Index()
        {

            var model = new StyleAddTimeStampModel();

  

            if (string.IsNullOrEmpty(this.StyleFilePath))
            {
                model.StyleFilePath = "";
            }
            else
            {
                model.StyleFilePath = this.StyleFilePath;
            }

           
            return View("Index", model);
        }
    }
}