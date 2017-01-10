using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIDemo.ViewModel
{
    public class DataGridDemoVM : ViewModelBase
    {
        public DataGridDemoVM()
        {
            init();
        }
        private int init()
        {
            DataGridColumns = new ObservableCollection<MyDataGrid.DataGridColumnInfoM>();
            DataGridColumns.Add(new MyDataGrid.DataGridColumnInfoM()
            { Header = "第一列", Binding = "ID", HorizontalContentAlignment = "Right", DefaultWidth = 10 });
            DataGridColumns.Add(new MyDataGrid.DataGridColumnInfoM() { Header = "第二列", Binding = "Name" });
            DataGridColumns.Add(new MyDataGrid.DataGridColumnInfoM() { Header = "第三列", Binding = "Address" });
            DataGridColumns.Add(new MyDataGrid.DataGridColumnInfoM() { Header = "第四列", Binding = "ID" });
            DataGridColumns.Add(new MyDataGrid.DataGridColumnInfoM() { Header = "第二列", Binding = "Name" });
            DataGridColumns.Add(new MyDataGrid.DataGridColumnInfoM() { Header = "第二列", Binding = "Name" });
            DataGridColumns.Add(new MyDataGrid.DataGridColumnInfoM() { Header = "第二列", Binding = "Name" });
            DataGridColumns.Add(new MyDataGrid.DataGridColumnInfoM() { Header = "第二列", Binding = "Name" });
            DataGridColumns.Add(new MyDataGrid.DataGridColumnInfoM() { Header = "第二列", Binding = "Name" });
            DataGridColumns.Add(new MyDataGrid.DataGridColumnInfoM() { Header = "第二列", Binding = "Name" });
            DataGridColumns.Add(new MyDataGrid.DataGridColumnInfoM() { Header = "第二列", Binding = "Name" });
            DataGridColumns.Add(new MyDataGrid.DataGridColumnInfoM() { Header = "第二列", Binding = "Name" });
            DataGridColumns.Add(new MyDataGrid.DataGridColumnInfoM() { Header = "第二列", Binding = "Name" });
            DataGridColumns.Add(new MyDataGrid.DataGridColumnInfoM() { Header = "第二列", Binding = "Name" });
            DataGridColumns.Add(new MyDataGrid.DataGridColumnInfoM() { Header = "第二列", Binding = "Name" });
            DataGridColumns.Add(new MyDataGrid.DataGridColumnInfoM() { Header = "第二列", Binding = "Name" });

            DataGridList = new ObservableCollection<Model.DataGridM>();
            DataGridList.Add(new Model.DataGridM() { ID = 1, Name = "dj1", Address = "ZJ" });
            DataGridList.Add(new Model.DataGridM() { ID = 2, Name = "dj2", Address = "ZJ" });
            DataGridList.Add(new Model.DataGridM() { ID = 3, Name = "dj3", Address = "ZJ" });
            DataGridList.Add(new Model.DataGridM() { ID = 4, Name = "dj4", Address = "ZJ" });
            DataGridList.Add(new Model.DataGridM() { ID = 5, Name = "dj5", Address = "ZJ" });
            DataGridList.Add(new Model.DataGridM() { ID = 6, Name = "dj6", Address = "ZJ" });
            DataGridList.Add(new Model.DataGridM() { ID = 7, Name = "dj7", Address = "ZJ" });
            DataGridList.Add(new Model.DataGridM() { ID = 8, Name = "dj8", Address = "ZJ" });
            DataGridList.Add(new Model.DataGridM() { ID = 9, Name = "dj9", Address = "ZJ" });
            DataGridList.Add(new Model.DataGridM() { ID = 10, Name = "dj10", Address = "ZJ" });
            int loop = 0;
            while (loop++ < 10000)
            {
                DataGridList.Add(new Model.DataGridM() { ID = loop, Name = "dj" + loop, Address = "ZJ" + loop });
            }
            return 0;
        }

        public ObservableCollection<Model.DataGridM> DataGridList
        {
            get { return GetValue(() => DataGridList); }
            set { SetValue(() => DataGridList, value); }
        }

        public ObservableCollection<MyDataGrid.DataGridColumnInfoM> DataGridColumns
        {
            get { return GetValue(() => DataGridColumns); }
            set { SetValue(() => DataGridColumns, value); }
        }
    }
}
