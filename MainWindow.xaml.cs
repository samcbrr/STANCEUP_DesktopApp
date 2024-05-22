using System.Windows;
using System.Windows.Controls;

namespace stanceup_new
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void sidebar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = sidebar.SelectedItem as NavButton;

            navframe.Navigate(selected.NavLink);

        }

        private void btnclick_close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
