using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace TP_Integrador_Master
{
    public partial class MantenimientoUsuarios : Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["miconexion"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            /////////////////VERIFICO SI USUARIO LOGGEADO O NO
            if (Session["usuariologgeado"] != null)
            {
                string usuariologgeado = Session["usuariologgeado"].ToString();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
            if (!Page.IsPostBack)
            {
                /////////////////CARGO DROPDOWNS DE PRIVILEGIOS
                con.Open();
                SqlCommand com = new SqlCommand("select *from Privileges", con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);
                lstpermisoscrear.DataTextField = ds.Tables[0].Columns["Description"].ToString();
                lstpermisoscrear.DataValueField = ds.Tables[0].Columns["Id"].ToString();
                lstpermisoscrear.DataSource = ds.Tables[0];
                lstpermisoscrear.DataBind();
                lstpermisosactualizar.DataTextField = ds.Tables[0].Columns["Description"].ToString();
                lstpermisosactualizar.DataValueField = ds.Tables[0].Columns["Id"].ToString();
                lstpermisosactualizar.DataSource = ds.Tables[0];
                lstpermisosactualizar.DataBind();
                con.Close();
            }
            if (!IsPostBack)
            {
                crearUsuario.Style["Display"] = "none";
                mantenimientoUsuario.Style["Display"] = "block";
            }

        }

        protected void buscar_click(object sender, EventArgs e)
        {
            limpiarError();
            try
            {
                ///////////////////////BUSCAR USUARIO
                string usuario = txt_buscar_usuario.Text;
                string qry = "SELECT  U.UserName, P.Description " +
                    "FROM  Users U LEFT JOIN UserPrivileges UP ON U.Id = UP.UserID LEFT JOIN " +
                    "Privileges P ON UP.PrivilegeID = P.Id " +
                    "where U.UserName = '" + usuario + "' and " +
                    "U.is_deleted=0;";

                SqlCommand sqlCmd = new SqlCommand(qry, con);
                con.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                limpiarError();
                limpiarTextos();
                if (dt.Rows.Count == 0)
                {
                    label_error_usuario.Text = "Usuario no existe";
                }
                else
                {
                    txt_usuario.Text = dt.Rows[0][0].ToString();
                    lstpermisosactualizar.ClearSelection();
                    lstpermisosactualizar.Items.FindByText(dt.Rows[0][1].ToString()).Selected = true;
                }
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void nuevo_Click(object sender, EventArgs e)
        {
            limpiarError();
            bool error = false;

            if (string.IsNullOrEmpty(txt_alta_usuario.Text))
            {
                label_error_nombre.Text = "Error! Nombre de usuario no valido";

                error = true;
            }
            if (txt_alta_password.Text != txt_alta_password_confirm.Text || string.IsNullOrEmpty(txt_alta_password.Text))
            {
                label_error_pass.Text = "Error! Verifique que las contraseña sean correctas";

                error = true;
            }
            if (!error)
            {
                try
                {
                    con.Open();
                    ///////////////////////////Inserta Usuario
                    string qry =
                        "IF NOT EXISTS (SELECT 1 FROM USERS WHERE USERNAME='" + txt_alta_usuario.Text + "') " +
                        "BEGIN " +
                        "INSERT INTO USERS(Id, UserName, Salt, PasswordHash, is_deleted) " +
                        "VALUES(NEWID(), '" + txt_alta_usuario.Text + "', '" + txt_alta_password.Text + "', " +
                        "HASHBYTES('MD5', '" + txt_alta_password.Text + "'), 0); " +
                        "END ";
                    SqlCommand sqlCmd = new SqlCommand(qry, con);
                    Int32 result = (Int32)sqlCmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        ///////////////////////////Inserta USUARIOPERMISO
                        ///
                        qry =
                            "IF EXISTS(SELECT 1 FROM USERS WHERE USERNAME = '" + txt_alta_usuario.Text + "') " +
                            "BEGIN " +
                            "DECLARE @USUARIO UNIQUEIDENTIFIER = (SELECT ID FROM Users WHERE USERNAME = '" + txt_alta_usuario.Text + "'); " +
                            "DECLARE @PERMISO UNIQUEIDENTIFIER = '" + lstpermisoscrear.SelectedValue.ToString() + "'; " +
                            "INSERT INTO UserPrivileges(Id, UserID, PrivilegeID) " +
                            "VALUES(NEWID(), @USUARIO, @PERMISO); " +
                            "END ";
                        sqlCmd = new SqlCommand(qry, con);
                        result = (Int32)sqlCmd.ExecuteNonQuery();

                        limpiarError();
                        limpiarTextos();
                        label_error_nombre.Text = "Usuario creado!";

                    }
                    else
                    {
                        limpiarError();
                        limpiarTextos();
                        label_error_nombre.Text = "Usuario ya existe";
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        protected void buttonNew_Click(object sender, EventArgs e)
        {
            limpiarError();
            limpiarTextos();
            crearUsuario.Style["Display"] = "Block";
            mantenimientoUsuario.Style["Display"] = "none";
        }

        protected void buttonEdit_Click(object sender, EventArgs e)
        {
            limpiarError();
            limpiarTextos();
            crearUsuario.Style["Display"] = "none";
            mantenimientoUsuario.Style["Display"] = "block";
        }
        protected void editar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_usuario.Text))
            {
                label_error_usuario.Text = "No hay un usuario seleccionado";
            }
            else
            {
                try
                {
                    con.Open();
                    ///////////////////////////Actualizar roles Usuario
                    string qry =
                        "DECLARE @USUARIO UNIQUEIDENTIFIER = (SELECT ID FROM Users WHERE USERNAME = '" + txt_usuario.Text + "'); " +
                        "DECLARE @PERMISO UNIQUEIDENTIFIER = '" + lstpermisosactualizar.SelectedValue.ToString() + "'; " +
                        "update UserPrivileges " +
                        "set PrivilegeID=@PERMISO " +
                        "where UserID=@USUARIO;";
                    SqlCommand sqlCmd = new SqlCommand(qry, con);
                    Int32 result = (Int32)sqlCmd.ExecuteNonQuery();
                    limpiarTextos();
                    limpiarError();
                    if (result != 1)
                    {

                        label_error_usuario.Text = "Error durante la actualización";
                    }
                    else
                    {
                        label_error_usuario.Text = "Usuario Actualizado!";
                    }
                    con.Close();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        protected void borrar_Click(object sender, EventArgs e)
        {
            limpiarError();
            limpiarTextos();         
            //////////////////////Valida Usuario Existe
            if (string.IsNullOrEmpty(txt_usuario.Text))
            {
                label_error_usuario.Text = "No hay un usuario seleccionado";
            }
            else
            {
                try
                {
                    con.Open();
                    ///////////////////////////Soft delete Usuario
                    string qry =
                        "update users " +
                        "set is_deleted = 1 " +
                        "where username = '" + txt_usuario.Text + "';";
                    SqlCommand sqlCmd = new SqlCommand(qry, con);
                    Int32 result = (Int32)sqlCmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        label_error_usuario.Text = "Usuario borrado!";
                    }
                    else
                    {
                        label_error_usuario.Text = "Error de borrado";
                    }
                    con.Close();
                    limpiarTextos();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        protected void limpiarError()
        {
            label_error_nombre.Text = "";
            label_error_pass.Text = "";
            label_error_usuario.Text = "";
        }

        protected void limpiarTextos()
        {
            txt_alta_usuario.Text = "";
            txt_buscar_usuario.Text = "";
            txt_alta_password.Text = "";
            txt_alta_password_confirm.Text = "";
            txt_usuario.Text = "";
        }
    }
}