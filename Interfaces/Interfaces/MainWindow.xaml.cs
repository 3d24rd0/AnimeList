using Interfaces.Clases;
using Interfaces.view;
using Interfaces.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hardcodet.Wpf.TaskbarNotification;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Interfaces
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static String nombre;
        Navegador ventanas;
        public MainWindow()
        {
          
            InitializeComponent();
            lista.Visibility = Visibility.Collapsed;
            datos.Visibility = Visibility.Collapsed;
            noti.Visibility = Visibility.Collapsed;
            this.Closing +=MainWindow_Closing;
            this.Hide();
            Servicio.GetInstance().loginCompleted += MainWindow_loginCompleted;
            Servicio.GetInstance().ListaSeriesCompleted += MainWindow_ListaSeriesComplete;
            Servicio.GetInstance().Get_listaquenotengoCompleted += MainWindow_Get_listaquenotengoCompleted;
            Servicio.GetInstance().GetNewsCompleted += MainWindow_GetNewsCompleted;
            ventanas = new Navegador();

       }

        #region cerrar
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tasckIcon.Visibility = Visibility.Collapsed;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
        #region login
        void MainWindow_loginCompleted(object sender, loginCompletedEventArgs e)
        {
            if (e.Result)
            {
                Accesibilidad.Visibility = Visibility.Collapsed;
                string title = "Login Completado";
                string text = "Establecienda la conexionn";
                tasckIcon.HideBalloonTip();
                tasckIcon.ShowBalloonTip(title, text, BalloonIcon.Info);
                lista.Visibility = Visibility.Visible;
                datos.Visibility = Visibility.Visible;
                noti.Visibility = Visibility.Visible;
            }
            else
            {
                string title = "Login";
                string text = "Error al connectar";

                tasckIcon.HideBalloonTip();
                tasckIcon.ShowBalloonTip(title, text, BalloonIcon.Error);
            }
        }
        
        private void login(object sender, RoutedEventArgs e)
        {
            login lo = new login();
            lo.lo += lo_lo;
            lo.ShowDialog();
        }

        /// <summary>
        /// Se ejecuta cuando se pulsa el boton de login en la ventana de login
        /// </summary>
        private void lo_lo(string nombree)
        {
            nombre = nombree;
            string title = "Logeandose";
            string text = "Estableciendo la conexion";
            tasckIcon.HideBalloonTip();
            tasckIcon.ShowBalloonTip(title, text, BalloonIcon.Info);
        }

        #endregion
        #region estilos
        private void uno_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources.MergedDictionaries.Clear();
            if (!(sender as MenuItem).Name.Equals("default"))
            {
                Application.Current.Resources.MergedDictionaries.Add(
                             new ResourceDictionary() { Source = new Uri("Themes/" + (sender as MenuItem).Header + ".xaml", UriKind.RelativeOrAbsolute) });
            }

        }
        #endregion
        #region lista que tengo
        private void MainWindow_ListaSeriesComplete(object sender, ListaSeriesCompletedEventArgs e)
        {
            Lista lis = new Lista();
            lis.Set_Lista(e.Result);
            ventanas.navigate(lis);
            ventanas.Show();
        }

        private void lis_Click(object sender, RoutedEventArgs e)
        {
            Servicio.GetListAnime(nombre);
        }
        #endregion
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
       
        }
        #region series que no he visto
        private void All_Click(object sender, RoutedEventArgs e)
        {
            Servicio.GetListAnimequenotengo(nombre);
        }
        void MainWindow_Get_listaquenotengoCompleted(object sender, Get_listaquenotengoCompletedEventArgs e)
        {
            Lista lis = new Lista();
            lis.Set_Lista(e.Result);
            lis.setname2C("Quiero tener");
            ventanas.navigate(lis);
            ventanas.Show();
        }
        #endregion
        #region exportar fichero
        private void xmlexport_Click(object sender, RoutedEventArgs e)
        {
            Frame a = ventanas.getnavigate();
            Lista s = a.Content as Lista;
           
            // Insert code to set properties and fields of the object.
            XmlSerializer mySerializer = new
            XmlSerializer(typeof(List<SeriesUser>));
            // To write to a file, create a StreamWriter object.
            StreamWriter myWriter = new StreamWriter("myFileName.xml");
            mySerializer.Serialize(myWriter, s.Get_lista());
            myWriter.Close();
        }
        #endregion
        #region obtener noticias
        private void noti_Click(object sender, RoutedEventArgs e)
        {
            Servicio.GetNoticias();
        }
        void MainWindow_GetNewsCompleted(object sender, GetNewsCompletedEventArgs e)
        {
            List<AnnimeNews> se = e.Result.ToList<AnnimeNews>();
            se.ForEach(anime =>
                {
                
                    string title = anime.Title;
                    string text = anime.Author;
                    tasckIcon.HideBalloonTip();
                    //tasckIcon.TrayBalloonTipClicked += tasckIcon_TrayBalloonTipClicked;
                    //tasckIcon.MouseLeftButtonDown += tasckIcon_MouseLeftButtonDown;
                     
                    tasckIcon.ShowBalloonTip(title, text, BalloonIcon.Info);
                    System.Threading.Thread.Sleep(5000);
                }
            );
        }
        #endregion
    }
}
