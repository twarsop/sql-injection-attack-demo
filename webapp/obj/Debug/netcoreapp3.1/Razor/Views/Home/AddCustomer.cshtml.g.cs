#pragma checksum "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\Home\AddCustomer.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "780d69cda153df77e62b95aceed37a3f8fd2f019"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_AddCustomer), @"mvc.1.0.view", @"/Views/Home/AddCustomer.cshtml")]
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
#line 1 "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\_ViewImports.cshtml"
using webapp;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"780d69cda153df77e62b95aceed37a3f8fd2f019", @"/Views/Home/AddCustomer.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7d14b038c307eedfddd9296a7b65b42fdd3fb342", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_AddCustomer : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<webapp.Models.AddCustomerViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\Home\AddCustomer.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<h1 class=\"display-4\">Add Customer</h1>\r\n\r\n");
#nullable restore
#line 9 "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\Home\AddCustomer.cshtml"
 using (Html.BeginForm("SaveCustomer", "Home", FormMethod.Post))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>Title: </div>\r\n");
#nullable restore
#line 12 "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\Home\AddCustomer.cshtml"
Write(Html.DropDownListFor(m => m.CustomerTitleId, new SelectList(Model.Titles, "Id", "Name")));

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>First Name: </div>\r\n");
#nullable restore
#line 15 "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\Home\AddCustomer.cshtml"
Write(Html.TextBoxFor(m => m.Customer.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>Last Name: </div>\r\n");
#nullable restore
#line 18 "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\Home\AddCustomer.cshtml"
Write(Html.TextBoxFor(m => m.Customer.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>Address Line 1: </div>\r\n");
#nullable restore
#line 21 "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\Home\AddCustomer.cshtml"
Write(Html.TextBoxFor(m => m.Customer.AddressLine1));

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>Address Postcode: </div>\r\n");
#nullable restore
#line 24 "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\Home\AddCustomer.cshtml"
Write(Html.TextBoxFor(m => m.Customer.AddressPostcode));

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div style=\"clear:both\"></div>\r\n");
            WriteLiteral("    <input type=\"submit\" value=\"Save Customer\" />\r\n");
#nullable restore
#line 29 "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\Home\AddCustomer.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<input type=\"button\" value=\"Cancel\"");
            BeginWriteAttribute("onclick", " onclick=\"", 791, "\"", 845, 3);
            WriteAttributeValue("", 801, "location.href=\'", 801, 15, true);
#nullable restore
#line 31 "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\Home\AddCustomer.cshtml"
WriteAttributeValue("", 816, Url.Action("Index", "Home"), 816, 28, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 844, "\'", 844, 1, true);
            EndWriteAttribute();
            WriteLiteral(" />");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<webapp.Models.AddCustomerViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
