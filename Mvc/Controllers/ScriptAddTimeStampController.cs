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
    [ControllerToolboxItem(Name = "ScriptAddTimeStamp", Title = "Script Add Time Stamp", SectionName = "Custom Widget")]
    public class ScriptAddTimeStampController : Controller
    {

        public string ScriptFilePath { get; set; }

        public ActionResult Index()
        {

            var model = new ScriptAddTimeStampModel();



            if (string.IsNullOrEmpty(this.ScriptFilePath))
            {
                model.ScriptFilePath = "";
            }
            else
            {
                model.ScriptFilePath = this.ScriptFilePath;
            }


            return View("Index", model);
        }
    }
}