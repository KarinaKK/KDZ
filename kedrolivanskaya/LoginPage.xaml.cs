using System.Windows;
using System.Windows.Controls;
using static kedrolivanskaya.Security;
namespace kedrolivanskaya
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage(AuthorizationWindow window)
        {
            InitializeComponent();
            _authorizationWindow = window;
        }

        private void log_in_click(object sender, RoutedEventArgs e)
        {
            bool isReal=false;
            if (Check_Fields())
            {
                foreach (var user in ((_authorizationWindow.Owner as MainWindow).DataContext as Budget).Family)
                {
                    if ((user.Name + " " + user.Surname).Trim().ToLower() == TextboxLogin.Text.Trim().ToLower())
                    {
                        if (VerifyHashedPassword(user.Password, PasswordBox.Password))
                        {
                            (_authorizationWindow.Owner as MainWindow).GetCurrentUser = user;                                                     
                            _authorizationWindow.Close();
                        }
                        else
                        {
                            MessageBox.Show("Пароль введен неверно!", "Некорректные данные", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        isReal = true;
                    }
                }
                if(isReal==false)
                    MessageBox.Show("Пользователь с таким именем и фамилией не зарегистрирован!", "Некорректные данные", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private bool Check_Fields()
        {

            if (!(Checker.check_text(TextboxLogin.Text, "Введите имя и фамилию!")))
                return false;           
            if (!(Checker.check_text(PasswordBox.Password, "Введите пароль!")))
                return false;            
            return true;
        }

        private AuthorizationWindow _authorizationWindow;
        private void Sign_up_click(object sender, RoutedEventArgs e)
        {
            _authorizationWindow.AuthFrame.Content = new RegistrationPage(_authorizationWindow);
        }
    }
}
