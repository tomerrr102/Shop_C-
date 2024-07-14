using BlApi;
using BO;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for UserProducts.xaml
    /// </summary>
    public partial class UserProducts : Window
    {

        private IBL BL_Func = BlApi.Factory.GetBL();
        private BO.Cart cart = new BO.Cart();
        public UserProducts()
        {
            InitializeComponent();
            List<BO.ProductItem> productItems = new();
            List<BO.OrderItem> orderItems = new();
            cbCategory.ItemsSource = Enum.GetValues(typeof(BO.CarCategory));

            foreach (var item in BL_Func.BO_Product.GetAllByCondition())
                productItems.Add(BL_Func.BO_Product.GetProdctItem(item.CarID, cart));
           
            ProductsListView.ItemsSource = productItems;
            ProductsListView.IsEnabled = false;
            CartListView.IsEnabled = false;
        }

        private void btEnterDetails_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text == "" || txtUserEmail.Text == "" || txtUserAddress.Text == "")
            {
                MessageBox.Show("Enter all details");
            }

            else
            {
                // if the user did fullfill the textBoxes, then he can no longer fullfill them again
                txtUserName.IsEnabled = false;
                txtUserEmail.IsEnabled = false;
                txtUserAddress.IsEnabled = false;
                btEnterDetails.IsEnabled = false;


                // and now he can use the lists of products and his personal list of orders
                ProductsListView.IsEnabled = true;
                CartListView.IsEnabled = true;


                // build the cart
                cart.CustomerName = txtUserName.Text;
                cart.CartMail = txtUserEmail.Text;
                cart.CartAdress = txtUserEmail.Text;

            }
        }

        private void ProductsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem p = (BO.ProductItem)ProductsListView.SelectedItem;
            if (p != null) { new UserDetails(cart, p.CarID, this).Show(); }
        }

        private void CartListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.OrderItem order = (BO.OrderItem)CartListView.SelectedItem;
            if (order != null) { new UserDetails(cart, order.ProductID, this, "update ctor").Show(); }
        }

        private void btBuyOrders_Click(object sender, RoutedEventArgs e)
        {

            if (cart.items.Count() > 0)
            {
                try
                {
                    BL_Func.BO_Cart.EnterCart(cart);
                    MessageBox.Show("Your orders shiped seccessfully");
                    new MainWindow().Show();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else { MessageBox.Show("You did not buy anything yet"); }
        }

        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        // the filter by category
        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            List<BO.ProductItem> tempList = new();

            if ((BO.CarCategory)cbCategory.SelectedItem == BO.CarCategory.None)
            {
                foreach (var item in BL_Func.BO_Product.GetAllByCondition())
                    tempList.Add(BL_Func.BO_Product.GetProdctItem(item.CarID, cart));
            }
            else
            {
                foreach (var item in BL_Func.BO_Product.GetAllByCondition(item => item.CarType == (BO.CarCategory)cbCategory.SelectedItem))
                    tempList.Add(BL_Func.BO_Product.GetProdctItem(item.CarID, cart));
            }
            ProductsListView.ItemsSource = tempList;
        }

        private void CartListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
