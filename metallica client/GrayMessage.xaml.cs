using metallica_client.MetallicaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
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
    /// Interaction logic for GrayMessage.xaml
    /// </summary>
    public partial class GrayMessage : UserControl
    {
        MetallicaService.MetallicaServiceClient clientService;
        public GrayMessage(Messages message)
        {
            InitializeComponent();
            this.DataContext = message;
            insertUserName(message.UserId);
        }
        public void insertUserName(int id)
        {
            clientService = new MetallicaService.MetallicaServiceClient();
            string userName = clientService.GetUser(id).UserName;
            user.Text = userName;
        }
    }
}
