using System;
using System.Windows;
using System.Windows.Controls;

namespace kedrolivanskaya
{
    /// <summary>
    /// Логика взаимодействия для Sig_up_window.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {        
        public AuthorizationWindow()
        {
            InitializeComponent();            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AuthFrame.Content = new LoginPage(this);
        }

        
    }
}
