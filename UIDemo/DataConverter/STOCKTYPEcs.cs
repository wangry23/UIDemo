using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UIDemo.DataConverter
{
    [ValueConversion(typeof(char), typeof(String))]
    public class STOCK_TYPE : IValueConverter
    {
        public static Dictionary<char, String> dic = new Dictionary<char, string> {
            {  '!',"其他"},
            {  '(',"认购期权"},
            {  ')',"认沽期权"},
            {  '+',"债券借贷"},
            {  '1',"股票"},
            {  '2',"封闭式基金"},
            {  '3',"国债"},
            {  '4',"企债"},
            {  '5',"可转债"},
            {  '6',"政策性金融债"},
            {  '8',"债券回购"},
            {  '9',"股票回购"},
            {  '<',"优先股转股"},
            {  '?',"基金质押"},
            {  'A',"企债标准券"},
            {  'B',"债转股"},
            {  'C',"配债"},
            {  'D',"央行票据"},
            {  'E',"存款"},
            {  'F',"开放式基金"},
            {  'G',"非政策性金融债"},
            {  'H',"通用配售权证"},
            {  'I',"买断式回购"},
            {  'J',"投票"},
            {  'K',"次级债"},
            {  'L',"次级债务"},
            {  'M',"认购权证"},
            {  'N',"认沽权证"},
            {  'O',"认购行权"},
            {  'P',"认沽行权"},
            {  'Q',"债券质押"},
            {  'R',"股指期货"},
            {  'S',"公司债"},
            {  'T',"地方债"},
            {  'U',"理财产品"},
            {  'V',"存托凭证"},
            {  'W',"国债期货"},
            {  'X',"项目自投"},
            {  'Y',"现金"},
            {  'Z',"债券认购"},
            {  '[',"信托计划"},
            {  ']',"债券预发行"},
            {  'a',"申购"},
            {  'b',"申购款"},
            {  'c',"配号"},
            {  'd',"增发"},
            {  'e',"增发款"},
            {  'f',"配售"},
            {  'g',"关联配售"},
            {  'h',"配股"},
            {  'i',"关联配股"},
            {  'j',"债券承销"},
            {  'k',"债券申购"},
            {  'l',"债券申购款"},
            {  'm',"非标"},
            {  'n',"债券增发"},
            {  'o',"债券增发款"},
            {  'p',"指定登记"},
            {  'q',"撤销指定"},
            {  'r',"回购登记"},
            {  's',"回购撤销"},
            {  't',"利率互换"},
            {  'v',"商品期货"},
            {  'w',"指数"},
            {  'x',"国债标准券"},
            {  'y',"债回售"},
            {  'z',"信用拆借"},
            {  '{',"优先股"},
            {  '}',"优先股回售"},
        };
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var key = System.Convert.ToChar(value);
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            return "未知";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = System.Convert.ToString(value);
            if (dic.ContainsValue(val))
            {
                return dic.Where(c => c.Value == val).FirstOrDefault().Key;
            }
            return null;
        }
    }
}
