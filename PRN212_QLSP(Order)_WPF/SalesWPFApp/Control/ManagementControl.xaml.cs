using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SalesWPFApp.Control
{
    public partial class ManagementControl : UserControl

    {
        public string ManagementType
        {
            get { return (string)GetValue(ManagementTypeProperty); }
            set { SetValue(ManagementTypeProperty, value); }
        }

        public static readonly DependencyProperty ManagementTypeProperty =
            DependencyProperty.Register("ManagementType", typeof(string), typeof(ManagementControl));

        public bool IsAdmin
        {
            get { return (bool)GetValue(IsAdminProperty); }
            set { SetValue(IsAdminProperty, value); }
        }

        public static readonly DependencyProperty IsAdminProperty =
            DependencyProperty.Register("IsAdmin", typeof(bool), typeof(ManagementControl));


        dynamic? repository = null;

        public ManagementControl()
        {
            InitializeComponent();
        }


        private void LoadData()
        {
            dg.ItemsSource = repository?.GetList();
            SetHeader();
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg.SelectedItem != null)
            {
                btnDelete.IsEnabled = true;
                btnUpdate.IsEnabled = true;
            }
            else
            {
                btnDelete.IsEnabled = false;
                btnUpdate.IsEnabled = false;
            }
        }

        private void dg_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid? grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    DataGridRow? dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                    if (!dgr.IsMouseOver)
                    {
                        dgr.IsSelected = false;
                    }
                }
            }
        }

        private void dg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Password" || e.PropertyName == "Orders" || e.PropertyName == "OrderDetails" || e.PropertyName == "Member")
            {
                e.Column = null;
            }
        }

        private void dg_Loaded(object sender, RoutedEventArgs e)
        {
            SetHeader();
        }

        private void SetHeader()
        {
            if (dg.Columns.Count == 0) return;

            switch (ManagementType)
            {
                case "MEMBER":
                    dg.Columns[0].Header = "ID";
                    dg.Columns[1].Header = "Email";
                    dg.Columns[2].Header = "Company Name";
                    dg.Columns[3].Header = "City";
                    dg.Columns[4].Header = "Country";

                    txtManagementName.Text = "Member management";
                    break;
                case "PRODUCT":
                    dg.Columns[0].Header = "ID";
                    dg.Columns[1].Header = "Product Name";
                    dg.Columns[2].Header = "Weight";
                    dg.Columns[3].Header = "Unit Price";
                    dg.Columns[4].Header = "Unit In Stock";

                    txtManagementName.Text = "Product management";
                    break;
                case "ORDER":
                    dg.Columns[0].Header = "ID";
                    dg.Columns[1].Header = "Member ID";
                    dg.Columns[2].Header = "Order Date";
                    dg.Columns[3].Header = "Required Date";
                    dg.Columns[4].Header = "Shipped Date";
                    dg.Columns[5].Header = "Freight";

                    txtManagementName.Text = "Order management";

                    break;
                default:
                    break;
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            btnDelete.Visibility = Visibility.Visible;
            btnUpdate.Visibility = Visibility.Visible;
            btnAdd.Visibility = Visibility.Visible;
            

            switch (ManagementType)
            {
                case "MEMBER":
                    repository = new MemberRepository();
                    if (!IsAdmin)
                    {
                        btnDelete.Visibility = Visibility.Hidden;
                        btnUpdate.Visibility = Visibility.Hidden;
                        btnAdd.Visibility = Visibility.Hidden;
                    }
                    break;
                case "PRODUCT":
                    repository = new ProductRepository();
                    if (!IsAdmin)
                    {
                        btnDelete.Visibility = Visibility.Hidden;
                        btnUpdate.Visibility = Visibility.Hidden;
                        btnAdd.Visibility = Visibility.Hidden;
                    }
                    cbProductSearch.Visibility = Visibility.Visible;
                    txtProductSearch.Visibility = Visibility.Visible;
                    break;
                case "ORDER":
                    if (IsAdmin)
                    {
                        btnUpdate.Visibility = Visibility.Visible;
                        btnAdd.Visibility = Visibility.Visible;
                    }
                    repository = new OrderRepository();
                    break;
                default:
                    break;
            }

            LoadData();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var item = GetItem();

            switch (ManagementType)
            {
                case "MEMBER":
                    repository?.DeleteById(item.MemberId);
                    break;
                case "PRODUCT":
                    repository?.DeleteById(item.ProductId);
                    break;
                case "ORDER":
                    repository?.DeleteById(item.OrderId);
                    break;
                default:
                    break;
            }

            LoadData();
        }

        private dynamic GetItem()
        {
            dynamic item = dg.SelectedItem;

            switch (ManagementType)
            {
                case "MEMBER":
                    var member = (Member)item;
                    return member;
                case "PRODUCT":
                    var product = (Product)item;
                    return product;
                case "ORDER":
                    var order = (Order)item;
                    return order;
                default:
                    throw new Exception("Cannot get current Item");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var item = GetItem();

            var updateWindow = new AddUpdateItemWindow(ManagementType, repository, item, true);
            updateWindow.Closing += UpdateWindow_Closing;
            updateWindow.ShowDialog();
        }

        private void UpdateWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddUpdateItemWindow(ManagementType, repository);
            addWindow.Closing += UpdateWindow_Closing;
            addWindow.ShowDialog();
        }

        private void txtProductSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtProductSearch.Text)) return;

            ProductRepository? r = repository;

            try
            {
                switch (cbProductSearch.Text)
                {
                    case "Unit In Stock":
                        dg.ItemsSource = r?.GetByUnitInStock(int.Parse(txtProductSearch.Text));
                        break;
                    case "Unit Price":
                        dg.ItemsSource = r?.GetByUnitPrice(decimal.Parse(txtProductSearch.Text));
                        break;
                    case "Prouct Name":
                        dg.ItemsSource = r?.GetByName(txtProductSearch.Text);
                        break;
                    case "Product ID":
                        var t = new List<Product>();
                        var item = r?.GetById(int.Parse(txtProductSearch.Text));
                        t.Add(item);
                        if (t != null)
                        {

                            dg.ItemsSource = t;
                        }
                        else
                        {
                            dg.ItemsSource = new List<Product>();
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                dg.ItemsSource = new List<Product>();
            }

        }
    }
}
