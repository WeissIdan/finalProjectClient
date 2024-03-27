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
    /// Interaction logic for AdminUserTableRow.xaml
    /// </summary>
    public partial class AdminUserTableRow : UserControl
    {
        User user;
        UserNav userNav;
        MetallicaService.MetallicaServiceClient clientService;
        public AdminUserTableRow(User user, UserNav nav)
        {
            InitializeComponent();
            this.user = user;
            this.userNav = nav;
            this.DataContext = user;
        }

        private void RemoveUser(object sender, RoutedEventArgs e)
        {
            clientService = new MetallicaServiceClient();
            int rowsAffected = clientService.DeleteUser(user);
            MessageBox.Show("Affected rows: " + rowsAffected);
            userNav.AdminShowData();
        }
    }
}
