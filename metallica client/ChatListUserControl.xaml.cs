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
    /// Interaction logic for ChatListUserControl.xaml
    /// </summary>
    public partial class ChatListUserControl : UserControl
    {
        Chat chat;
        UserNav parent;
        public ChatListUserControl(Chat cChat, UserNav parentWindow)
        {
            InitializeComponent();
            chat = cChat;
            parent = parentWindow;
            this.DataContext = chat;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            parent.ShowChat(chat);
        }
    }
}
