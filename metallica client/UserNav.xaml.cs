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
using System.Windows.Shapes;
using System.Threading;

namespace metallica_client
{
    /// <summary>
    /// Interaction logic for UserNav.xaml
    /// </summary>
    public partial class UserNav : Window
    {
        MetallicaService.MetallicaServiceClient clientService;
        User curruser;
        private static Mutex ChatMutex = new Mutex();

        public UserNav(User user)
        {
            InitializeComponent();
            curruser = user;
            if (curruser.Accesslevel == 1)
            {
                CreateAdminButton();
            }
        }

        private void Albums_Selected(object sender, RoutedEventArgs e)
        {
            List<AlbumsUserControl> albumsUserControls = new List<AlbumsUserControl>();
            Controls.Children.Clear();

            messageBarControl.Children.Clear();

            clientService = new MetallicaService.MetallicaServiceClient();
            AlbumList albums = clientService.GetAllAlbums();
            foreach (Album album in albums)
            {
                AlbumsUserControl AUC = new AlbumsUserControl(album, curruser, this);
                albumsUserControls.Add(AUC);                
                Controls.Children.Add(AUC);
            }

        }

        private void AddChat_Selected(object sender, RoutedEventArgs e)
        {
            Controls.Children.Clear();
            messageBarControl.Children.Clear();
            Controls.Children.Add(new ChatCreatorUserControl(curruser));
        }

        private void Chats_Selected(object sender, RoutedEventArgs e)
        {
            Controls.Children.Clear();
            messageBarControl.Children.Clear();

            clientService = new MetallicaService.MetallicaServiceClient();
            ChatList chats = clientService.GetAllChats();
            foreach (Chat chat in chats)
            {
                ChatListUserControl CLUC = new ChatListUserControl(chat, this);
                Controls.Children.Add(CLUC);
            }
        }

        public void ShowChat(Chat chat)
        {
            Controls.Children.Clear();
            messageBarControl.Children.Clear();
            clientService = new MetallicaService.MetallicaServiceClient();
            MessagesList messages = clientService.GetMessagesOfChat(chat.ID);
            foreach (Messages message in messages)
            {
                if (message.UserId == curruser.ID)
                {
                    GreenMessage msg = new GreenMessage(message);
                    msg.Margin = new Thickness(5);
                    msg.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    Controls.Children.Add(msg);
                }
                else
                {
                    GrayMessage msg = new GrayMessage(message);
                    msg.Margin = new Thickness(5);
                    msg.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    Controls.Children.Add(msg);

                }
            }
            MessageBar bar = new MessageBar(curruser, chat, this);
            Controls.Children.Add(bar);
        }

        public void showSongs(Song song)
        {
            SongUserControl SUC = new SongUserControl(song, curruser);
            Controls.Children.Add(SUC);
        }

        private void Show_Shows_Selected(object sender, RoutedEventArgs e)
        {
            Controls.Children.Clear();
            messageBarControl.Children.Clear();

            clientService = new MetallicaService.MetallicaServiceClient();
            ShowList shows = clientService.GetAllShows();
            foreach (Show show in shows)
            {
                ShowUserControl SUC = new ShowUserControl(show);
                Controls.Children.Add(SUC);
            }
        }
        private void ShowAdminData(object sender, RoutedEventArgs e)
        {
            AdminShowData();
        }
        public void AdminShowData()
        {
            Controls.Children.Clear();
            messageBarControl.Children.Clear();

            clientService = new MetallicaService.MetallicaServiceClient();
            UserList users = clientService.GetAllUser();
            AdminUserTableUserControl userTable = new AdminUserTableUserControl(this, users);
            ChatList chats = clientService.GetAllChats();
            AdminChatTableUserControl chatTable = new AdminChatTableUserControl(this, chats);
            Controls.Children.Add(userTable);
            Controls.Children.Add(chatTable);
        }
        public void CreateAdminButton()
        {
            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Horizontal;
            panel.Margin = new Thickness(10, 0, 0, 0);

            MaterialDesignThemes.Wpf.PackIcon icon = new MaterialDesignThemes.Wpf.PackIcon();
            icon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Administrator;
            icon.Width = 20;
            icon.Height = 20;
            icon.Foreground = Brushes.LightSteelBlue;
            icon.Margin = new Thickness(5);
            icon.VerticalAlignment = VerticalAlignment.Center;
            icon.FontFamily = new FontFamily("/fonts/#Metallica");
            panel.Children.Add(icon);

            TextBlock textBlock = new TextBlock();
            textBlock.Text = "admin";
            textBlock.Foreground = Brushes.White;
            textBlock.Margin = new Thickness(10);
            textBlock.FontWeight = FontWeights.Bold;
            panel.Children.Add(textBlock);

            ListViewItem listItem = new ListViewItem();
            listItem.Height = 45;
            listItem.Padding = new Thickness(0);
            listItem.Selected += ShowAdminData;
            listItem.Content = panel;

            buttonList.Items.Add(listItem);
        }
    }
}
