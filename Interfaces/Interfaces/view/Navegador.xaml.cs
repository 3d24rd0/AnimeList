﻿using System;
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
    /// Lógica de interacción para Navegador.xaml
    /// </summary>
    public partial class Navegador : Window
    {
        public Navegador()
        {
            InitializeComponent();
        }
        public void navigate(Lista p)
        {
            frame.Navigate(p);
        }
        public Frame getnavigate(){
            return frame;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

    }
}
