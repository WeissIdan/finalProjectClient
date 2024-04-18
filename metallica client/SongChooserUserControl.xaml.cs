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
    /// Interaction logic for SongChooserUserControl.xaml
    /// </summary>
    public partial class SongChooserUserControl : UserControl
    {
        SongList ByGenre = new SongList();
        SongList ByPace = new SongList();
        SongList ByKnowlege = new SongList();
        //SongList ByAlbum;
        SongList Final;
        CategoryList Genres = new CategoryList();
        CategoryList Paces = new CategoryList();
        CategoryList Knowleges = new CategoryList();
        CategoryList Categories;
        MetallicaService.MetallicaServiceClient clientService;
        UserNav userNav;

        public SongChooserUserControl(UserNav userNav)
        {
            InitializeComponent();
            InsertCategories();
            this.userNav = userNav;
        }
        private void InsertCategories()
        {
            clientService = new MetallicaService.MetallicaServiceClient();
            Categories = clientService.GetAllCategories();
            foreach(Category category in Categories)
            {
                if(category.CategoryType == "Genre")
                {
                    Genres.Add(category);
                }
                if(category.CategoryType == "Pace")
                {
                    Paces.Add(category);
                }
                if(category.CategoryType == "BandKnowledge")
                {
                    Knowleges.Add(category);
                }
            }
            GenreCB.ItemsSource = Genres;
            GenreCB.DisplayMemberPath = "CategoryName";
            PaceCB.ItemsSource = Paces;
            PaceCB.DisplayMemberPath = "CategoryName";
            BandKnowlegeCB.ItemsSource = Knowleges;
            BandKnowlegeCB.DisplayMemberPath = "CategoryName";
        }

        private void Request(object sender, RoutedEventArgs e)
        {
            clientService = new MetallicaService.MetallicaServiceClient();

            Category genre = GenreCB.SelectedValue as Category;
            Category pace = PaceCB.SelectedValue as Category;
            Category knowledge = BandKnowlegeCB.SelectedValue as Category;
            ByGenre = clientService.GetSongsByCategory(genre);
            ByKnowlege = clientService.GetSongsByCategory(knowledge);
            ByPace = clientService.GetSongsByCategory(pace);
            foreach(Song song in ByGenre)
            {
                if(ByPace.Contains(song) && ByKnowlege.Contains(song))
                {
                    Final.Add(song);
                }
            }
            userNav.Controls.Children.Clear();
            foreach(Song song in Final)
            {
                userNav.showSongs(song);
            }

        }
    }

}
