using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UIDemo.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        public MainWindowVM()
        {
            init();
        }

        private int init()
        {
            return 0;
        }

        private ICommand _DataGridCommand;
        public ICommand DataGridCommand
        {
            get
            {
                if (_DataGridCommand == null)
                    _DataGridCommand = new RelayCommand(DataGridCommandWorker, () => true);
                return _DataGridCommand;
            }
        }
        private void DataGridCommandWorker()
        {
            new View.DataGridDemo().ShowDialog();
        }

        private ICommand _UCDataGridCommand;
        public ICommand UCDataGridCommand
        {
            get
            {
                if (_UCDataGridCommand == null)
                    _UCDataGridCommand = new RelayCommand(UCDataGridCommandWorker, () => true);
                return _UCDataGridCommand;
            }
        }
        private void UCDataGridCommandWorker()
        {
            new View.UCDataGridDemo().ShowDialog();
        }
    }
}
