using Interfaces.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Interfaces.Clases
{
    static class Servicio
    {
        static Service1Client service;
        private static String nombre, password;

        public static Service1Client GetInstance()
        {
            if (service == null)
            {
                service = new Service1Client();
            }
            return service;
        }
        public static void Login(String nombred, String passwordd)
        {
            nombre = nombred;
            password = passwordd;
            service.loginAsync(nombre, password);
        }
        public static void GetListAnime(String nombre)
        {
           service.ListaSeriesAsync(nombre);
        }
        public static void UpdateVista(String user, Boolean tu, String anime)
        {
            service.update_vista_animeAsync(user, tu, anime);
        }
        public static void GetListAnimequenotengo(String nombre)
        {
            service.Get_listaquenotengoAsync(nombre);
        }
        public static void añadeanime(String user, String Nombre, Boolean tu)
        {
           service.Añade_alistaAsync( user, Nombre, tu);
        }

        internal static void GetNoticias()
        {
            service.GetNewsAsync();
        }
    }
}
