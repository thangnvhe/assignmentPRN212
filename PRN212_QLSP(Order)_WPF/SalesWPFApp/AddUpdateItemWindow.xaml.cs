using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SalesWPFApp
{
    public partial class AddUpdateItemWindow : Window
    {
        private bool IsEdit;
        private dynamic? Item;
        private string Type;
        private dynamic Repository;
        private List<OrderDetail> addedProducts = new();

        public AddUpdateItemWindow(string type, dynamic repository, dynamic item = null, bool isEdit = false)
        {
            InitializeComponent();
            Type = type;
            Item = item;
            IsEdit = isEdit;
            Repository = repository;

            string mode = "Adding new";
            string title = "";

            if (isEdit)
            {
                btn1.Content = "Update";
                mode = "Updating";
                txt0.IsEnabled = false;
                row0.Height = GridLength.Auto;
            }

            switch (type)
            {
                case "PRODUCT":
                    title = $"{mode} Product";

                    if (isEdit)
                    {
                        txt0.Text = item.ProductId.ToString();
                        txt1.Text = item.CategoryId.ToString();
                        txt2.Text = item.ProductName;
                        txt3.Text = item.Weight;
                        txt4.Text = item.UnitPrice.ToString();
                        txt5.Text = item.UnitInStock.ToString();
                    }

                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txt1, "Category Id");
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txt2, "Product Name");
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txt3, "Weight");
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txt4, "Unit Price");
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txt5, "Unit In Stock");

                    break;
                case "ORDER":
                    title = $"{mode} Order";

                    row2.Height = new(0);
                    row3.Height = new(0);
                    row4.Height = new(0);
                    row5.Height = new(0);
                    row6.Height = new(0);
                    row7.Height = GridLength.Auto;
                    row8.Height = GridLength.Auto;
                    row9.Height = GridLength.Auto;
                    row10.Height = GridLength.Auto;
                    row11.Height = GridLength.Auto;


                    var memberRepository = new MemberRepository();
                    var memebers = memberRepository.GetList();

                    var productRepository = new ProductRepository();
                    var products = productRepository.GetList();

                    foreach (var m in memebers)
                    {
                        cbmMember.Items.Add(m.MemberId.ToString());
                    }

                    foreach (var m in products)
                    {
                        cbProduct.Items.Add(m.ProductId.ToString());
                    }

                    if (isEdit)
                    {
                        var i = (Order)item;
                        txt0.Text = i.OrderId.ToString();
                        txt1.Text = i.Freight.ToString();
                        pickerOrderDate.Text = i.OrderDate.ToString();
                        pickerRequiredDate.Text = i.RequiredDate.ToString();
                        pickerShippedDate.Text = i.ShippedDate.ToString();
                        cbmMember.Text = i.MemberId.ToString();
                    }

                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txt1, "Freight");

                    break;
                case "MEMBER":
                    title = $"{mode} Member";

                    if (isEdit)
                    {
                        txt0.Text = item.MemberId.ToString();
                        txt1.Text = item.Email;
                        txt2.Text = item.CompanyName;
                        txt3.Text = item.City;
                        txt4.Text = item.Country;
                        txtPassword.Password = item.Password;
                    }

                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txt1, "Email");
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txt2, "Company Name");
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txt3, "City");
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(txt4, "Country");

                    row5.Height = new(0);
                    row6.Height = GridLength.Auto;
                    break;
                default:
                    break;
            }

            Title = title;
            lbTitle.Content = title;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var newItem = GetItemContent();
            string caption = "";
            try
            {
                if (newItem == null)
                {
                    lbError.Content = "Plese input all fields";
                }
                else
                {
                    if (IsEdit)
                    {
                        caption = "Update";
                        Repository.Update(newItem);
                    }
                    else
                    {
                        caption = "Add";
                        Repository.Add(newItem);
                    }
                    Close();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, caption);
            }

        }

        private dynamic? GetItemContent()
        {
            switch (Type)
            {
                case "PRODUCT":
                    var categoryId = txt1.Text;
                    var productName = txt2.Text;
                    var weight = txt3.Text;
                    var unitPrice = txt4.Text;
                    var unitInStock = txt5.Text;

                    if (string.IsNullOrWhiteSpace(categoryId) || string.IsNullOrWhiteSpace(productName) || string.IsNullOrWhiteSpace(weight) || string.IsNullOrWhiteSpace(unitPrice) || string.IsNullOrWhiteSpace(unitInStock))
                    {
                        return null;
                    }
                    else
                    {
                        if (IsEdit)
                        {
                            return new Product() { ProductId = int.Parse(txt0.Text), CategoryId = int.Parse(categoryId), ProductName = productName, UnitInStock = int.Parse(unitInStock), UnitPrice = decimal.Parse(unitPrice), Weight = weight };
                        }

                        return new Product() { CategoryId = int.Parse(categoryId), ProductName = productName, UnitInStock = int.Parse(unitInStock), UnitPrice = int.Parse(unitPrice), Weight = weight };
                    }

                case "ORDER":
                    var mId = cbmMember.Text;
                    var orderDate = pickerOrderDate.Text;
                    var requiredDate = pickerRequiredDate.Text;
                    var shippedDate = pickerShippedDate.Text;
                    var freight = txt1.Text;

                    if (string.IsNullOrWhiteSpace(mId) || string.IsNullOrWhiteSpace(orderDate) || string.IsNullOrWhiteSpace(requiredDate) || string.IsNullOrWhiteSpace(shippedDate) || string.IsNullOrWhiteSpace(freight))
                    {
                        return null;
                    }
                    else
                    {
                        if (IsEdit)
                        {
                            return new Order() { OrderId = int.Parse(txt0.Text), Member = new() { MemberId = int.Parse(mId) }, Freight = decimal.Parse(freight), OrderDate = DateTime.Parse(orderDate), RequiredDate = DateTime.Parse(requiredDate), ShippedDate = DateTime.Parse(shippedDate) };
                        }

                        return new Order() { Member = new() { MemberId = int.Parse(mId) }, Freight = decimal.Parse(freight), OrderDate = DateTime.Parse(orderDate), RequiredDate = DateTime.Parse(requiredDate), ShippedDate = DateTime.Parse(shippedDate), OrderDetails = addedProducts };
                    }

                case "MEMBER":
                    var email = txt1.Text;
                    var companyName = txt2.Text;
                    var city = txt3.Text;
                    var country = txt4.Text;
                    var password = txtPassword.Password;

                    if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(companyName) || string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(password))
                    {
                        return null;
                    }
                    else
                    {
                        if (IsEdit)
                        {
                            return new Member() { MemberId = int.Parse(txt0.Text), Email = email, CompanyName = companyName, City = city, Password = password, Country = country };
                        }
                        return new Member() { Email = email, CompanyName = companyName, City = city, Password = password, Country = country };
                    }

                default:
                    return null;

            }

        }



        private void DialogHost_DialogClosed(object sender, MaterialDesignThemes.Wpf.DialogClosedEventArgs eventArgs)
        {
            if (Equals(eventArgs.Parameter, true))
            {
                try
                {
                    var discount = double.Parse(txtDiscount.Text);
                    var unitPrice = decimal.Parse(txtUnitPrice.Text);
                    var quantity = int.Parse(txtQuantity.Text);
                    var productId = int.Parse(cbProduct.Text);

                    addedProducts.Add(new() { Discount = discount, ProductId = productId, Quantity = quantity, UnitPrice = unitPrice });


                    foreach (dynamic item in cbProduct.Items)
                    {
                        if (item == productId.ToString())
                        {
                            cbProduct.Items.Remove(item);
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
    }
}
