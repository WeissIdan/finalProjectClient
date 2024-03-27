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
    /// Interaction logic for AdminChatTableRow.xaml
    /// </summary>
    public partial class AdminChatTableRow : UserControl
    {
        Chat chat;
        UserNav userNav;
        MetallicaService.MetallicaServiceClient clientService;
        public AdminChatTableRow(UserNav nav, Chat chat)
        {
            InitializeComponent();
            this.chat = chat;
            this.userNav = nav;
            this.DataContext = chat;
        }
        private void RemoveChat(object sender, RoutedEventArgs e)
        {
            clientService = new MetallicaServiceClient();

            clientService.DeleteMessagesByChat(chat);
            int rowsAffected = clientService.DeleteChat(chat);

            MessageBox.Show("Affected rows: " + rowsAffected);
            userNav.AdminShowData();
        }
    }
}
