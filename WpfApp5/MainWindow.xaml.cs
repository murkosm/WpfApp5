using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
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
                QRCoder.QRCodeGenerator qr = new QRCoder.QRCodeGenerator();
                QRCoder.QRCodeData data = qr.CreateQrCode(str, QRCoder.QRCodeGenerator.ECCLevel.L);
                QRCoder.QRCode code = new QRCoder.QRCode(data);
                Bitmap bitmap = code.GetGraphic(100);
                using (MemoryStream memory = new MemoryStream())
                {
                    bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                    memory.Position = 0;
                    BitmapImage bitmapimage = new BitmapImage();
                    bitmapimage.BeginInit();
                    bitmapimage.StreamSource = memory;
                    bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapimage.EndInit();
                    
                }

                SelectProduct.Add(new Product { Name = product.Name, Description = product.Description, Id = product.Id, Price = product.Price, QrCode = product.QrCode });
            }

             selectProduct.ItemsSource = products;

        }

        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {

            Window1.Window1 add = new Window1.Window1();
            add.Show();
        }

        private void Btn_edit_Click(object sender, RoutedEventArgs e)
        {
            var product = selectProduct.SelectedItem as Product;
            Window1.EditWindow edit = new Window1.EditWindow();
            edit.Show();
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
    }
}
