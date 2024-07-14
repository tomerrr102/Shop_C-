using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for UserDetails.xaml
    /// </summary>
    public partial class UserDetails : Window
    {

        private IBL BL_Func = BlApi.Factory.GetBL();
        private UserProducts userProducts;
        BO.Cart cart = new Cart();
        int id;

        public UserDetails()
        {
            InitializeComponent();
        }

        public UserDetails(BO.Cart _cart, int _id, UserProducts _userProducts, string str)
        {
            InitializeComponent();
            BO.Product p = BL_Func.BO_Product.GetProduct(_id);

            // if we whant to update then the butten "add" should be hidden
            cbCarTypes.ItemsSource = Enum.GetValues(typeof(BO.CarCategory));
            txtBoxCarName.Text = p.CarName.ToString();
            txtBoxCarID.Text = p.CarID.ToString();
            txtBoxCarPrice.SelectedText = p.CarPrice.ToString();
            cbCarTypes.SelectedItem = p.CarType;
            txtBoxCarAmount.Text = p.CarAmount.ToString();

            // we do not allow to change the product
            txtBoxCarID.IsEnabled = false;
            cbCarTypes.IsEnabled = false;
            txtBoxCarName.IsEnabled = false;
            txtBoxCarID.IsEnabled = false;
            txtBoxCarPrice.IsEnabled = false;
            cbCarTypes.IsEnabled = false;
            txtBoxCarAmount.IsEnabled = false;

            userProducts = _userProducts;
            id = _id;
            cart = _cart;

            btAddToCart.Visibility = Visibility.Hidden;
        }
        public UserDetails(BO.Cart _cart, int _id, UserProducts _userProducts)
        {
            InitializeComponent();
            BO.Product p = BL_Func.BO_Product.GetProduct(_id);

            // if we whant to update then the butten "add" should be hidden
            cbCarTypes.ItemsSource = Enum.GetValues(typeof(BO.CarCategory));
            txtBoxCarName.Text = p.CarName.ToString();
            txtBoxCarID.Text = p.CarID.ToString();
            txtBoxCarPrice.SelectedText = p.CarPrice.ToString();
            cbCarTypes.SelectedItem = p.CarType;
            txtBoxCarAmount.Text = p.CarAmount.ToString();

            // we do not allaw to change the product
            txtBoxCarID.IsEnabled = false;
            cbCarTypes.IsEnabled = false;
            txtBoxCarName.IsEnabled = false;
            txtBoxCarID.IsEnabled = false;
            txtBoxCarPrice.IsEnabled = false;
            cbCarTypes.IsEnabled = false;
            txtBoxCarAmount.IsEnabled = false;


            userProducts = _userProducts;
            cart = _cart;
            id = _id;

            txtUpdateAmount.Visibility = Visibility.Hidden;
            btConfirmAmount.Visibility = Visibility.Hidden;
            lbNewAmount.Visibility = Visibility.Hidden;

        }

        private void txtUpdateAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(txtUpdateAmount.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtUpdateAmount.Text = txtUpdateAmount.Text.Remove(txtUpdateAmount.Text.Length - 1);
            }
        }
        private void cbCarTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void btAddToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cart = BL_Func.BO_Cart.AddItemToCart(cart, id);
                MessageBox.Show("the product added seccesfully");
                Close();
                //userProducts.CartListView.Items.Add(cart.items[cart.items.Count - 1]);

                List<BO.ProductItem> productItems = new();
                foreach (var item in BL_Func.BO_Product.GetProductForLists())
                {
                    productItems.Add(BL_Func.BO_Product.GetProdctItem(item.CarID, cart));
                }
                userProducts.ProductsListView.ItemsSource = productItems;


                userProducts.CartListView.Items.Refresh(); // refreshing the list 
                userProducts.CartListView.ItemsSource = cart.items;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btConfirmAmount_Click(object sender, RoutedEventArgs e)
        {

            if (txtUpdateAmount.Text == "")
            {
                MessageBox.Show("you need to fill the fileds");
                return;

            }
            try
            {
                cart = BL_Func.BO_Cart.UpdateProductAmount(cart, id, int.Parse(txtUpdateAmount.Text));
                Close();
                userProducts.CartListView.ItemsSource = cart.items;

                List<BO.ProductItem> productItems = new();
                foreach (var item in BL_Func.BO_Product.GetProductForLists())
                {
                    productItems.Add(BL_Func.BO_Product.GetProdctItem(item.CarID, cart));
                }
                userProducts.ProductsListView.Items.Refresh();
                userProducts.ProductsListView.ItemsSource = productItems;
                userProducts.CartListView.Items.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
