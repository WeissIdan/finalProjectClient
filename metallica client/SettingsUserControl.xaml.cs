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
    /// Interaction logic for SettingsUserControl.xaml
    /// </summary>
    public partial class SettingsUserControl : UserControl
    {
        User cUser;
        public SettingsUserControl(User user)
        {
            InitializeComponent();
            this.DataContext = user;
            cUser = user;
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            MetallicaService.MetallicaServiceClient clientService = new MetallicaServiceClient();
            User newUser = cUser;
            newUser.FirstName = FirstNameTB.Text;
            newUser.LastName = LastNameTB.Text;
            newUser.UserName = UserNameTB.Text;
            newUser.Password = PasswordTB.Text;
            clientService.UpdateUser(newUser);
        }
    }
}
