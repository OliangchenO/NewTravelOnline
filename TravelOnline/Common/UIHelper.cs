using Belinda.Jasp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TravelOnline
{
    public class UIHelper
    {
        /// <summary>
        /// 时间格式转日期格式
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="TimeFormat"></param>
        /// <returns></returns>
        public static string ToDate(string datetime)
        {
            DateTime now; string str = string.Empty;
            if (DateTime.TryParse(datetime, out now))
            {
                str = now.GetDateTimeFormats('D')[0].ToString();
            }
            return str;
        }

        #region 填充对象

        /// <summary>  
        /// 填充对象列表：用DataTable填充实体类
        /// </summary>  
        public static List<T> FillModel<T>(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            List<T> modelList = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T model = (T)Activator.CreateInstance(typeof(T));
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
                    if (propertyInfo != null && dr[i] != DBNull.Value)
                        propertyInfo.SetValue(model, dr[i], null);
                }
                modelList.Add(model);
            }
            return modelList;
        }
        /// <summary>  
        /// 填充对象：用DataRow填充实体类
        /// </summary>  
        public static T FillModel<T>(DataRow dr)
        {
            if (dr == null)
            {
                return default(T);
            }

            T model = (T)Activator.CreateInstance(typeof(T));

            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
                if (propertyInfo != null && dr[i] != DBNull.Value)
                    propertyInfo.SetValue(model, dr[i], null);
            }
            return model;
        }/// <summary>
         /// 实体类转换成DataSet
         /// </summary>
         /// <param name="modelList">实体类列表</param>
         /// <returns></returns>
        public static DataSet FillDataSet<T>(List<T> modelList)
        {
            if (modelList == null || modelList.Count == 0)
            {
                return null;
            }
            else
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(FillDataTable(modelList));
                return ds;
            }
        }

        /// <summary>
        /// 实体类转换成DataTable
        /// </summary>
        /// <param name="modelList">实体类列表</param>
        /// <returns></returns>
        public static DataTable FillDataTable<T>(List<T> modelList)
        {
            if (modelList == null || modelList.Count == 0)
            {
                return null;
            }
            DataTable dt = CreateData(modelList[0]);

            foreach (T model in modelList)
            {
                DataRow dataRow = dt.NewRow();
                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    dataRow[propertyInfo.Name] = propertyInfo.GetValue(model, null);
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        /// <summary>
        /// 根据实体类得到表结构
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public static DataTable CreateData<T>(T model)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                if (propertyInfo.PropertyType.UnderlyingSystemType.ToString() == "System.Nullable`1[System.Int32]")
                {
                    var column = new DataColumn(propertyInfo.Name, typeof(Int32));
                    dataTable.Columns.Add(column);
                }
                else if (propertyInfo.PropertyType.UnderlyingSystemType.ToString() == "System.Nullable`1[System.DateTime]")
                {
                    var column = new DataColumn(propertyInfo.Name, typeof(DateTime));
                    dataTable.Columns.Add(column);
                }
                else
                {
                    dataTable.Columns.Add(new DataColumn(propertyInfo.Name, propertyInfo.PropertyType));
                }
            }
            return dataTable;
        }

        /// <summary>
        /// 填充对象：用数组填充实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T ArrayToModel<T>(string[] array)
        {
            if (array == null)
            {
                return default(T);
            }
            T model = (T)Activator.CreateInstance(typeof(T));
            for (int i = 0; i < array.Length; i++)
            {
                PropertyInfo propertyInfo = model.GetType().GetProperty(array[i]);
                if (propertyInfo != null && !string.IsNullOrEmpty(array[i]))
                    propertyInfo.SetValue(model, array[i], null);
            }
            return model;
        }

        /// <summary>
        /// 利用反射将DataTable转换为List<T>对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(DataTable dt)
        {
            //定义集合
            List<T> ts = new List<T>();
            //定义一个临时变量
            string tempName = string.Empty;
            //遍历DataTable所有的数据行
            foreach (DataRow dr in dt.Rows)
            {
                T t = (T)Activator.CreateInstance(typeof(T));
                //获得此模型的公共属性
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历该对象的所有属性
                foreach (PropertyInfo pi in propertys)
                {
                    //将属性名赋值给临时变量
                    tempName = pi.Name;
                    //检查DataTable是否包含此列
                    if (dt.Columns.Contains(tempName))
                    {
                        //取值
                        object value = dr[tempName];
                        //如果非空，则赋给对象的属性
                        if (value != DBNull.Value)
                        {
                            pi.SetValue(t, value, null);
                        }
                    }
                }
                //对象添加到泛型集合中
                ts.Add(t);
            }
            return ts;
        }

        /// <summary>
        /// 利用反射将List<T>对象转换为DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    if (pi.PropertyType.UnderlyingSystemType.ToString() == "System.Nullable`1[System.Int32]")
                    {
                        var column = new DataColumn(pi.Name, typeof(Int32));
                        result.Columns.Add(column);
                    }
                    else if (pi.PropertyType.UnderlyingSystemType.ToString() == "System.Nullable`1[System.DateTime]")
                    {
                        var column = new DataColumn(pi.Name, typeof(DateTime));
                        result.Columns.Add(column);
                    }
                    else if (pi.PropertyType.UnderlyingSystemType.ToString() == "System.Nullable`1[System.Byte]")
                    {
                        var column = new DataColumn(pi.Name, typeof(Byte));
                        result.Columns.Add(column);
                    }
                    else
                    {
                        result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        /// <summary>    
        /// Datatable转换为Json    
        /// </summary>    
        /// <param name="table">Datatable对象</param>    
        /// <returns>Json字符串</returns>  
        public static string ToJson(DataTable dt)
        {
            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("[");
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString();
                    Type type = dt.Columns[j].DataType;
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = String.Format(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            return jsonString.ToString();
        }

        #endregion

        /// <summary>
        /// 两个实体对象互相复制
        /// </summary>
        /// <typeparam name="U">源实体对象类型</typeparam>
        /// <typeparam name="T">目的实体对象类型</typeparam>
        /// <param name="u">源实体对象</param>
        /// <param name="t">目的实体对象</param>
        /// <returns></returns>
        public static T Copy<U, T>(U u, T t)
        {
            //数据源
            PropertyInfo[] propertys = u.GetType().GetProperties();
            //目的实体对象
            PropertyInfo[] propertyDest = t.GetType().GetProperties();
            string name = string.Empty;
            foreach (PropertyInfo pi in propertys)
            {
                if (pi.PropertyType.IsArray)
                {
                    continue;
                }

                name = pi.Name.ToLower();

                object obj = pi.GetValue(u, null);
                foreach (PropertyInfo piDest in propertyDest)
                {
                    if (name == piDest.Name.ToLower())
                    {
                        piDest.SetValue(t, obj, null);
                    }
                }
            }
            return t;
        }

        #region Dictionary键值转换
        /// <summary>
        /// 把字典中的键转换为值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string DictionaryKeyToValue(string key, IDictionary<string, string> dic)
        {
            try
            {
                if (dic.Keys.Contains(key))
                {
                    return dic[key];
                }
                return key;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 把字典中的值转换为键
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string DictionaryValueToKey(string name, IDictionary<string, string> dic)
        {
            try
            {
                foreach (string key in dic.Keys)
                {
                    if (dic[key] == name)
                    {
                        return key;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return name;
        }
        #endregion

        /// <summary>
        /// 获取枚举类型的数值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="strType">枚举类型的名称</param>
        /// <returns></returns>
        public static string GetEnumValue<T>(string name)
        {
            try
            {
                int enumParseInt = -1;
                if (Enum.IsDefined(typeof(T), name))
                {

                    enumParseInt = Convert.ToInt32(Enum.Parse(typeof(T), name));
                }
                else
                {
                    return string.Empty;
                }
                return enumParseInt.ToString();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取枚举类型的名称
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="strType">枚举类型的数值</param>
        /// <returns></returns>
        public static string GetEnumName<T>(string strType)
        {
            string name = string.Empty;
            try
            {
                string[] names = Enum.GetNames(typeof(T));
                List<string> list = names.ToList<string>();
                if (list.Contains(strType))
                {
                    name = Enum.GetName(typeof(T), As<int>(strType));
                }
                return name;
            }
            catch (System.Exception ex)
            {
                throw ex;

            }
        }

        public static T As<T>(object value)
        {
            return As(value, default(T), CultureInfo.CurrentCulture);
        }
        public static T As<T>(Object value, T defaultValue, IFormatProvider provider)
        {
            if (value == null || value == System.DBNull.Value)
                return defaultValue;

            if (provider == null)
                provider = CultureInfo.CurrentCulture;

            T retval;

            try
            {
                retval = (T)Convert.ChangeType(value, Type.GetTypeCode(typeof(T)), provider);
            }
            catch (InvalidCastException ice)
            {
                retval = defaultValue;
                throw ice;

            }
            catch (FormatException fe)
            {
                retval = defaultValue;
                throw fe;
            }

            return retval;
        }
    }
}
