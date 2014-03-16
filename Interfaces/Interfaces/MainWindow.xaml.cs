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
namespace Interfaces
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            this.Hide();
            
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            ShowCustomBalloon();
        }

        private void Accesibilidad_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //ShowCustomBalloon();
            
        }
        private void ShowCustomBalloon()
        {
            ToolTip a = new ToolTip();

            //show balloon and close it after 4 seconds
            tasckIcon.ShowCustomBalloon(a, PopupAnimation.Slide, 4000);
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            
        }

    }
}
