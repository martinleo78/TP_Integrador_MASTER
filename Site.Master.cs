using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Integrador_Master
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void logout_Click(object sender, EventArgs e)
        {
            Session.Remove("usuariologgeado");
            boton_logout.Attributes["CssClass"] = "hidden";
            Response.Redirect("Default.aspx",true);
        }
    }
}