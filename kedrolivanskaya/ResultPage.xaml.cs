using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using static kedrolivanskaya.Checker;

namespace kedrolivanskaya
{
    /// <summary>
    /// Логика взаимодействия для ResultPage.xaml
    /// </summary>
    public partial class ResultPage : Page
    {
        public ResultPage(MainWindow m)
        {
            InitialSetting(m);
            InitializeComponent();
            SearchBox.Text = Placeholder;
            if (_mainWindow.GetCurrentUser != null)
            {
                (this.DataContext as Budget).Family.Remove(_mainWindow.GetCurrentUser);
                (this.DataContext as Budget).Family.Add(_mainWindow.GetCurrentUser);
            }                       
        }

        private void InitialSetting(MainWindow m)
        {
            _mainWindow = m;
            this.DataContext = m.DataContext;
            //_mainWindow.GetBudget = GetBudget;
            (this.DataContext as Budget).calc_incomes();
            (this.DataContext as Budget).calc_outcomes();
            (this.DataContext as Budget).calc_total();
            
        }

        private const string Placeholder = "Введите имя пользователя";
        private MainWindow _mainWindow;
        private int _loadcount;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {            
            if (_mainWindow.GetCurrentUser != null)
            {                
                ButtonCount.Visibility=Visibility.Visible;
            }            
            _mainWindow.Width = this.MinWidth;
            _mainWindow.Height = this.MinHeight;
           
            _loadcount = 0;
        }
        
        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            int count = (this.DataContext as Budget).Family.Count;
            if((sender as Button).DataContext is Income) 
                for (int i = 0; i < count; ++i)
                {
                    if ((this.DataContext as Budget).Family[i].Incomes.Remove((sender as Button).DataContext as Income))
                    {
                        (this.DataContext as Budget).calc_incomes();
                        (this.DataContext as Budget).calc_total();
                        break;
                    }
                }
            else
            {
                for (int i = 0; i < count; ++i)
                {
                    if ((this.DataContext as Budget).Family[i].Outcomes.Remove((sender as Button).DataContext as Outcome))
                    {
                        (this.DataContext as Budget).calc_outcomes();
                        (this.DataContext as Budget).calc_total();
                        break;
                    }
                }
            }
        }
        
        
        
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string nameText=(((sender as Button).Parent as Grid).Children[0] as TextBox).Text;
            string priceText= (((sender as Button).Parent as Grid).Children[1] as TextBox).Text;
            if (!(((sender as Button).Parent as Grid).Children[2] as DatePicker).SelectedDate.HasValue)
                MessageBox.Show("Выберите дату", "Некорректный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
            else                                        
            {               
                if (check_text(nameText, "Заполните название дохода!") &&
                    check_price(priceText, "В поле цена должно быть вещественное значение!"))
                    foreach (var user in (this.DataContext as Budget).Family)
                    {
                        if (user == (sender as Button).DataContext)
                        {
                            user.Incomes.Add(new Income(nameText, double.Parse(priceText),
                                (((sender as Button).Parent as Grid).Children[2] as DatePicker).SelectedDate.Value));
                            (this.DataContext as Budget).calc_incomes();
                            (this.DataContext as Budget).calc_total();
                        }
                    }
            }
            foreach (var child in ((sender as Button).Parent as Grid).Children)
            {
                if(child is TextBox)
                    (child as TextBox).Clear();
                if (child is DatePicker)
                {
                    (child as DatePicker).SelectedDate = null;
                    (child as DatePicker).DisplayDate = DateTime.Today;
                }
            }
            
        }

        private void ButtonBase1_OnClick(object sender, RoutedEventArgs e)
        {
            string nameText = (((sender as Button).Parent as Grid).Children[0] as TextBox).Text;
            string priceText = (((sender as Button).Parent as Grid).Children[1] as TextBox).Text;
            if (!(((sender as Button).Parent as Grid).Children[2] as DatePicker).SelectedDate.HasValue)
                MessageBox.Show("Выберите дату", "Некорректный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                if (check_text(nameText, "Заполните название расхода!") &&
                    check_price(priceText, "В поле цена должно быть вещественное значение!"))
                    foreach (var user in (this.DataContext as Budget).Family)
                    {
                        if (user == (sender as Button).DataContext)
                        {
                            user.Outcomes.Add(new Outcome(nameText, double.Parse(priceText),
                                (((sender as Button).Parent as Grid).Children[2] as DatePicker).SelectedDate.Value));
                            (this.DataContext as Budget).calc_outcomes();
                            (this.DataContext as Budget).calc_total();
                        }
                    }
            }
            foreach (var child in ((sender as Button).Parent as Grid).Children)
            {
                if (child is TextBox)
                    (child as TextBox).Clear();
                if (child is DatePicker)
                {
                    (child as DatePicker).SelectedDate = null;
                    (child as DatePicker).DisplayDate = DateTime.Today;
                }
            }
        }
        
        private void ButtonCount_OnClick(object sender, RoutedEventArgs e)
        {
            _mainWindow.CurrentBudget = (this.DataContext as Budget);
            UserInfo p = new UserInfo { Owner = Window.GetWindow(this)};
            p.ShowDialog();
        }

        private void SearchBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            if ((sender as TextBox).Text.Equals(Placeholder))
            {
                (sender as TextBox).Text = string.Empty;
            }
        }

        private void SearchBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            base.OnLostFocus(e);

            if ((sender as TextBox).Text.Equals(string.Empty))
            {
                (sender as TextBox).Text= Placeholder;
            }
        }

        private void SearchBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            string text = (sender as TextBox).Text;
            if (text.Equals(Placeholder) == false)
            {
                List<User> _list = new List<User>();
                foreach (var user in (_mainWindow.DataContext as Budget).Family)
                {
                    if (user.Name.Contains(text))
                        _list.Add(user);
                }
                ListView.ItemsSource = _list;
            }
            //  (this.DataContext as Budget).Family =searched;

        }
               
        delegate void CalcOperation();
        private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
        {
            CalcOperation calc;
            if(((sender as TextBox).Parent as Grid).DataContext is Income)
                calc=(DataContext as Budget).calc_incomes;
            else
            {
                calc = (DataContext as Budget).calc_outcomes;
            }
            if (int.TryParse((sender as TextBox).Text, out int i) && i > 0)
            {
                calc.Invoke();
                (DataContext as Budget).calc_total();
            }
            else
            {
                
                check_text(null, "Размер дохода должен быть целым числом !");
            }
        }

        
        

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (((sender as FrameworkElement).Parent as Grid).DataContext is User)
            {
                if (!(((sender as FrameworkElement).Parent as Grid).DataContext as User).Equals(_mainWindow
                    .GetCurrentUser))
                {
                    Grid a = (Grid) ((sender as FrameworkElement).Parent as Grid).Parent;
                    foreach (var children in a.Children)
                    {
                        (children as UIElement).IsEnabled = false;
                    }
                }
            }
            //if (!(((sender as FrameworkElement).Parent as Grid).DataContext as Income).Equals(_mainWindow.GetCurrentUser)
            //)
            //    (sender as FrameworkElement).IsEnabled = false;
        }
    }
}
