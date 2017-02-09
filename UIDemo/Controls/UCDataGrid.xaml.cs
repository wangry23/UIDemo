using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Xml;

namespace UIDemo.Controls
{
    #region DataGridColumnInfoM
    public class DataGridColumnInfoM
    {
        /// <summary>
        /// 是否显示
        /// </summary>
        public int Enable { get; set; }
        /// <summary>
        /// 列名
        /// </summary>
        public string Header { get; set; }
        /// <summary>
        /// 行显示的值。
        /// </summary>
        public string Binding { get; set; }
        /// <summary>
        /// 值的显示格式。
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// 数据转换。
        /// </summary>
        public string Converter { get; set; }
        /// <summary>
        /// 对齐方式
        /// </summary>
        public string HorizontalContentAlignment { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 最小宽度。
        /// </summary>
        public int MinWidth { get; set; }
    }
    #endregion
    #region
    class MyXMLColumnsHelper
    {
        public List<DataGridColumnInfoM> ColumnsList;
        private string FileName;
        public MyXMLColumnsHelper(string sFileName, string sXmlPath)
        {
            FileName = sFileName;
            ColumnsList = new List<DataGridColumnInfoM>();
            LoadFromXml(sXmlPath);
        }

        public int LoadFromXml(string sXmlPath)
        {
            DataGridColumnInfoM szModel;
            try
            {
                XmlDocument doc = new XmlDocument();
                ColumnsList.Clear();
                doc.Load(@FileName); ///加载配置文件。
                XmlNode xn = doc.SelectSingleNode(sXmlPath + "/column");
                if (xn == null)
                {
                    throw new Exception("配置文件" + FileName + "中配置有问题, 请检查!");
                }

                ///循环得到需要显示的tab页。
                ///
                do
                {
                    szModel = new DataGridColumnInfoM();

                    szModel.Enable = int.Parse(xn.Attributes["enable"].Value);
                    //szModel.Type = int.Parse(xn.Attributes["type"].Value);
                    szModel.Header = xn.Attributes["header"].Value;
                    szModel.Binding = xn.Attributes["binding"].Value;
                    try
                    {
                        szModel.Format = xn.Attributes["format"].Value;
                    }
                    catch(Exception ex)
                    { }
                    try
                    {
                        szModel.Converter = xn.Attributes["converter"].Value;
                    }
                    catch (Exception ex)
                    { }
                    try
                    {
                        szModel.HorizontalContentAlignment = xn.Attributes["horizontalalignment"].Value;
                    }
                    catch(Exception ex)
                    { }
                    szModel.Width = int.Parse(xn.Attributes["width"].Value);
                    ColumnsList.Add(szModel);
                    xn = xn.NextSibling;
                } while (xn != null);
            }
            catch (Exception ex)
            {
                //BizContext.Logger.LogException(ex, GetType().ToString());
                return -1;
            }
            return 0;
        }

        public int SaveToXml(string sXmlPath)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement xe1;
            try
            {
                doc.Load(@FileName); ///加载配置文件。
                XmlNode xn = doc.SelectSingleNode(sXmlPath);
                if (xn == null)
                {
                    throw new Exception("配置文件" + FileName + "中配置有问题, 请检查!");
                }
                xn.RemoveAll(); ///先删除tab下的所有节点。
                                ///循环写入到tab页。
                foreach (var item in ColumnsList)
                {
                    xe1 = doc.CreateElement("column");
                    xe1.SetAttribute("enable", item.Enable.ToString());
                    //xe1.SetAttribute("type", item.Type.ToString());
                    xe1.SetAttribute("header", item.Header);
                    xe1.SetAttribute("binding", item.Binding);
                    xe1.SetAttribute("width", item.Width.ToString());
                    xn.AppendChild(xe1);
                }
                doc.Save(FileName);
            }
            catch (Exception ex)
            {
                //BizContext.Logger.LogException(ex, GetType().ToString());
                return -1;
            }
            return 0;
        }
    }
    #endregion
    /// <summary>
    /// UCDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class UCDataGrid : DataGrid
    {
        #region ElementPath属性
        /// <summary>
        /// 依赖属性ElementPath
        /// </summary>
        public static readonly DependencyProperty ElementPathProperty =
            DependencyProperty.Register("ElementPath", typeof(string), typeof(UCDataGrid),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ElementPathPropertyChangedCallback)));
        /// <summary>
        /// ElementPath属性值发生变化。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private static void ElementPathPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            if (sender != null && sender is UCDataGrid)
            {
                UCDataGrid dg = sender as UCDataGrid;
                if (dg != null)
                {
                    dg.Columns.Clear();

                    MyXMLColumnsHelper xmlHelper = new MyXMLColumnsHelper(dg.ConfigPath, dg.ElementPath);

                    if (xmlHelper.ColumnsList != null)
                    {
                        //ICollectionView view = CollectionViewSource.GetDefaultView(arg.NewValue);
                        ICollectionView view = CollectionViewSource.GetDefaultView(xmlHelper.ColumnsList);
                        if (view != null)
                        {
                            dg.CreateColumns(dg, view);
                        }
                    }
                }
            }
        }
        public string ElementPath
        {
            get
            {
                return (string)this.GetValue(ElementPathProperty);
            }
            set
            {
                this.SetValue(ElementPathProperty, value);
            }
        }
        #endregion
        #region ConfigFile属性
        public static readonly DependencyProperty ConfigPathProperty =
            DependencyProperty.Register("ConfigPath", typeof(string), typeof(UCDataGrid),
            new FrameworkPropertyMetadata("Alpha.xml", new PropertyChangedCallback(ConfigPathPropertyChangedCallback)));
        private static void ConfigPathPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            if (sender != null && sender is UCDataGrid)
            {
                UCDataGrid dg = sender as UCDataGrid;
                if (dg.ElementPath == null || dg.ElementPath == string.Empty)
                    return;

                ///加载配置的信息。
            }
        }
        public string ConfigPath
        {
            get
            {
                return (string)this.GetValue(ConfigPathProperty);
            }
            set
            {
                this.SetValue(ConfigPathProperty, value);
            }
        }
        #endregion
        public UCDataGrid()
        {
            InitializeComponent();
        }

        public static T GetVisualChild<T>(DependencyObject parent) where T : Visual
        {
            T child = default(T);

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
        #region 事件响应
        private void UCDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Right)
            {
                ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(this);
                if (scrollViewer != null)
                {
                    if (scrollViewer.ScrollableWidth > 0 && scrollViewer.HorizontalOffset >= 0 &&
                        scrollViewer.HorizontalOffset < scrollViewer.ExtentWidth)
                    {
                        double offset = 50.0;
                        switch (e.Key)
                        {
                            case Key.Left:
                                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - offset);
                                break;
                            case Key.Right:
                                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + offset);
                                break;
                        }
                        e.Handled = true;
                    }
                }
            }
        }
        
        private void Config_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Config" + ElementPath);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("save");
        }
        #endregion

        #region 函数区。
        private void CreateColumns(DataGrid gridView, ICollectionView view)
        {
            Style rightStyle = new Style(typeof(TextBlock));
            Style leftStyle = new Style(typeof(TextBlock));
            Setter setRight = new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Right);
            rightStyle.Setters.Add(setRight);
            Setter setMargin = new Setter(TextBlock.MarginProperty, new Thickness(5, 0, 5, 0));
            rightStyle.Setters.Add(setMargin);
            setRight = new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Left);
            leftStyle.Setters.Add(setRight);
            leftStyle.Setters.Add(setMargin);
            foreach (var item in view)
            {
                DataGridTextColumn column = new DataGridTextColumn();
                //var info = columnSource as DataGridColumnInfoM;
                var info = item as DataGridColumnInfoM;
                if (info.Enable == 0)
                    continue;

                if (!string.IsNullOrEmpty(info.Header))
                {
                    column.Header = info.Header;
                }
                if (!string.IsNullOrEmpty(info.Binding))
                {

                    column.Binding = new Binding(info.Binding);
                    if (!string.IsNullOrEmpty(info.Format))
                        column.Binding.StringFormat = info.Format;
                    if(!string.IsNullOrEmpty(info.Converter))
                    {
                        object res = FindResource(info.Converter);
                        if(res != null)
                            (column.Binding as Binding).Converter = (IValueConverter)res;
                        //(column.Binding as Binding).ConverterParameter = info.ConverterParameter;
                    }
                }
                if (!string.IsNullOrEmpty(info.HorizontalContentAlignment))
                {
                    switch (info.HorizontalContentAlignment)
                    {
                        case "left":
                            column.ElementStyle = leftStyle; break;
                        case "right":
                            column.ElementStyle = rightStyle; break;
                        default:
                            column.ElementStyle = leftStyle; break;
                    }
                }
                if (info.Width > 0)
                {
                    column.Width = info.Width;
                }
                if (info.MinWidth > 0)
                {
                    column.MinWidth = info.MinWidth;
                }
                gridView.Columns.Add(column);
            }
        }

        private DataGridColumn CreateColumn(DataGrid gridView, object columnSource)
        {
            DataGridTextColumn column = new DataGridTextColumn();
            var info = columnSource as DataGridColumnInfoM;

            if (!string.IsNullOrEmpty(info.Header))
            {
                column.Header = info.Header;
            }
            if (!string.IsNullOrEmpty(info.Binding))
            {

                column.Binding = new Binding(info.Binding);
            }
            if (!string.IsNullOrEmpty(info.HorizontalContentAlignment))
            {
                //column.ElementStyle = new Binding(info.HorizontalContentAlignment);
                Style right = new Style(typeof(TextBlock));
                Setter setRight = new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Right);
                right.Setters.Add(setRight);
                setRight = new Setter(TextBlock.MarginProperty, new Thickness(5, 0, 5, 0));
                right.Setters.Add(setRight);
                column.ElementStyle = right;
            }
            if (info.Width > 0)
            {
                column.Width = info.Width;
            }

            return column;
        }
        #endregion
    }
}
