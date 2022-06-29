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
                label_bienvenida.Text = "Bienvenido/a " + Session["usuariologgeado"].ToString() + " (" + Session["privilegio"].ToString() + ") - Valido hasta:" + Session["vencimiento"].ToString(); 
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
}