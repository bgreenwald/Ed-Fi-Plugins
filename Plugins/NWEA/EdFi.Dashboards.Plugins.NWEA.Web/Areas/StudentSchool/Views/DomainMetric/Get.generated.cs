﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EdFi.Dashboards.Plugins.NWEA.Web.Areas.StudentSchool.Views.DomainMetric
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 1 "..\..\Areas\StudentSchool\Views\DomainMetric\Get.cshtml"
    using EdFi.Dashboards.Common;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Areas\StudentSchool\Views\DomainMetric\Get.cshtml"
    using EdFi.Dashboards.Metric.Rendering;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Areas\StudentSchool\Views\DomainMetric\Get.cshtml"
    using EdFi.Dashboards.Presentation.Core.Areas.StudentSchool.Controllers;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Areas\StudentSchool\Views\DomainMetric\Get.cshtml"
    using EdFi.Dashboards.Presentation.Web.Architecture;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/StudentSchool/Views/DomainMetric/Get.cshtml")]
    public partial class Get : System.Web.Mvc.WebViewPage<dynamic>
    {
        public Get()
        {
        }
        public override void Execute()
        {





WriteLiteral("\r\n");


DefineSection("ContentPlaceHolderHead", () => {

WriteLiteral("\r\n    <title>");


            
            #line 8 "..\..\Areas\StudentSchool\Views\DomainMetric\Get.cshtml"
       Write(Html.Action("Title", typeof(StudentSchoolLayoutController).GetControllerName(), new { subTitle = Model.DisplayName }));

            
            #line default
            #line hidden
WriteLiteral("</title>\r\n    <script type=\"text/javascript\">\r\n        var pageArea = { Area: \'st" +
"udent\', Page: \'Academic Dashboard - ");


            
            #line 10 "..\..\Areas\StudentSchool\Views\DomainMetric\Get.cshtml"
                                                                 Write(Model.DisplayName);

            
            #line default
            #line hidden
WriteLiteral(@"' };
        
        $(function () {
            $('a[id|=""mainmoreActions""]').on(""click keypress focus"", openMoreMenu);

            var tdId = requestQuerystring(""tdId"");
            if (tdId != null) {
                $(""[data-tdid='"" + tdId + ""']"").click();
            }

            var jump = requestQuerystring(""jump"");
            if (jump != null && jump != '') {
                window.location.hash = ""#"" + jump;
            }
            
            highlightMetric();
        });
    </script>

");


});

WriteLiteral("\r\n\r\n");


DefineSection("ContentPlaceHolder1", () => {

WriteLiteral("\r\n\r\n    <div class=\"l-metrics\">\r\n");


            
            #line 34 "..\..\Areas\StudentSchool\Views\DomainMetric\Get.cshtml"
           MetricRenderingExtensions.RenderMetrics(Html, Model, (RenderingMode) ViewBag.RenderingMode); 

            
            #line default
            #line hidden
WriteLiteral("    </div>\r\n\r\n");


});


        }
    }
}
#pragma warning restore 1591
