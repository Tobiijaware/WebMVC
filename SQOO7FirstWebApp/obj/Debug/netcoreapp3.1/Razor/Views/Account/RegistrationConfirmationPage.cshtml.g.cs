#pragma checksum "C:\Users\cidos\Source\Mentorship and Training\SQ007\Week 8\SQOO7FirstWebApp\SQOO7FirstWebApp\Views\Account\RegistrationConfirmationPage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d28369c5b72efb81a87264e09ab93ef01b45ce22"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_RegistrationConfirmationPage), @"mvc.1.0.view", @"/Views/Account/RegistrationConfirmationPage.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\cidos\Source\Mentorship and Training\SQ007\Week 8\SQOO7FirstWebApp\SQOO7FirstWebApp\Views\_ViewImports.cshtml"
using SQOO7FirstWebApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\cidos\Source\Mentorship and Training\SQ007\Week 8\SQOO7FirstWebApp\SQOO7FirstWebApp\Views\_ViewImports.cshtml"
using SQOO7FirstWebApp.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\cidos\Source\Mentorship and Training\SQ007\Week 8\SQOO7FirstWebApp\SQOO7FirstWebApp\Views\_ViewImports.cshtml"
using SQOO7FirstWebApp.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d28369c5b72efb81a87264e09ab93ef01b45ce22", @"/Views/Account/RegistrationConfirmationPage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19e7d7f99751bf37bc65d43300bd399c79fa42d4", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_RegistrationConfirmationPage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\cidos\Source\Mentorship and Training\SQ007\Week 8\SQOO7FirstWebApp\SQOO7FirstWebApp\Views\Account\RegistrationConfirmationPage.cshtml"
  
    ViewBag.Title = "Registration confirmation page";
    Layout = "_Layout.cshtml";

    var link = "";
    var username = "";
    if(!string.IsNullOrWhiteSpace(ViewBag.Link))
    {
        link =  ViewBag.Link;
    }
    
    if(!string.IsNullOrWhiteSpace(ViewBag.Username))
    {
        username =  ViewBag.Username;
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"d-flex justify-content-center align-items-center\" style=\"height:70vh\">\r\n    <div class=\"border col-4 rounded p-4\" style=\"min-height:300px\">\r\n        <h1 class=\"mb-3\">Congratulations ");
#nullable restore
#line 20 "C:\Users\cidos\Source\Mentorship and Training\SQ007\Week 8\SQOO7FirstWebApp\SQOO7FirstWebApp\Views\Account\RegistrationConfirmationPage.cshtml"
                                    Write(username);

#line default
#line hidden
#nullable disable
            WriteLiteral("!</h1>\r\n        <p>\r\n            Your account has been successfully created, but you have to activate\r\n            it before it can be used.\r\n            <p>\r\n                click to <a");
            BeginWriteAttribute("href", " href=\"", 739, "\"", 751, 1);
#nullable restore
#line 25 "C:\Users\cidos\Source\Mentorship and Training\SQ007\Week 8\SQOO7FirstWebApp\SQOO7FirstWebApp\Views\Account\RegistrationConfirmationPage.cshtml"
WriteAttributeValue("", 746, link, 746, 5, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">activate</a>\r\n            </p>\r\n        </p>\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591