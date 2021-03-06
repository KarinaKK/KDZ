﻿using System.Windows;
using System.Windows.Controls;
using static kedrolivanskaya.Security;

namespace kedrolivanskaya
{
    /// <summary>
    /// Логика взаимодействия для Page_Avtorization.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        private AuthorizationWindow _authorizationWindow;

        public RegistrationPage(AuthorizationWindow authorizationWindow)
        {
            InitializeComponent();
            _authorizationWindow = authorizationWindow;
        }



        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            User user=new User();
            if (Check_Fields())
            {
                user.Name = TextboxFirstname.Text;
                user.Surname = TextboxLastname.Text;
                user.BirthDate = DatePicker.SelectedDate.Value;
                user.Password = HashPassword(TextboxPassword.Password);
                if (!is_registred(user))
                {
                    
                    ((_authorizationWindow.Owner as MainWindow).DataContext as Budget).Family.Add(user);
                    ((_authorizationWindow.Owner as MainWindow).DataContext as Budget).Save_Data();
                    (_authorizationWindow.Owner as MainWindow).GetCurrentUser = user;                    
                    MessageBox.Show("Вы успешно зарегистрированы!", "\tРегистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                    _authorizationWindow.Close();
                }
                else
                {
                    MessageBox.Show("Пользователь с таким именем и фамилией уже зарегистрирован!", "Некорректный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            
            
        }

        private bool is_registred(User user)
        {           
            foreach (var tempUser in ((_authorizationWindow.Owner as MainWindow).DataContext as Budget).Family)
            {
                if (tempUser.Name.Trim().ToLower() == user.Name.Trim().ToLower()
                    && tempUser.Surname.Trim().ToLower() == tempUser.Surname.Trim().ToLower())
                {
                    MessageBox.Show("Пользователь с таким именем и фамилией уже зарегистрирован!", "Некорректный ввод",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return true;
                }
            }
            return false;
        }
        private bool Check_Fields()
        {

            if(!(Checker.check_text(TextboxFirstname.Text,"Введите имя!")))
                return false;
            if (!(Checker.check_text(TextboxLastname.Text, "Введите фамилию!")))
                return false;
            if (!DatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Выберите дату", "Некорректный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (!(Checker.check_text(TextboxPassword.Password, "Введите пароль!")))
                return false;
            if (!(Checker.check_text(TextboxPasswordConfirm.Password, "Подтвердите пароль!")))
                return false;
            if(TextboxPassword.Password!=TextboxPasswordConfirm.Password)
            {
                MessageBox.Show("Введенные пароли не совпадают", "Некорректный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
    }
}
