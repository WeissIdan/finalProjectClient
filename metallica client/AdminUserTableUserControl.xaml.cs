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
    /// Interaction logic for AdminUserTableUserControl.xaml
    /// </summary>
    public partial class AdminUserTableUserControl : UserControl
    {

        public AdminUserTableUserControl(UserNav nav, UserList users)
        {
            InitializeComponent();
            CreateTable(users, nav);
        }

        void CreateTable(UserList users, UserNav nav)
        {
            foreach(User user in users)
            {
                if(user.Accesslevel == 0)
                {
                    AdminUserTableRow row = new AdminUserTableRow(user, nav);
                    table.Children.Add(row);
                }
            }
        }
    }
}
