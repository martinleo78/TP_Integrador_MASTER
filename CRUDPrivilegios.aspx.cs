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
    public partial class CRUDPrivilegios : Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["miconexion"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuariologgeado"] == null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void buscarPrivilegio_click(object sender, EventArgs e)
        {
            try
            {
                string privilegio = txtb_buscarPrivilegio.Text;

                string qry = "SELECT  P.Description " +
                            "FROM  Privileges P " +
                            "where P.Description like ('%" + privilegio + "%') ";

                SqlCommand sqlCmd = new SqlCommand(qry, con);
                con.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                StringBuilder sb = new StringBuilder();
                sb.Append("<table class=\"table  table-striped table-bordered text-center mt-4\">");
                sb.Append("<thead >");
                sb.Append("<tr class=\"table-dark\">");
                sb.Append("<th scope=\"col\">Nombre de Permiso</th>");
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

                tablaPrivilegios.Controls.Add(new Label { Text = sb.ToString() });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void crearPrivilegio_click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                string privilegio = txtb_nuevoPrivilegio.Text;

                string qry = "BEGIN " +
                                "IF NOT EXISTS (SELECT * FROM [tp_integrador].[dbo].[Privileges] P " +
                                                  "WHERE P.[Description] = '" + privilegio + "')" +
                                    " BEGIN " +
                                        "INSERT INTO [tp_integrador].[dbo].[Privileges] ([Id], [Description])" +
                                        " VALUES(NEWID(), '" + privilegio + "')" +
                                    " END " +
                              "END;";

                SqlCommand sqlCmd = new SqlCommand(qry, con);

                //sqlCmd = new SqlCommand(qry, con);
                sqlCmd.ExecuteNonQuery();

                Console.WriteLine("Perfil agregado correctamente");

                label_resultado.Text = "Perfil '" + privilegio + "' agregado correctamente";
                txtb_nuevoPrivilegio.Text = "";

                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}