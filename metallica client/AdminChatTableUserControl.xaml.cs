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
    /// Interaction logic for AdminChatTableUserControl.xaml
    /// </summary>
    public partial class AdminChatTableUserControl : UserControl
    {
        public AdminChatTableUserControl(UserNav nav, ChatList chats)
        {
            InitializeComponent();
            createTable(nav, chats);
        }

        public void createTable(UserNav nav, ChatList chats)
        {
            foreach (Chat chat in chats)
            {
                AdminChatTableRow row = new AdminChatTableRow(nav, chat);
                table.Children.Add(row);
            }
        }

    }
}
