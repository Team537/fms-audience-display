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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Team537.Audience.Display.ViewModel;

namespace Team537.Audience.Display
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel;
        private FmsListener fmsListener;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainWindowViewModel();
            fmsListener = new FmsListener();

            this.DataContext = viewModel;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F11:
                    WindowStyle = WindowStyle.None;
                    WindowState = WindowState.Maximized;
                    ResizeMode = ResizeMode.NoResize;
                    break;
                case Key.Escape:
                    WindowStyle = WindowStyle.SingleBorderWindow;
                    WindowState = WindowState.Normal;
                    ResizeMode = ResizeMode.CanResizeWithGrip;
                    break;
            }            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fmsListener.Start(this.viewModel);
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            fmsListener.Stop();
        }
    }
}
