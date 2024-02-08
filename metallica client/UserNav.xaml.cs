﻿using metallica_client.MetallicaService;
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

namespace metallica_client
{
    /// <summary>
    /// Interaction logic for UserNav.xaml
    /// </summary>
    public partial class UserNav : Window
    {
        MetallicaService.MetallicaServiceClient clientService;
        User curruser;
        public UserNav(User user)
        {
            InitializeComponent();
            curruser = user;
        }

        private void Albums_Selected(object sender, RoutedEventArgs e)
        {
            Controls.Children.Clear();
            clientService = new MetallicaService.MetallicaServiceClient();
            AlbumList albums = clientService.GetAllAlbums();
            foreach (Album album in albums)
            {
                AlbumsUserControl AUC = new AlbumsUserControl(album, curruser, this);
                Controls.Children.Add(AUC);
            }

        }

        private void AddChat_Selected(object sender, RoutedEventArgs e)
        {
            Controls.Children.Clear();
            Controls.Children.Add(new ChatCreatorUserControl(curruser));
        }

        private void Chats_Selected(object sender, RoutedEventArgs e)
        {
            Controls.Children.Clear();
            clientService = new MetallicaService.MetallicaServiceClient();
            ChatList chats = clientService.GetAllChats();
            foreach(Chat chat in chats)
            {
                ChatListUserControl CLUC = new ChatListUserControl(chat, this);
                Controls.Children.Add(CLUC);
            }
        }

        public void ShowChat(Chat chat)
        {
            Controls.Children.Clear();
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
            clientService = new MetallicaService.MetallicaServiceClient();
            ShowList shows = clientService.GetAllShows();
            foreach(Show show in shows)
            {
                ShowUserControl SUC = new ShowUserControl(show);
                Controls.Children.Add(SUC);
            }
        }
    }
}