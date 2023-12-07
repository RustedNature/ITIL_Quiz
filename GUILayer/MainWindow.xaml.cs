using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace GuiLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Frame Frame;
        public MainWindow()
        {

            InitializeComponent();
            Frame = mainFrame;
            mainFrame.Navigate(new Uri("ChooseOptionsPage.xaml", UriKind.Relative));

        }
        private void MainFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                // Cancel the navigation if it's a backward navigation
                e.Cancel = true;
            }
        }

    }
}