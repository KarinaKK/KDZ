using System.Windows;
using System.Windows.Controls;

namespace kedrolivanskaya
{
    /// <summary>
    /// Логика взаимодействия для First_Page.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage(MainWindow mainWindow )
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _page=new ResultPage(mainWindow);
        }

        private ResultPage _page;
        private MainWindow _mainWindow;
        private void button_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainFrame.Content = _page;
        }
    }
}
