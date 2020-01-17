using MvvmDialogs;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;


namespace DragDropToDesk.ViewModels
{
    public class MainWindowViewModel: BindableBase
    {
        private readonly IDialogService dialogService;
        public ObservableCollection<string> Items { get; set; } = new ObservableCollection<string>() { "abc", "def"};

        public string SelectedItem { get; set; }

        public ICommand FileDeleteCommand => new DelegateCommand<string>(e =>
        {
            if (e is string item)
            {
                var result = this.dialogService.ShowMessageBox(
                                 this,
                                 $"Do you want to remove \"{item}\" item?",
                                 "Question...",
                                 MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    this.Items.Remove(item);
                }
            }
        }, e => e != null);
    }
}
