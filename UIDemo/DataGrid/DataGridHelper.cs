using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace UIDemo.MyDataGrid
{
    public class DataGridHelper
    {
        #region 动态列模式依赖属性
        [AttachedPropertyBrowsableForType(typeof(DataGrid))]
        public static object GetColumnsSource(DependencyObject obj)
        {
            return (object)obj.GetValue(ColumnsSourceProperty);
        }
        public static void SetColumnsSource(DependencyObject obj, object value)
        {
            obj.SetValue(ColumnsSourceProperty, value);
        }
        // Using a DependencyProperty as the backing store for ColumnsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnsSourceProperty =
            DependencyProperty.RegisterAttached("ColumnsSource",
                typeof(object), typeof(DataGridHelper), new UIPropertyMetadata(null, ColumnsSourceChanged));
        #endregion

        private static void ColumnsSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            DataGrid gridClumns = obj as DataGrid;
            if (gridClumns != null)
            {
                gridClumns.Columns.Clear();
#if false
                if (e.OldValue != null)
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(e.OldValue);
                    if (view != null)
                        RemoveHandlers(gridView, view);
                }
#endif

                if (e.NewValue != null)
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(e.NewValue);
                    if (view != null)
                    {
                        //AddHandlers(gridView, view);
                        CreateColumns(gridClumns, view);
                    }
                }
            }
        }
        private static void CreateColumns(DataGrid gridView, ICollectionView view)
        {
            foreach (var item in view)
            {
                DataGridColumn column = CreateColumn(gridView, item);
                gridView.Columns.Add(column);
            }
        }

        private static DataGridColumn CreateColumn(DataGrid gridView, object columnSource)
        {
            DataGridTextColumn column = new DataGridTextColumn();
            var info = columnSource as DataGridColumnInfoM;

            if (!string.IsNullOrEmpty(info.Header))
            {
                column.Header = info.Header;
            }
            if(!string.IsNullOrEmpty(info.Binding))
            {

                column.Binding = new Binding(info.Binding);
            }
            /*
            if (info.DefaultWidth > 0)
            {
                column.Width = info.DefaultWidth;
            }
            */
            return column;
#if false
            DataTemplate template = new DataTemplate();
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(TextBlock));
            factory.SetValue(TextBlock.HorizontalAlignmentProperty, info.HorizontalAlignment);
            if (info.Foreground != null)
            {
                factory.SetValue(TextBlock.ForegroundProperty, info.Foreground);
            }
            if (info.ForegroundTriggerParameters != null && !string.IsNullOrEmpty(info.ForegroundMemberPath))
            {
                foreach (var o in info.ForegroundTriggerParameters)
                {
                    var trigger = new DataTrigger();
                    trigger.Binding = new Binding(info.ForegroundMemberPath);
                    trigger.Value = o.Key;
                    var setter = new Setter();
                    setter.Property = TextBlock.ForegroundProperty;
                    setter.Value = o.Value;
                    trigger.Setters.Add(setter);
                    template.Triggers.Add(trigger);
                }
            }

            var binding = new Binding(info.DisplayMemberPath);
            if (!string.IsNullOrEmpty(info.StringFormat))
            {
                binding.StringFormat = string.Format("{{0:{0}}}", info.StringFormat);
            }
            if (info.Converter != null)
            {
                binding.Converter = info.Converter;
                binding.ConverterParameter = info.ConverterParameter;
            }
            factory.SetBinding(TextBlock.TextProperty, binding);
            template.VisualTree = factory;

            column.CellTemplate = template;

            ListViewItemSortData scd = new ListViewItemSortData(info.DisplayMemberPath);
            scd.Direction = info.SortDirection;
            if (SortColumns.Count(c => c.Key == column) == 0)
            {
                SortColumns.Add(column, scd);
            }
            else
            {
                Console.WriteLine("Repeat SortColumns..................");
            }

            return column;
#endif
        }
    }
}
