using metallica_client.MetallicaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
    /// Interaction logic for AlbumsUserControl.xaml
    /// </summary>
    public partial class AlbumsUserControl : UserControl
    {
        double albumRating = 0;
        const int KILL_EM_ALL = 1;
        const int RIDE_THE_LIGHTNING = 2;
        const int MASTER_OF_PUPPETS = 3;
        const int AND_JUSTICE_FOR_ALL = 4;
        const int THE_BLACK_ALBUM = 5;
        const int LOAD = 6;
        const int RELOAD = 7;
        const int ST_ANGER = 8;
        const int DEATH_MAGNETIC = 9;
        const int HARDWIRED_TO_SELF_DESTRUCT = 10;
        const int SEVENTY_TWO_SEASONS = 11;

        Album album;
        User user;
        UserNav userNav;
        MetallicaService.MetallicaServiceClient clientService;


        public AlbumsUserControl(Album cAlbum, User cuser, UserNav cuserNav)
        {
            InitializeComponent();
            album = cAlbum;
            user = cuser;
            userNav = cuserNav;
            DataContext = album;
            setup();
        }
        private void setup()
        {
            clientService = new MetallicaService.MetallicaServiceClient();
            string Path = "pack://application:,,,../pictures/AlbumCovers/" + album.ID.ToString()+".jpeg";
            imgCover.Source = new BitmapImage(new Uri(Path));
            double avg = clientService.GetAlbumRating(album.ID);
            int userRating = clientService.GetAlbumRatingByUser(album.ID, user.ID);
            if(userRating > 0) { BasicRatingBar.Value = userRating; }
            else { BasicRatingBar.Value = 0; }
            ReadOnlyRatingBar.Value = avg;
        }

        private void BasicRatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }

        private void submitRating(object sender, RoutedEventArgs e)
        {
            clientService = new MetallicaService.MetallicaServiceClient();
            double rating = BasicRatingBar.Value;
            int answer = clientService.DeleteAlbumRating(user.ID, album.ID);
            answer = clientService.InsertAlbumRating(user.ID, album.ID, (int)rating);
        }

        private void ToSongs(object sender, RoutedEventArgs e)
        {
            userNav.Controls.Children.Clear();
            clientService = new MetallicaService.MetallicaServiceClient();
            SongList songList = new SongList();
            songList = clientService.GetAllSongsFromAlbum(album.ID);
            foreach (Song song in songList)
            {
                userNav.showSongs(song);
            }
        }
    }
}
