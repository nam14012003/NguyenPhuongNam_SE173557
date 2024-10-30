using ConvenienceStore_BusinessObject;
using ConvenienceStore_Repository;
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

namespace NguyenPhuongNam_SE173557
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private IStoreAccountRepo _accountRepo;
        public LoginWindow()
        {
            InitializeComponent();
            _accountRepo = new StoreAccountRepo();
        }


        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            StoreAccount storeAccount = _accountRepo.GetStoreAccount(txtEmail.Text ,txtPassword.Text);
            if (storeAccount != null)
            {

                int? RoleID = storeAccount.Role;

                this.Hide();
                ProductWindow productWindow = new ProductWindow(RoleID);
                productWindow.Show();
            }
            else
            {
                MessageBox.Show("Bye");
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
