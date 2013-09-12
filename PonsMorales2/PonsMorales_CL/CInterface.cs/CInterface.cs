using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.IO;

public enum target { _blank, _parent, _self, _top };
public enum tipoItem { Texto, Foto, Video, Documento, Enlace, InscripcionRelacionado, MenuRelacionado, Todos };
public enum estadoInmueble { Todos = 0, Nuevo=1, Usado = 2, En_Construccion = 3, Para_Reformar = 4, Reformado= 5 };
public enum opcionInmueble { Todos = 0, Venta = 1, Alquiler = 2, Alquiler_Vacacional = 3 ,Traspaso=4};
public enum estadoComercializacion { Todos = 0, Disponible = 1, Vendido = 2, Alquilado = 3, Reservado_Venta = 4, Reservado_Alquiler = 5, Retirado_Venta=6, Retirado_Alquiler=7};

public partial class CInterface
{

    public string m_sConnString = "Server=ext-mssql.blackslot.com,41433;UID=lupa_PonsUsr;PWD=lupa_PonsAdmin;database=lupa_Pons";
    public SqlConnection m_sqlConn;
    public bool Connect()
    {
        try
        {
            m_sqlConn = new SqlConnection(m_sConnString);
            m_sqlConn.Open();

            if (m_sqlConn.State == ConnectionState.Open)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public bool DisConnect()
    {
        try
        {
            m_sqlConn.Close();
            if (m_sqlConn.State == ConnectionState.Closed)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public string m_filtrar_Html(string Titulo)
    {
        //Titulo = Titulo.Replace("'", "");
        Titulo = Titulo.Replace("'", "\"");
        Titulo = Titulo.Replace("[", "<");
        Titulo = Titulo.Replace("]", ">");

        return Titulo;
    }
    public string m_filtrarHtmlParaJavascript(string Titulo)
    {
        Titulo = Titulo.Replace("'", "\"");
        Titulo = Titulo.Replace("\n", "");

        return Titulo;
    }
    public string m_eliminarAcentos(string Titulo)
    {
        Titulo = Titulo.Replace("Á", "A");
        Titulo = Titulo.Replace("É", "E");
        Titulo = Titulo.Replace("Í", "I");
        Titulo = Titulo.Replace("Ó", "O");
        Titulo = Titulo.Replace("Ú", "U");
        Titulo = Titulo.Replace("Ñ", "N");

        return Titulo;
    }
    public string m_formatToMoney(decimal valor)
    {
        return String.Format("{0:C2}", valor).Replace(",00", "");
    }
    public string m_formatToNumeric(string valor)
    {
        return String.Format("{0:C}", valor);
    }
    public string getMes(int Num)
    {
        switch (Num)
        {
            case 1:
                return "Enero";
            case 2:
                return "Febrero";
            case 3:
                return "Marzo";
            case 4:
                return "Abril";
            case 5:
                return "Mayo";
            case 6:
                return "Junio";
            case 7:
                return "Julio";
            case 8:
                return "Agosto";
            case 9:
                return "Septiembre";
            case 10:
                return "Octubre";
            case 11:
                return "Noviembre";
            default:
                return "Diciembre";
        }
    }
    public string getDia(string Day)
    {
        switch (Day)
        {
            case "Monday":
                return "Lunes";
            case "Tuesday":
                return "Martes";
            case "Wednesday":
                return "Miercoles";
            case "Thursday":
                return "Jueves";
            case "Friday":
                return "Viernes";
            case "Saturday":
                return "Sábado";
            default:
                return "Domingo";
        }
    }
    public string getFavoritos(string lista, string clave)
    {
        string resultado = "";

        if (lista.Trim() != "")
        {
            char[] splitter = { ';' };
            string[] arInfo = lista.Split(splitter);

            for (int x = 0; x < arInfo.Length; x++)
            {
                string fav = arInfo[x];
                if (fav.Trim() != "")
                {
                    if (fav.Contains(clave))
                        resultado += fav + ";";
                }
            }
        }

        return resultado;
    }
    public int getFavoritos_Count(string lista, string clave)
    {
        int elementos = 0;

        if (lista.Trim() != "")
        {
            char[] splitter = { ';' };
            string[] arInfo = lista.Split(splitter);

            foreach (string s in arInfo)
            {
                if (s.Trim() != "")
                    if (s.Trim().Contains(clave))
                        elementos++;
            }
        }

        return elementos;
    }
    public string getfechaConFormato(DateTime fecha)
    {
        return fecha.Day.ToString() + " de " + getMes(fecha.Month) + " de " + fecha.Year;

    }
    public string get_DateIdioma(DateTime fecha)
    {
        return fecha.Year + "/" + fecha.Month + "/" + fecha.Day;
    }


}
