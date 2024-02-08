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
    /// Interaction logic for SongUserControl.xaml
    /// </summary>
    public partial class SongUserControl : UserControl
    {
        Song song;
        User user;
        MetallicaService.MetallicaServiceClient clientService;

        public SongUserControl(Song csong, User cuser)
        {
            InitializeComponent();
            song = csong;
            user = cuser;
            song.Lyrics = song.Lyrics.ToLower();
            this.DataContext = song;
        }

        private void submitRating(object sender, RoutedEventArgs e)
        {
            clientService = new MetallicaService.MetallicaServiceClient();
            double rating = BasicRatingBar.Value;
            int answer = clientService.DeleteSongRating(user.ID, song.ID);
            answer = clientService.InsertSongRating(user.ID, song.ID, (int)rating);
        }
        private void BasicRatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }
    }
}
