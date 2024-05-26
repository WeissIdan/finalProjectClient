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
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        const int ADMIN_ACCESS_LEVEL = 1;
        const int USER_ACCESS_LEVEL = 0;

        MetallicaService.MetallicaServiceClient clientService;
        User newUser = new User();
        public SignUp()
        {
            InitializeComponent();
            addDates();
            DataContext = newUser;
            
        }
        private void addDates()
        {
            DayComboBox.Items.Clear();
            MonthComboBox.Items.Clear();
            YearComboBox.Items.Clear();
            for (int i = 1907; i < DateTime.Now.Year; i++)
            {
                YearComboBox.Items.Add(i);
            }              
            for (int i = 1; i < 13; i++)
            {
                MonthComboBox.Items.Add(i);
            }                   
            for (int i = 1; i < 32; i++)
            {
                DayComboBox.Items.Add(i);
            }         
        }   
        private void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckValidationErrors())
            {
                clientService = new MetallicaServiceClient();
                newUser.UserName = UsernameTextBox.Text;
                newUser.FirstName = FirstNameTextBox.Text;
                newUser.Accesslevel = USER_ACCESS_LEVEL;
                newUser.LastName = LastNameTextBox.Text;
                newUser.Password = PasswordBox.Password;
                newUser.Email = EmailTextBox.Text;
                if (MaleRadioButton.IsChecked == true) newUser.IsMale = true;
                else newUser.IsMale = false;
                int day = int.Parse(DayComboBox.Text);
                int month = int.Parse(MonthComboBox.Text);
                int year = int.Parse(YearComboBox.Text);
                DateTime time = new DateTime(year, month, day);
                newUser.Birthdate = time;
                clientService.InsertUser(newUser);
                Login login = new Login();
                login.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("please fill in evertyrhing correctly!");
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ValidationPassword valid = new ValidationPassword();
            ValidationResult result = valid.Validate(PasswordBox.Password, null);
            if(!result.IsValid)
            {
                PasswordBox.BorderBrush = Brushes.DarkRed;
                PasswordBox.BorderThickness = new Thickness(2);
                PasswordBox.ToolTip = result.ErrorContent;
                PasswordBox.FontFamily = new FontFamily("/fonts/#Metallica");
            }
            else
            {
                PasswordBox.BorderThickness = new Thickness(0);
                PasswordBox.ToolTip = null;
            }
        }

        private bool CheckValidationErrors()
        {
            return (Validation.GetHasError(FirstNameTextBox) || Validation.GetHasError(LastNameTextBox) || Validation.GetHasError(EmailTextBox) || Validation.GetHasError(UsernameTextBox) || Validation.GetHasError(PasswordBox));

        }
    }
}
