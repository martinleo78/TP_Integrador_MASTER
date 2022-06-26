using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Integrador_Master
{
    public partial class Principal : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuariologgeado"] != null)
            {
                string[] usuariolog = (string[])Session["usuariologgeado"];
                label_bienvenida.Text = "Bienvenido/a " + usuariolog[0] +" (" + usuariolog[1]+") - Valido hasta:" + usuariolog[3];
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
}