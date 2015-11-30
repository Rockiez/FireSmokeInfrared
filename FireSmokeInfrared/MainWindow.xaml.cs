using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ICS.Acquisition;
using ICS.Common;
using ICS.Models.Com;

namespace FireSmokeInfrared
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var timer = new Timer(_sender =>
                {
                var equipment = new ADAM4150(new ComSettingModel());
                    if (equipment.CheckSerialPort(equipment.ADAM4017Provider).Status == RunStatus.Success)
                    {
                        equipment.SetData();
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Fire.Background = equipment.fireValue ? Brushes.Tomato : Brushes.CornflowerBlue;
                            Smoke.Background = equipment.smokeValue ? Brushes.Tomato : Brushes.CornflowerBlue;
                            BodyInfrared.Background = equipment.bodyInfraredValue
                                ? Brushes.Tomato
                                : Brushes.CornflowerBlue;

                        });
                    }
                },null, 100, 1000);
            }
        

        private void closeBotton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void minBotton(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
