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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace metallica_client
{
    /// <summary>
    /// Interaction logic for MessageBar.xaml
    /// </summary>
    public partial class MessageBar : UserControl
    {
        MetallicaService.MetallicaServiceClient clientService;
        Chat chat;
        User user;
        UserNav nav;
        public MessageBar(User cuser, Chat cchat, UserNav cnav)
        {
            InitializeComponent();
            user = cuser;
            chat = cchat;
            nav = cnav;
        }
        private void enterPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                send();
            }
        }


        private void SendMessage(object sender, RoutedEventArgs e)
        {
            send();
        }

        private void send()
        {
            clientService = new MetallicaService.MetallicaServiceClient();
            string msg = message.Text;
            if (msg != null)
            {
                Messages newMsg = new Messages();
                newMsg.Message = msg;
                newMsg.UserId = user.ID;
                newMsg.ChatId = chat.ID;
                clientService.InsertMessage(newMsg);
                message.Text = "";
                nav.ShowChat(chat);
            }
        }
        private void txtSearch_TextChanged(Object sender, TextChangedEventArgs e)

        {

            if (message.Text != "")
            {
                txtSearchPlaceholder.Visibility = Visibility.Hidden;
            }
            else
            {
                txtSearchPlaceholder.Visibility = Visibility.Visible;
            }
        }
    }
}
