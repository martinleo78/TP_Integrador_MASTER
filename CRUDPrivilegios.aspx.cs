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
    public partial class CRUDPrivilegios : Page
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
            if (!IsPostBack)
            {
                crearPrivilegio.Style["Display"] = "none";
                mantenimientoPrivilegio.Style["Display"] = "block";
            }

        }

        protected void buscar_click(object sender, EventArgs e)
        {
            limpiarError();
            try
            {
                ///////////////////////BUSCAR Privilegio
                string Privilegio = txt_buscar_Privilegio.Text;
                string qry =
                    "SELECT distinct PRIV, usuarios, [Listar Usuarios],[CRUD Privilegios],[Mantenimiento Usuarios] " +
                    "FROM " +
                    "(SELECT sum(distinct iif(pw.is_deleted=1,0,1)) as Existe, count(distinct U.UserName) as usuarios, wi.description AS VENT, P.Description as PRIV " +
                    "from Privileges P " +
                    "left join PrivilegeWindow pw on p.Id = pw.PrivilegeID " +
                    "left join Windows wi on pw.WindowID = wi.Id " +
                    "LEFT JOIN UserPrivileges UP ON UP.PrivilegeID = P.Id " +
                    "left JOIN Users U ON U.Id = UP.UserID " +
                    "where p.description='" + Privilegio + "' " +
                    "and p.is_deleted = 0 " +
                    "and wi.is_deleted = 0 " +
                    "group by wi.description, P.Description) p " +
                    "PIVOT " +
                    "( " +
                    "sum(Existe) " +
                    "FOR VENT IN " +
                    "([Listar Usuarios],[CRUD Privilegios],[Mantenimiento Usuarios]) " +
                    ") AS pvt " +
                    "ORDER BY pvt.PRIV;";

                SqlCommand sqlCmd = new SqlCommand(qry, con);
                con.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                limpiarError();
                limpiarTextos();
                if (dt.Rows.Count == 0)
                {
                    label_error_mantenimiento.Text = "Privilegio no existe";
                }
                else
                {
                    txt_mantiene_Privilegio.Text = dt.Rows[0][0].ToString();
                    txt_usuarios_Privilegio.Text = dt.Rows[0][1].ToString();
                    if (dt.Rows[0][2].ToString() == "1")
                    { listar_M_checkbox.Checked = true; }
                    if (dt.Rows[0][3].ToString() == "1")
                    { privilegios_M_checkbox.Checked = true; }
                    if (dt.Rows[0][4].ToString() == "1")
                    { mantenimiento_M_checkbox.Checked = true; }
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

            if (string.IsNullOrEmpty(txt_alta_Privilegio.Text))
            {
                limpiarError();
                limpiarTextos();
                label_error_nuevo.Text = "Error! Nombre de Privilegio no valido";
                error = true;
            }
            if (!error)
            {
                try
                {
                    con.Open();
                    ///////////////////////////Inserta Privilegio
                    string qry =
                        "IF NOT EXISTS(SELECT 1 FROM Privileges WHERE Description = '" + txt_alta_Privilegio.Text + "') " +
                        "BEGIN TRY   " +
                        "   begin transaction " +
                        "       insert into privileges(Id, Description, is_deleted) " +
                        "       values(NEWID(), '" + txt_alta_Privilegio.Text + "', 0); " +
                        "   commit transaction; " +
                        "END TRY " +
                        "BEGIN CATCH " +
                        "    SELECT " +
                        "        ERROR_NUMBER() AS ErrorNumber " +
                        "       ,ERROR_MESSAGE() AS ErrorMessage;  " +
                        "END CATCH ";

                    SqlCommand sqlCmd = new SqlCommand(qry, con);
                    Int32 result = (Int32)sqlCmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        var listar = 1;
                        var mantenimiento = 1;
                        var privilegios = 1;
                        if (listar_checkbox.Checked)
                        { listar = 0; }
                        if (mantenimiento_checkbox.Checked)
                        { mantenimiento = 0; }
                        if (privilegios_checkbox.Checked)
                        { privilegios = 0; }

                        ///////////////////////////Inserta PrivilegioPERMISO
                        ///
                        qry =
                            "BEGIN TRY   " +
                            "   begin transaction " +
                            "		INSERT INTO tp_integrador.dbo.privilegewindow (Id, PrivilegeID, WindowID, is_deleted) " +
                            "		values " +
                            "		(NEWID(),(select ID from Privileges where description = '" + txt_alta_Privilegio.Text + "'),(select Id from Windows where description = 'Listar Usuarios'),"+listar+"), " +
                            "		(NEWID(),(select ID from Privileges where description = '" + txt_alta_Privilegio.Text + "'),(select Id from Windows where description = 'Mantenimiento Usuarios'),"+mantenimiento+"), " +
                            "		(NEWID(),(select ID from Privileges where description = '" + txt_alta_Privilegio.Text + "'),(select Id from Windows where description = 'CRUD Privilegios'),"+privilegios+"); " +
                            "   commit transaction; " +
                            "END TRY " +
                            "BEGIN CATCH " +
                            "    SELECT " +
                            "        ERROR_NUMBER() AS ErrorNumber " +
                            "       ,ERROR_MESSAGE() AS ErrorMessage;  " +
                            "END CATCH ";
                        sqlCmd = new SqlCommand(qry, con);
                        result = (Int32)sqlCmd.ExecuteNonQuery();

                        label_error_nuevo.Text = "Privilegio Creado!";
                    }
                    else
                    {
                        label_error_nuevo.Text = "Privilegio ya existe";
                    }
                    limpiarTextos();
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
            crearPrivilegio.Style["Display"] = "Block";
            mantenimientoPrivilegio.Style["Display"] = "none";
        }

        protected void buttonEdit_Click(object sender, EventArgs e)
        {
            limpiarError();
            limpiarTextos();
            crearPrivilegio.Style["Display"] = "none";
            mantenimientoPrivilegio.Style["Display"] = "block";
        }
        protected void editar_Click(object sender, EventArgs e)
        {
            limpiarError();
            if (string.IsNullOrEmpty(txt_mantiene_Privilegio.Text))
            {
                limpiarTextos();
                label_error_mantenimiento.Text = "No hay un Privilegio seleccionado";
            }
            else
            {
                try
                {
                    var listar = 1;
                    var mantenimiento = 1;
                    var privilegios = 1;
                    if (listar_M_checkbox.Checked)
                    { listar = 0; }
                    if (mantenimiento_M_checkbox.Checked)
                    { mantenimiento = 0; }
                    if (privilegios_M_checkbox.Checked)
                    { privilegios = 0; }
                    con.Open();
                    ///////////////////////////Actualizar roles Privilegio
                    string qry =
                            "BEGIN TRY   " +
                            "   begin transaction " +
                            "       update X " +
                            "       set is_deleted = " + listar + " " +
                            "       from tp_integrador.dbo.privilegewindow X " +
                            "       where PrivilegeID in (select ID from Privileges where description = '" + txt_mantiene_Privilegio.Text + "') " +
                            "       and WindowID in (select Id from Windows where description = 'Listar Usuarios'); " +
                            " " +
                            "       update X " +
                            "       set is_deleted = " + mantenimiento + " " +
                            "       from tp_integrador.dbo.privilegewindow X " +
                            "       where PrivilegeID in (select ID from Privileges where description = '" + txt_mantiene_Privilegio.Text + "') " +
                            "       and WindowID in (select Id from Windows where description = 'Mantenimiento Usuarios'); " +
                            " " +
                            "       update X " +
                            "       set is_deleted = " + privilegios + " " +
                            "       from tp_integrador.dbo.privilegewindow X " +
                            "       where PrivilegeID in (select ID from Privileges where description = '" + txt_mantiene_Privilegio.Text + "') " +
                            "       and WindowID in (select Id from Windows where description = 'CRUD Privilegios'); " +
                            "   commit transaction; " +
                            "END TRY " +
                            "BEGIN CATCH " +
                            "    SELECT " +
                            "        ERROR_NUMBER() AS ErrorNumber " +
                            "       ,ERROR_MESSAGE() AS ErrorMessage;  " +
                            "END CATCH ";
                    SqlCommand sqlCmd = new SqlCommand(qry, con);
                    Int32 result = (Int32)sqlCmd.ExecuteNonQuery();
                    limpiarError();
                    limpiarTextos();
                    if (result <= 1)
                    {

                        label_error_mantenimiento.Text = "Error durante la actualización";
                    }
                    else
                    {
                        label_error_mantenimiento.Text = "Privilegio Actualizado!";
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
            //////////////////////Valida Privilegio Existe
            if (string.IsNullOrEmpty(txt_mantiene_Privilegio.Text))
            {
                label_error_mantenimiento.Text = "No hay un Privilegio seleccionado";
            }
            else
            if(txt_usuarios_Privilegio.Text!="0")
            {
                label_error_mantenimiento.Text = "No se pueden eliminar privilegios con usuarios";
            }
            else
            {
                try
                {
                    con.Open();
                    ///////////////////////////Soft delete Privilegio
                    string qry =
                        "update Privileges " +
                        "set is_deleted = 1 " +
                        "where Description = '" + txt_mantiene_Privilegio.Text + "';";
                    SqlCommand sqlCmd = new SqlCommand(qry, con);
                    Int32 result = (Int32)sqlCmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        label_error_mantenimiento.Text = "Privilegio borrado!";
                    }
                    else
                    {
                        label_error_mantenimiento.Text = "Error de borrado";
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
            label_error_nuevo.Text = "";
            label_error_mantenimiento.Text = "";
        }

        protected void limpiarTextos()
        {
            txt_buscar_Privilegio.Text = "";
            txt_alta_Privilegio.Text = "";
            txt_mantiene_Privilegio.Text = "";
            txt_usuarios_Privilegio.Text = "0";
            listar_M_checkbox.Checked = false;
            mantenimiento_M_checkbox.Checked = false;
            privilegios_M_checkbox.Checked = false;
            listar_checkbox.Checked = false;
            mantenimiento_checkbox.Checked = false;
            privilegios_checkbox.Checked = false;
        }
    }
}