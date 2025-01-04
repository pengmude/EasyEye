using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLib
{
    /// <summary>
    /// IControlEx 控件扩展接口
    /// </summary>
    public interface IControlEx
    {
        /// <summary>
        /// 变量名称
        /// </summary>
        string VariableName { get; set; }

        /// <summary>
        /// 对象的类名
        /// </summary>
        string ObjectClassName { get; set; }

        /// <summary>
        /// 使用数据绑定
        /// </summary>
        bool IsUseDataBinding { get; set; }

        /// <summary>
        /// 数据源绑定
        /// </summary>
        /// <param name="AlldataSouces"></param>
        void SetDataBinding(object[] AlldataSouces);


        /// <summary>
        /// 获取数据,IsUseDataBinding为false才使用
        /// </summary>
        /// <param name="AlldataSouces"></param>
        void GettData(object[] AlldataSouces);
    }
}
