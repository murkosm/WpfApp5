using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Xml.Linq;
using WpfApp5.DB;
using static QRCoder.PayloadGenerator.ShadowSocksConfig;

namespace WpfApp5.Window1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Product product { get; set; }
        public Window1()
        {
            InitializeComponent();
            product = new Product();
            product.Id = new Guid();
            DataContext = product;
        }

        //public static string AddProduct(Guid Id , string Name , double Price , string Description)
        //{
        //    string result = "fkfjfjfj";
        //    using (DataBaseContext db = new DataBaseContext())
        //    {
        //        db.Database.Migrate();
        //        bool check = db.Products.Any(el => el.Name == Name && el.Price == Price && el.Description == Description);
        //        if (!check)
        //        {
        //            Product newproduct = new Product
        //            {
        //                Name = Name,
        //                Price = Price,
        //                Description = Description,
        //                Id = new Guid()
        //            };
        //            db.Products.Add(newproduct);
        //            db.SaveChanges();
        //            result = "done";
        //        }
        //        return result;
        //    }
        //}
        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {
            AddProduct(product.Id, product.Name, product.Price, product.Description);
            MainWindow mainWindow = new MainWindow();
            Close();
            mainWindow.ShowDialog();

        }

    
    }
}
