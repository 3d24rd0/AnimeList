using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using servicio.clases;
using System.Data;

namespace servicio
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
       
        public List<AnnimeNews> GetNews()
        {
            return Read_rss.GetData();
        }

        public Boolean login(String user, String password)
        {
            Mysql db = new Mysql();
            return db.Login(user, password);
        }

        private void InsertUser(String user, String Password)
        {
            Mysql db = new Mysql();
            db.InsertUser(user, Password);

        }
        public List<SeriesUser> ListaSeries(String user)
        {
            Mysql db = new Mysql();
            List<SeriesUser> s = db.Get_Series_By_user(user);
            return s;
        }
        public void update_vista_anime(String user, Boolean tu, String anime)
        {
            Mysql db = new Mysql();
            db.Update_vista(user,anime,tu);
        }
        public List<SeriesUser> Get_listaquenotengo(String user)
        {
            Mysql db = new Mysql();
            return db.Get_All_List_notuser(user);
        }
        public void Añade_alista(String user, String Nombre, Boolean tu)
        {
            Mysql db = new Mysql();
            db.Añade_alista( user, Nombre, tu);
        }
    }
}
