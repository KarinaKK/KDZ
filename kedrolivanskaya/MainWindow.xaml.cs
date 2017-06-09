using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace kedrolivanskaya
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
       // public Budget GetBudget { get; set; }
        public bool IsAuthorized { get; set; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {           
            MainFrame.Content = new MainPage(this);           
        }

        public Budget CurrentBudget { get; set; }
        public User GetCurrentUser
        {
            get { return _currUser; }
            set
            {
                _currUser = value;              
            }
        }                 
        private User _currUser;

        private void LinkAuth_OnClick(object sender, RoutedEventArgs e)
        {
            if (_currUser != null)
            {
                WelcomeTb.Text = "";
                (sender as Button).Content = "Войти";                
                  //  if (MessageBox.Show("\t\tСохранить изменения?", "\tСохранение", MessageBoxButton.YesNo)==MessageBoxResult.Yes)
                    //    GetBudget.Save_Data();
                _currUser = null;
                
                MainFrame.Content = new MainPage(this);
                
            }
            else
            {
                AuthorizationWindow p = new AuthorizationWindow {Owner = this};
                p.ShowDialog();
                if (_currUser != null)
                {
                    WelcomeTb.Text = _currUser.Name + "   " + _currUser.Surname + ", Добро пожаловать";
                    (sender as Button).Content = "Выйти";
                    MainFrame.Content = new MainPage(this);
                }
            }
        }
        
        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (!(this.DataContext as Budget).check_Updates())
                switch (MessageBox.Show("\t\tСохранить изменения?", "\tСохранение", MessageBoxButton.YesNoCancel))
            {
                case MessageBoxResult.Cancel:
                {
                    e.Cancel = true;
                        break;                    
                }
                case MessageBoxResult.Yes:
                {
                    (this.DataContext as Budget).Save_Data();
                        break;
                }

            }
            

        }
    }
}
