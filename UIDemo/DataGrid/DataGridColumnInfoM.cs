using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIDemo.MyDataGrid
{
    public class DataGridColumnInfoM
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Header { get; set; }
        /// <summary>
        /// 行显示的值。
        /// </summary>
        public string Binding { get; set; }
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
}
