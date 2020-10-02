using AudioSwitcher.AudioApi.CoreAudio;
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

namespace VolumeController
{
    /// <summary>
    /// Логика взаимодействия для VolumeControl.xaml
    /// </summary>
    public partial class VolumeControl : UserControl
    {
        public static readonly DependencyProperty DefaultVolumeProperty = DependencyProperty.RegisterAttached("DefaultVolume", typeof(int), typeof(VolumeControl), new FrameworkPropertyMetadata(50, OnVolumeChanged));
        public int DefaultVolume
        {
            get { return Convert.ToInt32(GetValue(DefaultVolumeProperty)); }
            set { SetValue(DefaultVolumeProperty, value); }
        }

        public VolumeControl()
        {
            InitializeComponent();
            CoreAudioController coreAudioConteroller = new CoreAudioController();
            App.CoreAudioDevice = coreAudioConteroller.GetDefaultDevice(AudioSwitcher.AudioApi.DeviceType.Playback, AudioSwitcher.AudioApi.Role.Multimedia);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            App.CoreAudioDevice.Volume = e.NewValue;
        }

        private static void OnVolumeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            App.CoreAudioDevice.Volume = Convert.ToInt32(e.NewValue);
        }
    }
}
