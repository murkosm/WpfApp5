﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp5.DB;


namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
    
    public ObservableCollection<Product> SelectProduct { get; set; }
    public Product Product { get; set; }

    public MainWindow()
        {

            InitializeComponent();
            SelectProduct = new();
            DataContext = this;
            this.Loaded += Sqlite_Loaded;
        }

        private void Sqlite_Loaded(object sender, RoutedEventArgs e)
        {
            DataBaseContext db = new DataBaseContext();
            db.Database.Migrate();
            List<Product> products = db.Products.ToList();
            selectProduct.ItemsSource = products;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            foreach (Product product in products)
            {
                string str = "Id " + product.Id + "\n" + "Name: " + product.Name + "\n" + "Price: " + product.Price + "₽" + "\n" + "Description: " + product.Description;
                QRCodeData data = qrGenerator.CreateQrCode(str, QRCodeGenerator.ECCLevel.Q);
                QRCode qR = new QRCode(data);
                BitmapImage qrCodeImage = new BitmapImage();

                using (MemoryStream stream = new MemoryStream())
                {
                    qR.GetGraphic(20).Save(stream, ImageFormat.Png);
                    stream.Seek(0, SeekOrigin.Begin);
                    qrCodeImage.BeginInit();
                    qrCodeImage.CacheOption = BitmapCacheOption.OnLoad;
                    qrCodeImage.StreamSource = stream;
                    qrCodeImage.EndInit();
                }

                SelectProduct.Add(new Product { Name = product.Name, Description = product.Description, Id = product.Id, Price = product.Price, QrCode = qrCodeImage });
            }

            selectProduct.ItemsSource = SelectProduct;
        }

       
        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {

            Window1.Window1 add = new Window1.Window1();
            Close();
            add.Show();
          
        }

        private void Btn_edit_Click(object sender, RoutedEventArgs e)
        {
           
            var product = selectProduct.SelectedItem as Product;
            if (new Window1.EditWindow(product).ShowDialog() == true)
            {
                using (var context = new DataBaseContext())
                {
                    context.Entry(product).State = EntityState.Modified;
                    context.SaveChanges();
                }
                selectProduct.Items.Refresh();
                
            }
        }

            private void Btn_delete_Click(object sender, RoutedEventArgs e)

            {
                if (Product != null)
                {


                    if (MessageBox.Show("Delete this product?",
                        "Warning",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        var product = selectProduct.SelectedItem as Product;
                        using (var context = new DataBaseContext())
                        {
                            context.Products.Remove(product);
                            context.SaveChanges();
                            selectProduct.ItemsSource = context.Products.ToList();
                        }
                    }
                }
            }
        public BitmapImage Convert(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }


    }
}
