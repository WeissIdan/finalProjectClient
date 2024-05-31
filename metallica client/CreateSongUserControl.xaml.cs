using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
using metallica_client.MetallicaService;

namespace metallica_client
{

    /// <summary>
    /// Interaction logic for CreateSongUserControl.xaml
    /// </summary>
    public partial class CreateSongUserControl : UserControl
    {
        private MetallicaService.MetallicaServiceClient clientService;
        AlbumList albums;
        CategoryList Genres = new CategoryList();
        CategoryList Paces = new CategoryList();
        CategoryList Knowleges = new CategoryList();
        CategoryList Categories;

        public CreateSongUserControl()
        {
            InitializeComponent();
            clientService = new MetallicaService.MetallicaServiceClient();
            albums = clientService.GetAllAlbums();
            this.DataContext = albums;
            InsertCategories();
            foreach (Album album in albums)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.GroupName = "AlbumsButtons";
                radioButton.DataContext = album;
                radioButton.Content = album.AlbumName;
                radioList.Items.Add(radioButton);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var checkedValue = radioList.Items.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked.HasValue && r.IsChecked.Value);
            if(checkedValue == null) { MessageBox.Show("select album!"); return; }
            if(name.Text == string.Empty) { MessageBox.Show("enter name!"); return; }
            if(lyrics.Text == string.Empty) { MessageBox.Show("enter Lyrics!"); return; }
            if(pace.SelectedIndex == -1 || knowledge.SelectedIndex==-1 || genre.SelectedIndex == -1) { MessageBox.Show("select from all categories!"); return; };
            RadioButton radioButton = checkedValue as RadioButton;
            Album selected = radioButton.DataContext as Album;
            Song newSong = new Song();
            newSong.Lyrics = lyrics.Text;
            newSong.AlbumId = selected.ID;
            newSong.SongName = name.Text;
            clientService.InsertSong(newSong);
            int songId = clientService.GetLastSongId();
            SongCategory category1 = new SongCategory();
            SongCategory category2= new SongCategory();
            SongCategory category3 = new SongCategory();
            category1.CategoryID = (genre.SelectedValue as Category).ID;
            category2.CategoryID = (pace.SelectedValue as Category).ID;
            category3.CategoryID = (knowledge.SelectedValue as Category).ID;            
            category1.SongID= (songId);
            category2.SongID = songId;
            category3.SongID = songId;
            clientService.InsertSongCategory(category1);
            clientService.InsertSongCategory(category2);
            clientService.InsertSongCategory(category3);


        }
        private void InsertCategories()
        {
            clientService = new MetallicaService.MetallicaServiceClient();
            Categories = clientService.GetAllCategories();
            foreach (Category category in Categories)
            {
                if (category.CategoryType == "Genre")
                {
                    Genres.Add(category);
                }
                if (category.CategoryType == "Pace")
                {
                    Paces.Add(category);
                }
                if (category.CategoryType == "BandKnowledge")
                {
                    Knowleges.Add(category);
                }
            }
            genre.ItemsSource = Genres;
            genre.DisplayMemberPath = "CategoryName";
            pace.ItemsSource = Paces;
            pace.DisplayMemberPath = "CategoryName";
            knowledge.ItemsSource = Knowleges;
            knowledge.DisplayMemberPath = "CategoryName";
        }
    }
}
