using ConvenienceStore_BusinessObject;
using ConvenienceStore_DAO;
using ConvenienceStore_Repository;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private int? Role = 0;
        private IProductRepo _productRepo;
        private IVendorRepo _vendorRepo;
        public ProductWindow()
        {
            InitializeComponent();
            _productRepo = new ProductRepo();
            _vendorRepo = new VendorRepo();
        }

        public ProductWindow(int ? RoleID)
        {
            InitializeComponent();
            _productRepo = new ProductRepo();
            _vendorRepo = new VendorRepo();
            this.Role = RoleID;
            switch (Role)
            {
                case 1:// Full quyền
                    break;

                case 2:// Don't add
                    this.btnAdd.IsEnabled = false;
                    this.btnDelete.IsEnabled = false;
                    this.btnUpdate.IsEnabled = false;
                    break;

                default:
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            this.dtgProducts.ItemsSource = _productRepo.GetAllProduct();
            this.cmbVendor.ItemsSource = _vendorRepo.GetVendor();
            this.cmbVendor.DisplayMemberPath = "VendorName";
            this.cmbVendor.SelectedValuePath = "VendorId";

            txtProductId.Text = "";
            txtProductName.Text = "";
            txtPrice.Text = "";
            txtQuantity.Text = "";
            txtDescription.Text = "";
            dpManufactureDate.Text = "";
            dpExpiryDate.Text = "";
        }


        private void dtgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (dtgProducts.SelectedItem is Product selectedProduct)
            {
                txtProductId.Text = selectedProduct.ProductId.ToString();
                txtProductName.Text = selectedProduct.ProductName;
                txtPrice.Text = selectedProduct.Price.ToString();
                txtQuantity.Text = selectedProduct.Quantity.ToString();
                txtDescription.Text = selectedProduct.Description.ToString();
                dpManufactureDate.Text = selectedProduct.ManufactureDate.ToString();
                dpExpiryDate.Text = selectedProduct.ExpiredDate.ToString();
                cmbVendor.SelectedValue = selectedProduct.VendorId;
            }
        }



        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int productId = int.Parse(txtProductId.Text);

            var product = _productRepo.GetProductProfileById(productId);
            if (product != null)
            {
                MessageBox.Show($"Sản Phẩm Với ID {productId} đã tồn tại.");
                return;
            }
            Product product1 = new Product();


            // Kiểm tra tên sản phẩm
            string productName = txtProductName.Text;

            if (productName.Length < 5 || productName.Length > 90)
            {
                MessageBox.Show("Tên sản phẩm phải từ 5 đến 90 ký tự.");
                return;
            }
            string[] words = productName.Split(' ');
            foreach (string word in words)
            {
                if (string.IsNullOrEmpty(word) || !char.IsUpper(word[0]))
                {
                    MessageBox.Show("Tên sản phẩm phải bắt đầu bằng chữ cái in hoa cho mỗi từ.");
                    return;
                }
            }
            product1.ProductName = productName;

            //Kiểm tra giá tiền
            if (decimal.TryParse(txtPrice.Text, out decimal price) && price > 0 && price < 10)
            {
                product1.Price = price;
            }
            else
            {
                MessageBox.Show("Giá sản phẩm phải lớn hơn 0 và nhỏ hơn 10$.");
                return;
            }

            //Kiểm tra số lượng
            if (int.TryParse(txtQuantity.Text, out int quantity) && quantity > 0 && quantity < 300)
            {
                product1.Quantity = quantity;
            }
            else
            {
                MessageBox.Show("Số lượng sản phẩm phải lớn hơn 0 và nhỏ hơn 300.");
                return;
            }


            product1.ProductId = int.Parse(txtProductId.Text);
            product1.Description = txtDescription.Text;
            product1.ManufactureDate = DateOnly.Parse(dpManufactureDate.Text);
            product1.ExpiredDate = DateOnly.Parse(dpExpiryDate.Text);
            product1.VendorId = int.Parse(cmbVendor.SelectedValue.ToString());

            if (_productRepo.AddNewProduct(product1))
            {
                MessageBox.Show(" Thêm Sản Phẩm Thành Công");
                this.ButtonClear_Click(sender, e);
                LoadData();
            }
            else
            {
                MessageBox.Show("Thêm Sản Phẩm Thất Bại");
            }
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int productId = int.Parse(txtProductId.Text);

            var product = _productRepo.GetProductProfileById(productId);
            if (product == null)
            {
                MessageBox.Show($"Sản Phẩm Với ID {productId} Không Tồn Tại.");
                return;
            }
            product.ProductName = txtProductName.Text;
            product.Price = decimal.Parse(txtPrice.Text);
            product.Quantity = int.Parse(txtQuantity.Text);
            product.Description = txtDescription.Text;
            product.ManufactureDate = DateOnly.Parse(dpManufactureDate.Text);
            product.ExpiredDate = DateOnly.Parse(dpExpiryDate.Text);
            product.VendorId = int.Parse(cmbVendor.SelectedValue.ToString());

            if (_productRepo.UpdateProductProfile(product))
            {
                MessageBox.Show("Cập Nhập Sản Phẩm Thành Công");
                this.ButtonClear_Click(sender, e);
                LoadData();
            }
            else
            {
                MessageBox.Show("Cập Nhật Sản Phẩm Thất Bại");
            }
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int productId = int.Parse(txtProductId.Text);


            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này không?",
                                          "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                if (productId > 0 && _productRepo.DeleteProductProfile(productId))
                {
                    MessageBox.Show("Xóa Sản Phẩm Thành Công");
                    this.ButtonClear_Click(sender, e);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Xóa Sản Phẩm Không Thành Công");
                }
            }
        }


        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            txtProductId.Text = string.Empty;
            txtProductName.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtDescription.Text = string.Empty;
            dpManufactureDate.SelectedDate = null;
            dpExpiryDate.SelectedDate = null;
            cmbVendor.SelectedIndex = -1;
            dtgProducts.SelectedItem = null;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dpManufactureDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;

            if (datePicker.SelectedDate.HasValue && datePicker.SelectedDate.Value < DateTime.Today)
            {
                MessageBox.Show("Ngày sản xuất phải lớn hơn hoặc bằng ngày hiện tại.");
            }
        }

        private void dpExpiryDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            try
            {
                if (datePicker.SelectedDate.HasValue &&
                    (datePicker.SelectedDate.Value <= DateTime.Today ||
                     datePicker.SelectedDate.Value <= dpManufactureDate.SelectedDate.Value))
                {
                    MessageBox.Show("Hạn sử dụng phải lớn hơn ngày sản xuất");

                    dpExpiryDate.SelectedDate = null;
                    dpManufactureDate.SelectedDate = null;
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            string productName = txtProductName.Text.Trim();
            string description = txtDescription.Text.Trim();

            var results = _productRepo.SearchProducts(productName, description);

            if (results.Count == 0)
            {
                MessageBox.Show("Không tìm thấy sản phẩm nào.");
                dtgProducts.ItemsSource = _productRepo.GetAllProduct();
            }
            else
            {
                dtgProducts.ItemsSource = results;
            }
        }


    }
}
