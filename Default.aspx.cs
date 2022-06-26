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
                string qry = "SELECT  U.UserName, P.Description, U.is_deleted, left(newid(),50) as token, " +
                "dateadd(MINUTE,10,getdate()) as vencimiento, rt.expires as vencimiento_anterior " +
                "FROM Users U LEFT JOIN UserPrivileges ON U.Id = UserPrivileges.UserID " +
                "INNER JOIN Privileges P ON UserPrivileges.PrivilegeID = P.Id " +
                "left join RefreshToken rt on U.id = rt.UserId "+
                "where U.UserName = '" + uid + "' " +
                "and salt = '" + pass + "';";

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
                        string[] usuariolog = new string[] { sdr[0].ToString(), sdr[1].ToString(), sdr[3].ToString(), sdr[4].ToString(), sdr[5].ToString() };
                        Session["usuariologgeado"] = usuariolog;
                        sdr.Close();
                        sdr = null;

                        ////////////////Guardo Token
                        ///Si no existe, creo
                        if (string.IsNullOrEmpty(usuariolog[4]))
                        {
                            qry = "insert into refreshtoken(Id, UserId, Token, Expires) " +
                                    "select newid(), id, '" + usuariolog[2] + "', '" + usuariolog[3] + "' " +
                                    "from users us " +
                                    "where us.UserName ='" + usuariolog[0] + "';";
                            cmd = new SqlCommand(qry, con);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            ///Si existe, actualizo
                            qry = "update RT " +
                                "set token='" + usuariolog[2] + "', " +
                                "expires='" + usuariolog[3] + "' " +
                                "from RefreshToken rt " +
                                "inner join users us on rt.UserId = us.Id " +
                                "where us.UserName ='" + usuariolog[0] + "';";
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