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

namespace metallica_client
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        MetallicaService.MetallicaServiceClient clientService;
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            clientService = new MetallicaService.MetallicaServiceClient();
            User user = new User();
            user.UserName = unameBox.Text;
            user.Password = passBox.Password;
            user =clientService.Login(user);
            if(user != null)
            {
                UserNav mainWindow = new UserNav(user);
                mainWindow.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("wrong info");
            }
        }
    }
}
