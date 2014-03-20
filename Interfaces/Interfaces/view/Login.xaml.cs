using Interfaces.Clases;
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
using System.Windows.Shapes;

namespace Interfaces.view
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class login : Window
    {
        public delegate void DLogin(String nombre);
        public event DLogin lo;

        public login()
        {
            InitializeComponent();
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(
                             new ResourceDictionary() { Source = new Uri("Themes/ShinyRed.xaml", UriKind.RelativeOrAbsolute) });

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //logea
            this.Hide();
            this.lo(nombre.Text);
            Servicio.Login(nombre.Text, pass.Password);
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
