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
    /// Interaction logic for CreateAlbumUserControl.xaml
    /// </summary>
    public partial class CreateAlbumUserControl : UserControl
    {
        public CreateAlbumUserControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MetallicaServiceClient clientService = new MetallicaService.MetallicaServiceClient();

            if (!datePicker.SelectedDate.HasValue) { MessageBox.Show("select date!"); return; }
            if(albumName.Text == string.Empty) { MessageBox.Show("enter name!"); return; }
            if(songAmount.Text == string.Empty) { MessageBox.Show("enter soung amount!"); return; }
            int.TryParse(songAmount.Text, out int n);
            if (n==0) { MessageBox.Show("enter a number!"); return; }
            Album newAlbum = new Album();
            newAlbum.AlbumName = albumName.Text;
            newAlbum.ReleaseDate = datePicker.SelectedDate.Value;
            newAlbum.SongAmount = int.Parse(songAmount.Text);
            clientService.InsertAlbum(newAlbum);
        }
    }
}
