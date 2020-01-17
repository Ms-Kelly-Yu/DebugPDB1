using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragDropToDesk.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {

        }
        private ObservableCollection<DataGridValues> users = new ObservableCollection<DataGridValues>();
        public ObservableCollection<DataGridValues> Users {
            get { return users; }
            set { users = value;
                RaisePropertyChanged("Users");
            }
        }

        private string ip;

        public string IP
        {
            get { return ip; }
            set
            {
                ip = value;
                RaisePropertyChanged("IP");
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }
    }

    public class DataGridValues: BindableBase
    {
        private string id;

        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                RaisePropertyChanged("ID");
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }
    }
}
