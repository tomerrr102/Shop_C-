using B_L;
using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace PL
{
    /// <summary>
    /// Interaction logic for BO_ProductWindow.xaml
    /// </summary>
    public partial class BO_ProductWindow : Window
    {
        //public static B_L.BL BL_Func = new B_L.BL();
        //public bool ClosingWindow { get; private set; } = true;
        
        private IBL BL_Func = BlApi.Factory.GetBL();

        BO.Product AddProduct = new BO.Product();
        private BO_ProductListWindow productListWindow;   

        public BO_ProductWindow(int id, BO_ProductListWindow _productListWindow)
        {
            InitializeComponent();
            cbCarTypes.ItemsSource = Enum.GetValues(typeof(BO.CarCategory));
            productListWindow = _productListWindow;

            if (id > 0)
            {
                btAddCar.Visibility = Visibility.Hidden; // if we whant to update then the butten "add" should be hidden
                BO.Product p = BL_Func.BO_Product.GetProduct(id);
                
                txtBoxCarName.Text = p.CarName.ToString();
                txtBoxCarID.Text = p.CarID.ToString();
                txtBoxCarPrice.SelectedText = p.CarPrice.ToString();
                cbCarTypes.SelectedItem = p.CarType;
                txtBoxCarAmount.Text = p.CarAmount.ToString();

                txtBoxCarID.IsEnabled = false; // we do not allow to updat ID
            }
            else
            {
                btUpdateProduct.Visibility = Visibility.Hidden;
                cbCarTypes.SelectedItem = BO.CarCategory.None;
            }
           
        }

        private void txtBoxCarID_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (Regex.IsMatch(txtBoxCarID.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtBoxCarID.Text = txtBoxCarID.Text.Remove(txtBoxCarID.Text.Length - 1);
            }
        }

        private void txtBoxCarPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(txtBoxCarPrice.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtBoxCarPrice.Text = txtBoxCarPrice.Text.Remove(txtBoxCarPrice.Text.Length - 1);
            }
        }

        private void txtBoxCarAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(txtBoxCarAmount.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtBoxCarAmount.Text = txtBoxCarAmount.Text.Remove(txtBoxCarAmount.Text.Length - 1);
            }
        }

        private void btAddCar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtBoxCarName.Text == "" || txtBoxCarID.Text == "" || cbCarTypes.SelectedItem == null
                    || txtBoxCarPrice.Text == "" || txtBoxCarAmount.Text == "")
                    //not all the fields are full
                    MessageBox.Show("Not all fields are full");
                else
                {
                    //add the car
                    BL_Func.BO_Product.AddProduct(new BO.Product
                    {
                        CarName = txtBoxCarName.Text,
                        CarID = Convert.ToInt32(txtBoxCarID.Text),
                        CarType = (BO.CarCategory)cbCarTypes.SelectedItem,
                        CarPrice = Convert.ToInt32(txtBoxCarPrice.Text),
                        CarAmount = Convert.ToInt32(txtBoxCarAmount.Text)
                    });
                    //seccessfully added message
                    MessageBoxResult result = MessageBox.Show("Seccussfuly added");
                    if (result == MessageBoxResult.OK)
                    {
                        //close when OK pressed
                        //ClosingWindow = false;
                        this.Close();
                        productListWindow.CarListView.ItemsSource = BL_Func.BO_Product.GetProductForLists();
                        // we have to show all the list again
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtBoxCarName.Text == "" || txtBoxCarID.Text == "" || cbCarTypes.SelectedItem == null
                    || txtBoxCarPrice.Text == "" || txtBoxCarAmount.Text == "")
                    //not all the fields are full
                    MessageBox.Show("Not all fields are full");
                else
                {
                    //add the car
                    BL_Func.BO_Product.UpdateProduct(new BO.Product
                    {
                        CarName = txtBoxCarName.Text,
                        CarID = Convert.ToInt32(txtBoxCarID.Text), 
                        CarType = (BO.CarCategory)cbCarTypes.SelectedItem,
                        CarPrice = Convert.ToInt32(txtBoxCarPrice.Text),
                        CarAmount = Convert.ToInt32(txtBoxCarAmount.Text)
                    });
                    //seccessfully added message
                    MessageBoxResult result = MessageBox.Show("Seccussfuly added");
                    if (result == MessageBoxResult.OK)
                    {
                        //close when OK pressed
                        //ClosingWindow = false;
                        this.Close();
                        productListWindow.CarListView.ItemsSource = BL_Func.BO_Product.GetProductForLists();
                        // we have to show all the list again
                    }
                }
            }
            catch (BO.NotFoundException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}