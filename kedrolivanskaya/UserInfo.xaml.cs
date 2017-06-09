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

namespace kedrolivanskaya
{
    /// <summary>
    /// Логика взаимодействия для UserInfo.xaml
    /// </summary>
    public partial class UserInfo : Window
    {
        public UserInfo()
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
            _currUser = ((this.Owner as MainWindow).DataContext as Budget).Family.First(
                x => x.Name == (this.Owner as MainWindow).GetCurrentUser.Name
                     && x.Surname == (this.Owner as MainWindow).GetCurrentUser.Surname);
            DatePickerStart.SelectedDate = DateTime.Now;
            DatePickerEnd.SelectedDate = DateTime.Now;
        }        
        private User _currUser;
        private void DatePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {            
                if ((sender as DatePicker).SelectedDate.HasValue)
                if ((sender as DatePicker).SelectedDate <= DatePickerEnd.SelectedDate)
                {
                    IncomesBlcok.Text = _currUser.Incomes
                        .Where(x => x.Date > (sender as DatePicker).SelectedDate &&
                                    x.Date <= DatePickerEnd.SelectedDate).Sum(x => x.Price).ToString();
                    OutcomesBlcok.Text = _currUser.Outcomes
                        .Where(x => x.Date > (sender as DatePicker).SelectedDate &&
                                    x.Date <= DatePickerEnd.SelectedDate).Sum(x => x.Price).ToString();
                    calc_sum();
                }

        }

        private void calc_sum()
        {
            double tempIncome;
            double tempOutcome;
            if (double.TryParse(IncomesBlcok.Text, out tempIncome) &&
                double.TryParse(OutcomesBlcok.Text, out tempOutcome))
                TotalBlcok.Text = (tempIncome - tempOutcome).ToString();
        }
        private void DatePicker1_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {         
            if ((sender as DatePicker).SelectedDate.HasValue)
                if ((sender as DatePicker).SelectedDate >= DatePickerStart.SelectedDate)
                {
                    IncomesBlcok.Text = _currUser.Incomes
                        .Where(x => x.Date <= (sender as DatePicker).SelectedDate &&
                                    x.Date > DatePickerStart.SelectedDate).Sum(x => x.Price).ToString();
                    OutcomesBlcok.Text = _currUser.Outcomes.Where(x => x.Date <= (sender as DatePicker).SelectedDate && x.Date > DatePickerStart.SelectedDate).Sum(x => x.Price).ToString();
                    calc_sum();
                }
        }
    }
}
