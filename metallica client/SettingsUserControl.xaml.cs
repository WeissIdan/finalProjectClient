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
            if (!CheckValidationErrors())
            {
                MetallicaService.MetallicaServiceClient clientService = new MetallicaServiceClient();
                User newUser = cUser;
                newUser.FirstName = FirstNameTextBox.Text;
                newUser.LastName = LastNameTextBox.Text;
                newUser.UserName = UsernameTextBox.Text;
                newUser.Password = PasswordBox.Password;
                clientService.UpdateUser(newUser);
            }
            else
            {
                MessageBox.Show("please fill the informaion correctly");
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ValidationPassword valid = new ValidationPassword();
            ValidationResult result = valid.Validate(PasswordBox.Password, null);
            if (!result.IsValid)
            {
                PasswordBox.BorderBrush = Brushes.DarkRed;
                PasswordBox.BorderThickness = new Thickness(2);
                PasswordBox.ToolTip = result.ErrorContent;
            }
            else
            {
                PasswordBox.BorderThickness = new Thickness(0);
                PasswordBox.ToolTip = null;
            }
        }
        private bool CheckValidationErrors()
        {
            return (Validation.GetHasError(FirstNameTextBox) || Validation.GetHasError(LastNameTextBox) || Validation.GetHasError(UsernameTextBox) || Validation.GetHasError(PasswordBox));

        }
    }
}
