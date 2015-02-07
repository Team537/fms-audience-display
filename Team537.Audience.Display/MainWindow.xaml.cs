using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using Team537.Audience.Display.DataModel;
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
            viewModel.TotalTime = 150;
            viewModel.PlaySoundHandler += (s, e) =>
            {
                this.PlaySound(e.SoundEffect);
            };

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
                case Key.F3:
                    this.PlaySound(SoundEffect.StartAuto);
                    break;
                case Key.F4:
                    this.PlaySound(SoundEffect.StartTele);
                    break;
                case Key.F5:
                    this.PlaySound(SoundEffect.Warning1);
                    break;
                case Key.F6:
                    this.PlaySound(SoundEffect.Warning2);
                    break;
                case Key.F7:
                    this.PlaySound(SoundEffect.EndMatch);
                    break;
                case Key.F8:
                    this.PlaySound(SoundEffect.CancelMatch);
                    break;
            }            
        }

        private void PlaySound(SoundEffect sound)
        {
            var soundPlayer = new SoundPlayer();
            switch(sound)
            {
                case SoundEffect.StartAuto:
                    soundPlayer.SoundLocation = "Sounds/CHARGE.wav";
                    break;
                case SoundEffect.StartTele:
                    soundPlayer.SoundLocation = "Sounds/3BELLS.wav";
                    break;
                case SoundEffect.Warning1:
                    soundPlayer.SoundLocation = "Sounds/TimeToRun.wav";
                    break;
                case SoundEffect.Warning2:
                    soundPlayer.SoundLocation = "Sounds/SHUTDOWN.wav";
                    break;
                case SoundEffect.EndMatch:
                    soundPlayer.SoundLocation = "Sounds/ENDMATCH.wav";
                    break;
                case SoundEffect.CancelMatch:
                    soundPlayer.SoundLocation = "Sounds/FOGHORN.wav";
                    break;
            }
            soundPlayer.Play();
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
