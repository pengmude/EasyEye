using System;
using System.Collections.Specialized;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace SmartVEye
{
    /// <summary>
    ///     ini文件工具类
    /// </summary>
    public class IniFileHelper
    {
        #region INI文件操作
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(byte[] section, byte[] key, byte[] val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(byte[] section, byte[] key, byte[] def, byte[] retVal, int size, string filePath);

        /// <summary>  
        /// 获取所有节点名称
        /// </summary>  
        /// <param name="lpszReturnBuffer">存放节点名称的内存地址,每个节点之间用\0分隔</param>  
        /// <param name="nSize">内存大小(characters)</param>  
        /// <param name="lpFileName">Ini文件</param>  
        /// <returns>内容的实际长度,为0表示没有内容,为nSize-2表示内存大小不够</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint
        GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, uint nSize, string lpFileName);

        /// <summary>
        /// 设定INI文件中的属性，如果section不存在，则自动创建该section，如果key不存在，则自动创建该key
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="filePath">INI文件的绝对地址</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary>
        /// 读取INI文件中指定section的值
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <param name="retVal"></param>
        /// <param name="size"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal,
            int size, string filePath);

        /// <summary>
        /// 删除指定的key
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool DelIniFileKey(string section, string key, string filePath)
        {
            bool result = false;
            if (WritePrivateProfileString(section, key, null, filePath) != 0)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 删除指定的section
        /// </summary>
        /// <param name="section"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool DelIniFileSection(string section, string filePath)
        {
            bool result = false;
            if (WritePrivateProfileString(section, null, null, filePath) != 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 从Ini文件中，将指定的Section名称中的所有key添加到字符串数组
        /// </summary>
        /// <param name="Section">段</param>
        /// <param name="filePath">文件地址</param>
        /// <returns></returns>
        public static string[] ReadKeys(string Section, string filePath)
        {
            byte[] Buffer = new byte[1024 * 2];
            int bufLen = GetPrivateProfileString(Section, null, null, Buffer, Buffer.GetUpperBound(0), filePath);
            ////对Section进行解析
            //return GetStringsFromBuffer(Buffer, bufLen);
            StringCollection Strings = new StringCollection();
            if (bufLen != 0)
            {
                int start = 0;
                for (int i = 0; i < bufLen; i++)
                {
                    if ((Buffer[i] == 0) && ((i - start) > 0))
                    {
                        String s = Encoding.UTF8.GetString(Buffer, start, i - start);
                        //String s = Encoding.Default.GetString(Buffer, start, i - start);
                        // s = Encoding.GetEncoding(0).GetString(Buffer, start, i - start);
                        Strings.Add(s);
                        start = i + 1;
                    }
                }
            }

            if (Strings.Count > 0)
            {
                string[] returnArrey = new string[Strings.Count];
                for (int i = 0; i < Strings.Count; i++)
                {
                    returnArrey[i] = Strings[i];
                }

                return returnArrey;
            }
            else
                return null;
        }

        /// <summary>
        /// 从Ini文件中，所有Section添加到字符串数组
        /// </summary>
        /// <param name="iniFile"></param>
        /// <returns></returns>
        public static string[] ReadSections(string iniFile)
        {
            uint MAX_BUFFER = 32767; //默认为32767  

            string[] sections = new string[0]; //返回值  

            //申请内存  
            IntPtr pReturnedString = Marshal.AllocCoTaskMem((int)MAX_BUFFER * sizeof(char));
            uint bytesReturned = GetPrivateProfileSectionNames(pReturnedString, MAX_BUFFER, iniFile);
            if (bytesReturned != 0)
            {
                //读取指定内存的内容  
                string local = Marshal.PtrToStringAuto(pReturnedString, (int)bytesReturned).ToString();
                //每个节点之间用\0分隔,末尾有一个\0  
                sections = local.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }

            //释放内存  
            Marshal.FreeCoTaskMem(pReturnedString);

            return sections;
        }

        //与ini交互必须统一编码格式
        private static byte[] getBytes(string s, string encodingName)
        {
            return null == s ? null : Encoding.GetEncoding(encodingName).GetBytes(s);
        }

        private static string ReadString(string fileName, string section, string key, string def, string encodingName = "utf-8", int size = 1024)
        {
            byte[] buffer = new byte[size];
            int count = GetPrivateProfileString(getBytes(section, encodingName), getBytes(key, encodingName), getBytes(def, encodingName), buffer, size, fileName);
            return Encoding.GetEncoding(encodingName).GetString(buffer, 0, count).Trim();
        }
        private static bool WriteString(string section, string key, string value, string fileName, string encodingName = "utf-8")
        {
            return WritePrivateProfileString(getBytes(section, encodingName), getBytes(key, encodingName), getBytes(value, encodingName), fileName);
        }
        #endregion

        #region 新式INI读写

        /// <summary>
        /// INI保存路径
        /// </summary>
        public static string INI_FilePath = "";

        /// <summary>
        /// 设置INI保存的路径
        /// </summary>
        /// <param name="filePath"></param>
        public static void SetINISaveFilePath(string filePath)
        {
            INI_FilePath = filePath;
        }

        #region 写入INI
        /// <summary>
        ///写入INI,保存变量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"> ()=> 变量</param>
        public static void SaveINI<T>(Expression<Func<T>> data)
        {
            if (typeof(T).IsClass && typeof(T) != typeof(string))
                throw new Exception("不能传对象,只能传值类型!");

            var me = data.Body as MemberExpression;
            if (me == null)
            {
                var me2 = data.Body as BinaryExpression;
                me = me2.Left as MemberExpression;
            }
            var name = me.Member.Name;
            object value = data.Compile().Invoke();
            WriteString("section", name, value.ToString(), INI_FilePath);
        }


        /// <summary>
        ///写入INI,保存数组变量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"> ()=> 数组变量</param>
        public static void SaveINI<T>(Expression<Func<T[]>> data)
        {
            if (typeof(T).IsClass && typeof(T) != typeof(string))
                throw new Exception("不能传对象,只能传值类型!");

            var me = data.Body as MemberExpression;
            if (me == null)
            {
                var me2 = data.Body as BinaryExpression;
                me = me2.Left as MemberExpression;
            }
            var name = me.Member.Name;
            object value = data.Compile().Invoke();
            T[] TheData = (T[])value;
            for (int i = 0; i < TheData.Length; i++)
                WriteString(name, i.ToString(), TheData[i].ToString(), INI_FilePath);
        }



        /// <summary>
        /// 保存INI,普通版
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="section">段</param>
        /// <param name="key">键</param>
        /// <param name="Value">保存的值</param>
        public static void SaveINI<T>(string section, string key, T Value)
        {
            if (typeof(T).IsClass && typeof(T) != typeof(string))
                throw new Exception("不能传对象,只能传值类型!");
            WriteString(section, key, Value.ToString(), INI_FilePath);
        }

        #endregion

        #region 写入INI要输入地址
        /// <summary>
        /// 写入INI要输入地址
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">地址</param>
        /// <param name="data"></param>
        public static void SaveINI<T>(string filePath, Expression<Func<T>> data)
        {
            if (typeof(T).IsClass && typeof(T) != typeof(string))
                throw new Exception("不能传对象,只能传值类型!");

            var me = data.Body as MemberExpression;
            if (me == null)
            {
                var me2 = data.Body as BinaryExpression;
                me = me2.Left as MemberExpression;
            }
            var name = me.Member.Name;
            object value = data.Compile().Invoke();
            WriteString("section", name, value.ToString(), filePath);
        }


        /// <summary>
        /// 写入INI要输入地址
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">地址</param>
        /// <param name="data"></param>
        public static void SaveINI<T>(string filePath, Expression<Func<T[]>> data)
        {
            if (typeof(T).IsClass && typeof(T) != typeof(string))
                throw new Exception("不能传对象,只能传值类型!");

            var me = data.Body as MemberExpression;
            if (me == null)
            {
                var me2 = data.Body as BinaryExpression;
                me = me2.Left as MemberExpression;
            }
            var name = me.Member.Name;
            object value = data.Compile().Invoke();
            T[] TheData = (T[])value;
            for (int i = 0; i < TheData.Length; i++)
                WriteString(name, i.ToString(), TheData[i].ToString(), filePath);
        }



        /// <summary>
        /// 保存INI,普通版,要输入地址
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">地址</param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="Value"></param>
        public static void SaveINI<T>(string filePath, string section, string key, T Value)
        {
            if (typeof(T).IsClass && typeof(T) != typeof(string))
                throw new Exception("不能传对象,只能传值类型!");

            WriteString(section, key, Value.ToString(), filePath);
        }

        #endregion

        #region 读INI
        /// <summary>
        /// 读取INI,输入静态变量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">()=> 静态变量</param>
        public static void ReadINI<T>(Expression<Func<T>> data)
        {
            if (typeof(T).IsClass && typeof(T) != typeof(string))
                throw new Exception("不能传对象,只能传值类型!");
            var me = data.Body as MemberExpression;
            if (me == null)
            {
                var me2 = data.Body as BinaryExpression;
                me = me2.Left as MemberExpression;
            }
            var name = me.Member.Name;
            var value = data.Compile().Invoke();
            var result = ReadString(INI_FilePath, "section", name, value.ToString());
            //var result = GetStringValue(INI_FilePath, "section", name, value.ToString());
            value = (T)Convert.ChangeType(result, typeof(T));

            ////通过反射赋值
            BindingFlags flag = BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance;
            var f_key = me.Member.ReflectedType.GetField(name, flag);
            if (f_key != null)
                f_key.SetValue(me.Member.ReflectedType, value);
            else
            {
                var f_key2 = me.Member.ReflectedType.GetProperty(name);
                if (f_key2 != null)
                    f_key2.SetValue(me.Member.ReflectedType, value);
            }

        }

        /// <summary>
        /// 读取INI,输入静态数组变量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">()=> 静态数组变量</param>
        public static void ReadINI<T>(Expression<Func<T[]>> data)
        {
            if (typeof(T).IsClass && typeof(T) != typeof(string))
                throw new Exception("不能传对象,只能传值类型!");

            var me = data.Body as MemberExpression;
            if (me == null)
            {
                var me2 = data.Body as BinaryExpression;
                me = me2.Left as MemberExpression;
            }

            var name = me.Member.Name;
            object value = data.Compile().Invoke();
            T[] TheData = (T[])value;
            for (int i = 0; i < TheData.Length; i++)
            {
                var result = ReadString(INI_FilePath, name, i.ToString(), TheData[i].ToString());
                //var result = GetStringValue(INI_FilePath, name, i.ToString(), TheData[i].ToString());
                TheData[i] = (T)Convert.ChangeType(result, typeof(T));
            }

            ////通过反射赋值
            BindingFlags flag = BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance;
            var f_key = me.Member.ReflectedType.GetField(name, flag);
            if (f_key != null)
                f_key.SetValue(me.Member.ReflectedType, TheData);
            else
            {
                var f_key2 = me.Member.ReflectedType.GetProperty(name);
                if (f_key2 != null)
                    f_key2.SetValue(me.Member.ReflectedType, TheData);
            }
        }

        /// <summary>
        /// 读INI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T ReadINI<T>(string section, string key, T defaultValue)
        {
            if (typeof(T).IsClass && typeof(T) != typeof(string))
                throw new Exception("不能传对象,只能传值类型!");

            var value = ReadString(INI_FilePath, section, key, defaultValue.ToString());
            T s = (T)Convert.ChangeType(value, typeof(T));
            return s;
        }

        #endregion

        #region 读INI要输文件地址
        /// <summary>
        /// 读取INI,输入静态变量,要输文件地址
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">地址</param>
        /// <param name="data"></param>
        public static void ReadINI<T>(string filePath, Expression<Func<T>> data)
        {
            if (typeof(T).IsClass && typeof(T) != typeof(string))
                throw new Exception("不能传对象,只能传值类型!");
            var me = data.Body as MemberExpression;
            if (me == null)
            {
                var me2 = data.Body as BinaryExpression;
                me = me2.Left as MemberExpression;
            }
            var name = me.Member.Name;
            var value = data.Compile().Invoke();
            var result = ReadString(filePath, "section", name, value.ToString());
            value = (T)Convert.ChangeType(result, typeof(T));

            ////通过反射赋值
            BindingFlags flag = BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance;
            var f_key = me.Member.ReflectedType.GetField(name, flag);
            if (f_key != null)
                f_key.SetValue(me.Member.ReflectedType, value);
            else
            {
                var f_key2 = me.Member.ReflectedType.GetProperty(name);
                if (f_key2 != null)
                    f_key2.SetValue(me.Member.ReflectedType, value);
            }
        }

        /// <summary>
        /// 读取INI,输入静态变量,要输文件地址
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">地址</param>
        /// <param name="data"></param>
        public static void ReadINI<T>(string filePath, Expression<Func<T[]>> data)
        {
            if (typeof(T).IsClass && typeof(T) != typeof(string))
                throw new Exception("不能传对象,只能传值类型!");

            var me = data.Body as MemberExpression;
            if (me == null)
            {
                var me2 = data.Body as BinaryExpression;
                me = me2.Left as MemberExpression;
            }

            var name = me.Member.Name;
            object value = data.Compile().Invoke();
            T[] TheData = (T[])value;
            for (int i = 0; i < TheData.Length; i++)
            {
                var result = ReadString(filePath, name, i.ToString(), TheData[i].ToString());
                TheData[i] = (T)Convert.ChangeType(result, typeof(T));
            }

            ////通过反射赋值
            BindingFlags flag = BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance;
            var f_key = me.Member.ReflectedType.GetField(name, flag);
            if (f_key != null)
                f_key.SetValue(me.Member.ReflectedType, TheData);
            else
            {
                var f_key2 = me.Member.ReflectedType.GetProperty(name);
                if (f_key2 != null)
                    f_key2.SetValue(me.Member.ReflectedType, TheData);
            }
        }

        /// <summary>
        /// 读取INI,要输文件地址
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">地址</param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T ReadINI<T>(string filePath, string section, string key, T defaultValue)
        {
            if (typeof(T).IsClass && typeof(T) != typeof(string))
                throw new Exception("不能传对象,只能传值类型!");

            var value = ReadString(filePath, section, key, defaultValue.ToString());
            T s = (T)Convert.ChangeType(value, typeof(T));
            return s;
        }

        #endregion

        #region 缺陷,专门用来读动态对象的INI

        /// <summary>
        /// 读INI,读取动态对象变量(缺陷)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="data"></param>
        public static void ReadINI<T>(object instance, Expression<Func<T>> data)
        {
            if (typeof(T).IsClass && typeof(T) != typeof(string))
                throw new Exception("不能传对象,只能传值类型!");
            var me = data.Body as MemberExpression;
            if (me == null)
            {
                var me2 = data.Body as BinaryExpression;
                me = me2.Left as MemberExpression;
            }
            var name = me.Member.Name;
            var value = data.Compile().Invoke();
            var result = ReadString(INI_FilePath, "section", name, value.ToString());
            value = (T)Convert.ChangeType(result, typeof(T));
            //通过反射赋值
            var f_key = instance.GetType().GetProperty(name);
            if (f_key != null)
                f_key.SetValue(instance, value);
            else
            {
                var f_key2 = instance.GetType().GetField(name);
                if (f_key2 != null)
                    f_key2.SetValue(instance, value);
            }
        }


        /// <summary>
        /// 读INI数组,读取动态对象变量(缺陷)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="data"></param>
        public static void ReadINI<T>(object instance, Expression<Func<T[]>> data)
        {
            if (typeof(T).IsClass && typeof(T) != typeof(string))
                throw new Exception("不能传对象,只能传值类型!");

            var me = data.Body as MemberExpression;
            if (me == null)
            {
                var me2 = data.Body as BinaryExpression;
                me = me2.Left as MemberExpression;
            }

            var name = me.Member.Name;
            object value = data.Compile().Invoke();
            T[] TheData = (T[])value;
            for (int i = 0; i < TheData.Length; i++)
            {
                var result = ReadString(INI_FilePath, name, i.ToString(), TheData[i].ToString());
                TheData[i] = (T)Convert.ChangeType(result, typeof(T));
            }
            var f_key = instance.GetType().GetProperty(name);
            if (f_key != null)
                f_key.SetValue(instance, value);
            else
            {
                var f_key2 = instance.GetType().GetField(name);
                if (f_key2 != null)
                    f_key2.SetValue(instance, value);
            }
        }

        /// <summary>
        /// 读INI,读取动态对象变量(缺陷)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="data"></param>
        public static void ReadINI<T>(string filePath, object instance, Expression<Func<T>> data)
        {
            if (typeof(T).IsClass && typeof(T) != typeof(string))
                throw new Exception("不能传对象,只能传值类型!");
            var me = data.Body as MemberExpression;
            if (me == null)
            {
                var me2 = data.Body as BinaryExpression;
                me = me2.Left as MemberExpression;
            }
            var name = me.Member.Name;
            var value = data.Compile().Invoke();
            var result = ReadString(filePath, "section", name, value.ToString());
            value = (T)Convert.ChangeType(result, typeof(T));
            //通过反射赋值
            var f_key = instance.GetType().GetProperty(name);
            if (f_key != null)
                f_key.SetValue(instance, value);
            else
            {
                var f_key2 = instance.GetType().GetField(name);
                if (f_key2 != null)
                    f_key2.SetValue(instance, value);
            }
        }


        /// <summary>
        /// 读INI数组,读取动态对象变量(缺陷)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="data"></param>
        public static void ReadINI<T>(string filePath, object instance, Expression<Func<T[]>> data)
        {
            if (typeof(T).IsClass && typeof(T) != typeof(string))
                throw new Exception("不能传对象,只能传值类型!");

            var me = data.Body as MemberExpression;
            if (me == null)
            {
                var me2 = data.Body as BinaryExpression;
                me = me2.Left as MemberExpression;
            }

            var name = me.Member.Name;
            object value = data.Compile().Invoke();
            T[] TheData = (T[])value;
            for (int i = 0; i < TheData.Length; i++)
            {
                var result = ReadString(filePath, name, i.ToString(), TheData[i].ToString());
                TheData[i] = (T)Convert.ChangeType(result, typeof(T));
            }
            var f_key = instance.GetType().GetProperty(name);
            if (f_key != null)
                f_key.SetValue(instance, value);
            else
            {
                var f_key2 = instance.GetType().GetField(name);
                if (f_key2 != null)
                    f_key2.SetValue(instance, value);

                var f_key3 = instance.GetType().GetMember(name);

            }
        }
        #endregion

        #endregion
    }
}