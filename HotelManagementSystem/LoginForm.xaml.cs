using HotelManagementSystem.Context.Entities;
using HotelManagementSystem.Context;
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

namespace HotelManagementSystem
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        private HotelContext _context;

        public LoginForm()
        {
            InitializeComponent();
            _context = new HotelContext();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            Login login = _context.Logins.FirstOrDefault(l => l.Username == username && l.Password == password);

            if (login != null && login.LoginType == LoginType.clerk)
            {
                ReservationForm reservationForm = new ReservationForm();
                reservationForm.Show();
                this.Close();
            }
            else if (login != null && login.LoginType == LoginType.kitchen)
            {
                KitchenForm kitchenForm = new KitchenForm();
                kitchenForm.Show();
                this.Close();
            }
            else if (login != null)
            {
                MessageBox.Show("Login success");
            }
            else
            {
                MessageBox.Show("Login failure");
            }
        }

    }
}
