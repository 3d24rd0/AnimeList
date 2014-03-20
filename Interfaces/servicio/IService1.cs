using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace servicio
{

    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        Boolean login(String user, String password);

        [OperationContract]
        List<SeriesUser> ListaSeries(String user);

        [OperationContract]
        void update_vista_anime(String user, Boolean tu, String anime);

        [OperationContract]
        List<SeriesUser> Get_listaquenotengo(String user);

        [OperationContract]
        void Añade_alista(String user, String Nombre, Boolean tu);

        [OperationContract]
        List<AnnimeNews> GetNews();
    }


    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    [DataContract]
    public class AnnimeNews
    {
        private String title;
        private String description;
        private String author;
        private DateTime pubDate;
        private String link;//torrent
        private String guid;//url

        [DataMember]
        public String Title
        {
            get { return title; }
            set { title = value; }
        }
        [DataMember]
        public String Description
        {
            get { return description; }
            set { description = value; }
        }
        [DataMember]
        public String Author
        {
            get { return author; }
            set { author = value; }
        }
        [DataMember]
        public DateTime PubDate
        {
            get { return pubDate; }
            set { pubDate = value; }
        }
        [DataMember]
        public String Link
        {
            get { return link; }
            set { link = value; }
        }
        [DataMember]
        public String Guid
        {
            get { return guid; }
            set { guid = value; }
        }
    }
    [DataContract]
    public class SeriesUser
    {
        private String nonmbre;
        private Boolean tener;
        private Boolean vista;
        [DataMember]
        public String Nonmbre
        {
            get { return nonmbre; }
            set { nonmbre = value; }
        }
        [DataMember]
        public Boolean Tener
        {
            get { return tener; }
            set { tener = value; }
        }
        [DataMember]
        public Boolean Vista
        {
            get { return vista; }
            set { vista = value; }
        }
    }
}
