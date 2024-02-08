using MaterialDesignThemes.Wpf.Transitions;
using metallica_client.MetallicaService;
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

namespace metallica_client
{
    /// <summary>
    /// Interaction logic for ShowUserControl.xaml
    /// </summary>
    public partial class ShowUserControl : UserControl
    {
        string url;
        public ShowUserControl(Show show)
        {
            InitializeComponent();
            show.ShowName = show.ShowName.ToLower();
            this.DataContext = show;
            string text = $"{show.City}, {show.Country} at {show.StadiumName}";
            location.Text = text;
            url = show.Url;
        }

        private void GoToSite(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }
    }
}
