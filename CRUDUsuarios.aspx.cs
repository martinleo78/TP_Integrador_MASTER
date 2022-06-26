using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using System.Data;
using System.Text;

namespace TP_Integrador_Master
{
    public partial class CRUDUsuarios : Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["miconexion"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuariologgeado"] != null)
            {
                string usuariologgeado = Session["usuariologgeado"].ToString();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void buscar_click(object sender, EventArgs e)
        {
            try
            {
                string usuario = txt_buscar_usuario.Text;
                string privilegio = txt_buscar_privilegio.Text;

                if (String.IsNullOrEmpty(usuario))
                {
                    usuario = "";
                }
                if (String.IsNullOrEmpty(privilegio))
                {
                    privilegio = "";
                }


                string qry = "SELECT  U.UserName, P.Description " +
                    "FROM  Users U LEFT JOIN UserPrivileges ON U.Id = UserPrivileges.UserID INNER JOIN " +
                    "Privileges P ON UserPrivileges.PrivilegeID = P.Id " +
                    "where U.UserName like '%" + usuario + "%' and " +
                    "P.Description like '%" + privilegio + "%' and " +
                    "U.is_deleted=0;";

                SqlCommand sqlCmd = new SqlCommand(qry, con);
                con.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                StringBuilder sb = new StringBuilder();
                sb.Append("<table class=\"table  table-striped table-bordered text-center mt-4\">");
                sb.Append("<thead >");
                sb.Append("<tr class=\"table-dark\">");
                sb.Append("<th scope=\"col\">Usuario</th>");
                sb.Append("<th scope=\"col\">Permiso</th>");
                sb.Append("</tr>");
                sb.Append("</thead>");
                sb.Append("<tbody>");

                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("<tr>");
                    foreach (DataColumn dc in dt.Columns)
                    {
                        sb.Append("<td>");
                        sb.Append(dr[dc.ColumnName].ToString());
                        sb.Append("</td>");
                    }

                    sb.Append("</tr>");
                }
                sb.Append("</tbody>");
                sb.Append("</table>");
                con.Close();

                tablaUsuarios.Controls.Add(new Label { Text = sb.ToString() });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}