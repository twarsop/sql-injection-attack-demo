#pragma checksum "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\Home\EditCustomer.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "39cc2bdea0a7d5809fda2c77b79d87e543c9328e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_EditCustomer), @"mvc.1.0.view", @"/Views/Home/EditCustomer.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"39cc2bdea0a7d5809fda2c77b79d87e543c9328e", @"/Views/Home/EditCustomer.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7d14b038c307eedfddd9296a7b65b42fdd3fb342", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_EditCustomer : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<webapp.Models.EditCustomerViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\Home\EditCustomer.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<h1 class=\"display-4\">Add Customer</h1>\r\n\r\n");
#nullable restore
#line 9 "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\Home\EditCustomer.cshtml"
 using (Html.BeginForm("SaveEditCustomer", "Home", FormMethod.Post))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>Id: </div>\r\n");
#nullable restore
#line 12 "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\Home\EditCustomer.cshtml"
Write(Html.TextBoxFor(m => m.Customer.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>Title: </div>\r\n");
#nullable restore
#line 15 "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\Home\EditCustomer.cshtml"
Write(Html.DropDownListFor(m => m.CustomerTitleId, new SelectList(Model.Titles, "Id", "Name")));

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>First Name: </div>\r\n");
#nullable restore
#line 18 "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\Home\EditCustomer.cshtml"
Write(Html.TextBoxFor(m => m.Customer.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>Last Name: </div>\r\n");
#nullable restore
#line 21 "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\Home\EditCustomer.cshtml"
Write(Html.TextBoxFor(m => m.Customer.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>Address Line 1: </div>\r\n");
#nullable restore
#line 24 "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\Home\EditCustomer.cshtml"
Write(Html.TextBoxFor(m => m.Customer.AddressLine1));

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>Address Postcode: </div>\r\n");
#nullable restore
#line 27 "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\Home\EditCustomer.cshtml"
Write(Html.TextBoxFor(m => m.Customer.AddressPostcode));

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div style=\"clear:both\"></div>\r\n");
            WriteLiteral("    <input type=\"submit\" value=\"Save Customer\" />\r\n");
#nullable restore
#line 32 "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\Home\EditCustomer.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<input type=\"button\" value=\"Cancel\"");
            BeginWriteAttribute("onclick", " onclick=\"", 861, "\"", 915, 3);
            WriteAttributeValue("", 871, "location.href=\'", 871, 15, true);
#nullable restore
#line 34 "C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\webapp\Views\Home\EditCustomer.cshtml"
WriteAttributeValue("", 886, Url.Action("Index", "Home"), 886, 28, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 914, "\'", 914, 1, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<webapp.Models.EditCustomerViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
