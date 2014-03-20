using Interfaces.Clases;
using Interfaces.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace Interfaces.view
{
    /// <summary>
    /// Lógica de interacción para Page.xaml
    /// </summary>
    public partial class Lista : Page
    {
        private List<SeriesUser> series;

        public Lista()
        {
            InitializeComponent();
        }

        public void Set_Lista(SeriesUser[] seriesUser)
        {
            series = seriesUser.ToList<SeriesUser>();
            lista.ItemsSource = series;
            
        }
        public List<SeriesUser> Get_lista()
        {
            return series;
        }

        private void fil_Click(object sender, RoutedEventArgs e)
        {
            lista.ItemsSource = null;
            lista.ItemsSource = series.FindAll(d => d.Nonmbre.ToLower().StartsWith(Expresion.Text.ToLower()));
        }
        public void setname2C(String name)
        {
            nombre2c.Tag = name;
        }
        private void Vista_Checked(object sender, RoutedEventArgs e)
        {

            switch (nombre2c.Tag.ToString())
            {
                case "Vista":
                    Servicio.UpdateVista(MainWindow.nombre, true, ((e.Source as CheckBox).Tag.ToString()));
                    break;
                case "Quiero tener":
                    Servicio.añadeanime(MainWindow.nombre, ((e.Source as CheckBox).Tag.ToString()), true);
                    break;
                default:
                    break;
            }


        }

        private void Vista_Unchecked(object sender, RoutedEventArgs e)
        {
            switch (nombre2c.Tag.ToString())
            {
                case "Vista":
                    Servicio.UpdateVista(MainWindow.nombre, false, ((e.Source as CheckBox).Tag.ToString()));
                    break;
                case "Quiero tener":
                   
                    break;
                default:
                    break;
            }
           
        }

        private void lista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

    }
}
