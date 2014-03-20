using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace servicio.clases
{
    internal class Mysql
    {
        private static MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
    
        //Constructor
        internal Mysql()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "listanime";
            uid = "root";
            password = "edu1";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + 
		    database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        //MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                       // MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException )
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }
        public Boolean InsertUser(String user, String pass)
        {
            if (OpenConnection())
            {
                try
                {
                    List<string> l= Lista_Users();
                    if (!l.Contains(user))
                    {

                        string query = "INSERT INTO login (usuario, password) VALUES('" + user + "', SHA('" + pass + "'))";
                        MySqlCommand cmd = new MySqlCommand(query, connection);

                        //Execute command
                        cmd.ExecuteNonQuery();
                        //CloseConnection();
                        
                        CloseConnection();
                        return true;
                    }
                    else
                    {
                        CloseConnection();
                        //ya existe
                        return false;
                    }

                }
                catch
                {
                    CloseConnection();
                    return false;
                }               
               
            }
            else
            {
                CloseConnection();
                return false;
            }
        }
       
        public List<string> Lista_Users()
        {
            if (OpenConnection())
            {
                List<string> list = new List<string>();

                string query = "SELECT usuario, password FROM login";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list.Add(dataReader["usuario"] + "");

                }

                //close Data Reader
                dataReader.Close();

                //return list to be displayed
                CloseConnection();
                return list;
               
            }else{
                CloseConnection();
                return null;
               
            }
        }
        public Boolean Login(String user, String pass)
        {
            if (OpenConnection())
            {
                string query = "SELECT idlogin FROM login WHERE usuario='"+user+"' AND password=SHA('"+pass+"')";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                Boolean log;
                if (dataReader.Read())
                {
                    log = true;
                }
                else
                {
                    log = false;
                }
                //close Data Reader
                dataReader.Close();
                CloseConnection();
                return log;
            }
            else
            {
                CloseConnection();
                return false;
            }

        }

        private int GetIDForUser(String user)
        {
            if (OpenConnection())
            {
                string query = "SELECT idlogin FROM login WHERE usuario='" + user + "'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {
                    int temp = int.Parse(dataReader["idlogin"].ToString());
                    CloseConnection();
                    dataReader.Close();
                    return temp;
                }
                else
                {
                    CloseConnection();
                    dataReader.Close();
                    return -1;
                }

            }
            else
            {
                CloseConnection();
                return -1;
            }
        }

        internal List<SeriesUser> Get_Series_By_user(string user)
        {
             int id = GetIDForUser(user);
             if (id != -1)
             {
                 if (OpenConnection())
                 {
                     string query = "SELECT Nombre, vista,tener FROM login_has_series, series where " + id + "=login_has_series.login_idlogin and login_has_series.series_idseries=series.idseries;";
                     MySqlCommand cmd = new MySqlCommand(query, connection);
                     MySqlDataReader dataReader = cmd.ExecuteReader();

                     List<SeriesUser> list = new List<SeriesUser>();
                     while (dataReader.Read())
                     {
                         SeriesUser serie = new SeriesUser();
                         serie.Nonmbre = dataReader["Nombre"].ToString();

                         Boolean bo = false;
                         if (dataReader["vista"].ToString().Equals("S"))
                         {
                             bo = true;
                         }

                         serie.Vista = bo;

                         bo = false;
                         if (dataReader["tener"].ToString().Equals("1"))
                         {
                             bo = true;
                         }

                         serie.Tener = bo;
                         list.Add(serie);

                     }
                     //close Data Reader
                     dataReader.Close();
                     CloseConnection();
                     return list;

                 }
                 else
                 {
                     CloseConnection();
                     return null;
                 }

             }
             else
             {
                 CloseConnection();
                 return null;
             }
        }
        //UPDATE `listanime`.`login_has_series` SET `vista`='S' WHERE `login_idlogin`='1' and`series_idseries`='4';
        internal void Update_vista(String user,String Nombre , Boolean tu)
        {
            int id_user = GetIDForUser(user);
            if (id_user != -1)
            {
                int id_anime = GetIDAnime(Nombre);
                if (id_anime != -1)
                {
                    if (OpenConnection())
                    {
                        String sn = "N";
                        if (tu)
                        {
                            sn = "S";
                        }
                        string query = "UPDATE `listanime`.`login_has_series` SET `vista`='"+sn+"' WHERE `login_idlogin`='"+id_user+"' and`series_idseries`='"+id_anime+"';";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                       // MySqlDataReader dataReader = cmd.ExecuteReader();

                        //close Data Reader
                        //dataReader.Close();
                        CloseConnection();
                    }
                }
            }
                CloseConnection();
            
        }

        private int GetIDAnime(string Nombre)
        {
            if (OpenConnection())
            {
                string query = "SELECT idseries FROM listanime.series where Nombre = \""+Nombre+"\";";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {
                    int temp = int.Parse(dataReader["idseries"].ToString());
                    CloseConnection();
                    dataReader.Close();
                    return temp;
                }
                else
                {
                    CloseConnection();
                    dataReader.Close();
                    return -1;
                }

            }
            else
            {
                CloseConnection();
                return -1;
            }
        }

        internal List<SeriesUser> Get_All_List_notuser(String user)
        {
            int id = GetIDForUser(user);
            if (id != -1)
            {
                if (OpenConnection())
                {
                    string query = "SELECT * FROM series se where not EXISTS(select *  from login_has_series f where f.login_idlogin="+id+" and f.series_idseries=se.idseries);";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    List<SeriesUser> list = new List<SeriesUser>();
                    while (dataReader.Read())
                    {
                        SeriesUser serie = new SeriesUser();
                        serie.Nonmbre = dataReader["Nombre"].ToString();

                        Boolean bo = false;

                        serie.Vista = bo;
                        list.Add(serie);

                    }
                    //close Data Reader
                    dataReader.Close();
                    CloseConnection();
                    return list;

                }
                else
                {
                    CloseConnection();
                    return null;
                }

            }
            else
            {
                CloseConnection();
                return null;
            }
        }
        //INSERT INTO `listanime`.`login_has_series` (`login_idlogin`, `series_idseries`, `vista`) VALUES ('1', '5', 'S');
        internal void Añade_alista(String user, String Nombre, Boolean tu)
        {
            int id_user = GetIDForUser(user);
            if (id_user != -1)
            {
                int id_anime = GetIDAnime(Nombre);
                if (id_anime != -1)
                {
                    if (OpenConnection())
                    {
                        String sn = "N";
                        if (tu)
                        {
                            sn = "S";
                        }
                        string query = "INSERT INTO `listanime`.`login_has_series` (`login_idlogin`, `series_idseries`, `vista`) VALUES ('"+id_user+"', '"+id_anime+"', '"+sn+"');";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        // MySqlDataReader dataReader = cmd.ExecuteReader();
                        //close Data Reader
                        //dataReader.Close();
                        CloseConnection();
                    }
                }
            }
        }
    }
}