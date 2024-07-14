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
    /// Interaction logic for BO_OrderWindow.xaml
    /// </summary>
    public partial class BO_OrderWindow : Window
    {

        private IBL BL_Func = BlApi.Factory.GetBL();
        private BO_ProductListWindow productListWindow;
        public BO_OrderWindow()
        {
            InitializeComponent();
        }

        public BO_OrderWindow(int id, BO_ProductListWindow _productListWindow)
        {
            InitializeComponent();

            
            BO.Order order = BL_Func.BO_Order.GetOrder(id);
            productListWindow = _productListWindow;
            OrderItemList.ItemsSource = order.OrderDetails;

            if (id > 0 && order != null)
            {

                txtBoxUniqCode.Text = order.OrderUniqCode.ToString();
                txtBoxCustomerName.Text = order.OrderCustomerName;
                txtBoxCustomerEmail.Text = order.OrderCustomerEmail;
                txtBoxCustomerAddress.Text = order.OrderCustomerAddress;
                txtBoxTotalPrice.Text = order.Totalprice.ToString();
                txtBoxOrderCreationDate.Text = order.OrderCreationDate.ToString();                
                txtBoxOrderShipDate.Text = order.OrderShipDate.ToString();
                txtBoxOrderDeliveryDate.Text = order.OrderDeliveryDate.ToString();
                txtBoxPaymentDate.Text = order.paymentDate.ToString();
                txtBoxOrderStatus.Text = order.OrderStatus.ToString();
                //MessageBox.Show(order.OrderDetails[0].ToString());
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                BL_Func.BO_Order.UpdateOrder(Convert.ToInt32(txtBoxUniqCode.Text));
                MessageBox.Show("Seccussfuly shiped");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);               
            }           
            this.Close();
            productListWindow.OrderForListView.ItemsSource = BL_Func.BO_Order.GetOrderForList();
        }

        private void Update_Deliver_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL_Func.BO_Order.UpdateOrderDeliver(Convert.ToInt32(txtBoxUniqCode.Text));
                MessageBox.Show("Seccussfuly delivered");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
            productListWindow.OrderForListView.ItemsSource = BL_Func.BO_Order.GetOrderForList();
        }

        private void btShow_Details(object sender, RoutedEventArgs e)
        {

            int id = Convert.ToInt32(txtBoxUniqCode.Text);
            BO.OrderTracking orderTracking = BL_Func.BO_Order.OrderTrack(id); 
            MessageBox.Show(orderTracking.ToString());
            //MessageBox.Show(order.OrderDetails[0].ToString());
        }
    }
}



