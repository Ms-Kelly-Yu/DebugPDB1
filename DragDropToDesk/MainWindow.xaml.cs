using DragDropToDesk.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DragDropToDesk
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //
            InitializeComponent();

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            dt.Rows.Add("111", "yuli");
            dt.Rows.Add("222", "aza");

            var users = dt.ToModels<User>();
            MainWindowViewModel model = new MainWindowViewModel();
            model.Users.Add(new DataGridValues() { ID = "1", Name = "Test11" });
            model.Users.Add(new DataGridValues() { ID = "2", Name = "Test22" });
            model.Users.Add(new DataGridValues() { ID = "3", Name = "Test33" });
            model.Users.Add(new DataGridValues() { ID = "4", Name = "Test44" });
            DataContext = model;
        }

        

        private MainWindowViewModel GetModel()
        {
            MainWindowViewModel model = new MainWindowViewModel();
            model.Users.Add(new DataGridValues() { ID = "1",Name = "Test11" });
            return model;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var data = DataContext as MainWindowViewModel;
            var macs = new List<string>() { "1qaz" };
            int i = 0;
            data.IP.Split('.').ToList().ForEach(x =>
            {
                //if (i++ > 1)
                    macs.Add(x);
            });
            string strPaw = "";
            macs.ForEach(x => strPaw += x);
            byte[] btPassword = Encoding.ASCII.GetBytes(strPaw);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] btMD5Password = md5.ComputeHash(btPassword, 2, 7);
            data.Password = BitConverter.ToString(btMD5Password).Replace("-", "");
        }
    }

    public static class Extension {

        public static List<T> ToModels<T>(this DataTable dt) where T : new()
        {
            List<T> result = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T target = new T();
                Type targetType = target.GetType();
                foreach (DataColumn dc in dt.Columns)
                {
                    targetType.GetProperty(dc.ColumnName).SetValue(target, dr[dc.ColumnName]);
                }
                result.Add(target);
            }
            return result;
        }
    }

}
