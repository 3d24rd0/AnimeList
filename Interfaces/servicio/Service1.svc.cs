using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Oracle.DataAccess.Client;

namespace servicio
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        private OracleConnection con;
        private void Connect()
        {
            con = new OracleConnection();
            con.ConnectionString = "User Id=edaurdo;Password=eduardo;Data Source=eduardo";
            con.HostName = "192.168.0.10";
            con.Open();
            
            Console.WriteLine("Connected to Oracle" + con.ServerVersion);
        }
        private void Close()
        {
            con.Close();
            con.Dispose();
        }

        public Boolean Test()
        {
            try
            {
                Connect();
                Close();
                return true;
            }
            catch
            {
                return false;
            }
        } 
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
