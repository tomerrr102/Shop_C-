using B_L;
using BlApi;
using DalApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Annotations;
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
    /// Interaction logic for BO_ProductListWindow.xaml
    /// </summary>
    public partial class BO_ProductListWindow : Window
    {
        private IBL BL_Func = BlApi.Factory.GetBL();

        public BO_ProductListWindow()
        {
            InitializeComponent();
            //ObservableCollection<BO.ProductForList> collection
            // = new ObservableCollection<BO.ProductForList>(BL_Func.BO_Product.GetProductForLists());

            // CarListView
            CarListView.ItemsSource = BL_Func.BO_Product.GetProductForLists();
            cbProductSelector.ItemsSource = Enum.GetValues(typeof(BO.CarCategory));

            // OrderForListView
            OrderForListView.ItemsSource = BL_Func.BO_Order.GetOrderForList();

        }


        // --------------------------------  CarListView  -------------------------------------------

        private void cbProductSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((BO.CarCategory)cbProductSelector.SelectedItem == BO.CarCategory.None)
                CarListView.ItemsSource = BL_Func.BO_Product.GetAllByCondition();

            else
                CarListView.ItemsSource = BL_Func.BO_Product.
                    GetAllByCondition(x => x.CarType == (BO.CarCategory)cbProductSelector.SelectedItem);
        }

        private void btAddCar_Click(object sender, RoutedEventArgs e)
        {
            new BO_ProductWindow(0, this).Show();
        }

        private void CarListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CarListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList p = (BO.ProductForList)CarListView.SelectedItem;
            if (p != null) { new BO_ProductWindow(p.CarID, this).Show(); }

        }


        //----------------------------------  OrderForListView  -----------------------------------
        private void OrderForList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.OrderForList order = (BO.OrderForList)OrderForListView.SelectedItem;
            if(order != null) { new BO_OrderWindow(order.ID,this).Show(); }
        }

        private void btBackManu_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
                
        }
    }
}
