using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp3
{
    class CheckComboBoxViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> AllItems { get; set; } = new ObservableCollection<string>()
        {
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k"
        };

        public ObservableCollection<string> SelectedItems { get; set; } = new ObservableCollection<string>()
        {
            "b", "e", "f"
        };

        public ObservableCollection<string> TriStateItems { get; set; } = new ObservableCollection<string>()
        {
            "g", "i"
        };

        public CheckComboBoxViewModel()
        {
            SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;
            System.Diagnostics.Debug.WriteLine("SelectedItems=" +
                String.Join(",", SelectedItems.ToArray()));
            TriStateItems.CollectionChanged += TriStateItems_CollectionChanged;
            AddToSelectedItemsCommand = new TestCommand(AddToSelectdItems);

        }

        private void TriStateItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("TriStateItems=" +
               String.Join(",", TriStateItems.ToArray()));
        }

        private void SelectedItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("SelectedItems=" +
                String.Join(",", SelectedItems.ToArray()));
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach(string item in e.NewItems)
                {
                    TriStateItems.Remove(item);
                }
            }
        }

        public ICommand AddToSelectedItemsCommand { get; }

        private void AddToSelectdItems()
        {
            TriStateItems.Add("a");
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    class TestCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        Action _action;

        public TestCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action.Invoke();
        }
    }
}
