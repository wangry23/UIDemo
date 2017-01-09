using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UIDemo.ViewModel
{
    public class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase
    {
        //值目录
        private Dictionary<string, object> values;

        public ViewModelBase()
        {
            this.values = new Dictionary<string, object>();
        }

        /// <summary>
        /// 获得属性名称
        /// </summary>
        /// <param name="lambda">lambda表达式</param>
        /// <returns></returns>
        private string GetPropertyName(LambdaExpression lambda)
        {
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambda.Body as MemberExpression;
            }
            var constantExpression = memberExpression.Expression as ConstantExpression;
            var propertyInfo = memberExpression.Member as PropertyInfo;
            return propertyInfo.Name;
        }

        /// <summary>
        /// 赋值（泛型）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">属性（当前属性）</param>
        /// <param name="value">值</param>
        protected void SetValue<T>(Expression<Func<T>> property, T value)
        {

            LambdaExpression lambda = property as LambdaExpression;

            if (lambda == null)
                throw new ArgumentException("无效的视图模型的属性定义.");

            string propertyName = this.GetPropertyName(lambda);

            T existingValue = GetValueInternal<T>(propertyName);

            if (object.Equals(existingValue, value))
            {
                return;
            }
            this.values[propertyName] = value;
            this.RaisePropertyChanged<T>(propertyName, existingValue, value);
        }

        /// <summary>
        /// 获得值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">属性（当前属性）</param>
        /// <returns></returns>
        protected T GetValue<T>(Expression<Func<T>> property)
        {
            LambdaExpression lambda = property as LambdaExpression;

            if (lambda == null)
                throw new ArgumentException("无效的视图模型属性定义.");

            string propertyName = this.GetPropertyName(lambda);

            return GetValueInternal<T>(propertyName);

        }

        private T GetValueInternal<T>(string propertyName)
        {
            object value;
            if (values.TryGetValue(propertyName, out value))
                return (T)value;
            else
                return default(T);
        }
    }
}
