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
    /// Interaction logic for ChatCreatorUserControl.xaml
    /// </summary>
    public partial class ChatCreatorUserControl : UserControl
    {
        User user;
        MetallicaService.MetallicaServiceClient clientService;
        public ChatCreatorUserControl(User cUser)
        {
            InitializeComponent();
            user = cUser;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DateTime currTime = DateTime.Now;
            clientService = new MetallicaService.MetallicaServiceClient();
            Chat chat = new Chat();
            chat.ChatManager = user.ID;
            chat.CreationDate = currTime;
            chat.ChatName = chatName.Text;
            clientService.InsertChat(chat);
            MessageBox.Show("chat created!", "success creation", MessageBoxButton.OK, MessageBoxImage.Hand);
        }
    }
}
