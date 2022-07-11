using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace TP_Integrador_Master
{
    public partial class _Default : Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["miconexion"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string uid = TextBox1.Text;
                string pass = TextBox2.Text;
                con.Open();
                string qry =
                    "SELECT  U.UserName, P.Description, U.is_deleted, left(newid(),50) as token, "+
                    "dateadd(MINUTE,10,getdate()) as vencimiento, rt.expires as vencimiento_anterior, "+
                    "STRING_AGG(Wi.Description + ', ', '') as ventanas "+
                    "FROM Users U "+
                    "LEFT JOIN UserPrivileges UP ON U.Id = UP.UserID "+
                    "left JOIN Privileges P ON UP.PrivilegeID = P.Id "+
                    "left join RefreshToken rt on U.id = rt.UserId "+
                    "left join PrivilegeWindow pw on p.Id = pw.PrivilegeID "+
                    "left join Windows wi on pw.WindowID = wi.Id "+
                    "where U.UserName = '" + uid + "' " +
                    "and salt = '" + pass + "' " +
                    "and p.is_deleted = 0 " +
                    "and pw.is_deleted = 0 " +
                    "and wi.is_deleted = 0 " +
                    "group by U.UserName, P.Description, U.is_deleted, rt.expires;";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    if (sdr[2].ToString() == "True")
                    {
                        Label4.Text = "El usuario no está activo!";
                    }
                    else
                    {
                        ////////////////Levanto la sesión y cierro datareader.
                        Session["usuariologgeado"] = sdr[0].ToString();
                        Session["privilegio"] = sdr[1].ToString();
                        Session["borrado"] = sdr[2].ToString();
                        Session["token"] = sdr[3].ToString();
                        Session["vencimiento"] = sdr[4].ToString();
                        Session["vencimiento_anterior"] = sdr[5].ToString();
                        Session["ventanas"] = sdr[6].ToString();

                        sdr.Close();
                        sdr = null;

                        ////////////////Guardo Token
                        ///Si no existe, creo
                        if (string.IsNullOrEmpty(Session["vencimiento_anterior"].ToString()))
                        {
                            qry = "insert into refreshtoken(Id, UserId, Token, Expires) " +
                                    "select newid(), id, '" + Session["token"].ToString() + "', '" + Session["vencimiento"].ToString() + "' " +
                                    "from users us " +
                                    "where us.UserName ='" + Session["usuariologgeado"].ToString() + "';";
                            cmd = new SqlCommand(qry, con);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            ///Si existe, actualizo
                            qry = "update RT " +
                                "set token='" + Session["token"].ToString() + "', " +
                                "expires='" + Session["vencimiento"].ToString() + "' " +
                                "from RefreshToken rt " +
                                "inner join users us on rt.UserId = us.Id " +
                                "where us.UserName ='" + Session["usuariologgeado"].ToString() + "';";
                            cmd = new SqlCommand(qry, con);
                            cmd.ExecuteNonQuery();
                        }
                        ////////////////Voy a Principal
                        Response.Redirect("Principal.aspx");
                    }
                }
                else
                {
                    Label4.Text = "Usuario o password incorrecto!";
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}