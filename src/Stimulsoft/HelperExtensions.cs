using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Stimulsoft.Properties;
using Stimulsoft.Report;


namespace Clinic.Core
{
    public static class HelperExtensions
    {

        public static string ToHex(this byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);

            foreach (var t in bytes)
                result.Append(t.ToString(upperCase ? "X2" : "x2"));

            return result.ToString();
        }

        public static ExpandoObject ToExpando(this object anonymousObject)
        {
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(anonymousObject))
            {
                var obj = propertyDescriptor.GetValue(anonymousObject);
                expando.Add(propertyDescriptor.Name, obj);
            }

            return (ExpandoObject)expando;
        }


        public static dynamic DapperRowToExpando(this object value)
        {
            IDictionary<string, object> dapperRowProperties = value as IDictionary<string, object>;

            IDictionary<string, object> expando = new ExpandoObject();

            foreach (KeyValuePair<string, object> property in dapperRowProperties)
                expando.Add(property.Key, property.Value);

            return (ExpandoObject)expando;
        }
 
        public static List<ExpandoObject> GetSchemaAndData(this IDataReader reader, out List<string> schema)
        {
            schema = reader.GetSchemaTable()?.Rows
                                     .Cast<DataRow>()
                                     .Select(r => (string)r["ColumnName"])
                                     .ToList();

            var results = new List<ExpandoObject>();


            while (reader.Read())
            {
                var obj = new ExpandoObject();
                if (schema != null)
                    foreach (var col in schema)
                    {
                        var row = ((IDictionary<string, object>)obj);

                        if (string.IsNullOrEmpty(col) || row.ContainsKey(col)) continue;

                        row.Add(col, reader[col]);
                    }
                results.Add(obj);
            }

            return results;
        }

        public static List<ExpandoObject> GetSchemaAndData(this IDataReader reader)
        {
            List<string> schema;
            return reader.GetSchemaAndData(out schema);
        }

        public static DataTable ToDataTable(this IDataReader reader)
        {
            var dt = new DataTable();
            dt.Load(reader);

            return dt;
        }

        public static string GetPersianDate(this DateTime date)
        {
            var jc = new PersianCalendar();
            return
                $"{jc.GetYear(date):0000}/{jc.GetMonth(date):00}/{jc.GetDayOfMonth(date):00} {jc.GetHour(date):00}:{jc.GetMinute(date):00}:{jc.GetSecond(date):00}.{jc.GetMilliseconds(date)}";
        }

        public static string GetPersianDateByDashSpliter(this DateTime date)
        {
            var jc = new PersianCalendar();
            return
                $"{jc.GetYear(date):0000}-{jc.GetMonth(date):00}-{jc.GetDayOfMonth(date):00}";
        }

        public static long GetPersianDateNumber(this DateTime date)
        {
            var jc = new PersianCalendar();
            return
                long.Parse($"{jc.GetYear(date):0000}{jc.GetMonth(date):00}{jc.GetDayOfMonth(date):00}{jc.GetHour(date):00}{jc.GetMinute(date):00}{jc.GetSecond(date):00}{jc.GetMilliseconds(date):000}");
        }


       

        public static DataTable ToDataTable(this object[] data)
        {
            var dt = new DataTable();

            if (!data.Any()) return dt;

            // Get header
            var o = data[0] as IDictionary<string, object>;
            if (o == null) return dt;
            var dapperRowProperties = o.Keys;
            foreach (var col in dapperRowProperties)
            {
                dt.Columns.Add(col);
            }
            //
            // Get details
            foreach (IDictionary<string, object> item in data)
            {
                var row = dt.Rows.Add();
                foreach (string prop in dapperRowProperties)
                {
                    row[prop] = item[prop]?.ToString();
                }
            }

            return dt;
        }

        public static DataTable ToDataTable(this IEnumerable<dynamic> data)
        {
            return data.ToArray().ToDataTable();
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            var dt = new DataTable();
            dt.Columns.Add("Id");

            foreach (var item in data)
            {
                dt.Rows.Add(item);
            }

            return dt;
        }

        public static DataTable ToDataTable<TKey, TValue>(this IDictionary<TKey, TValue> data)
        {
            var dt = new DataTable();
            dt.Columns.Add("Key");
            dt.Columns.Add("Value");

            foreach (var item in data)
            {
                dt.Rows.Add(item.Key, item.Value);
            }

            return dt;
        }

       

        public static string RepairCipher(this string invalidString)
        {
            // some times get url convert '+' char to white space ' ' so rollback converting:
            var result = invalidString?.Replace(" ", "+");

            int mod4 = result.Length % 4;
            if (mod4 > 0)
            {
                result += new string('=', 4 - mod4);
            }

            return result;
        }

        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static List<T> ToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
 

        public static List<string> GetUserDefinedMethodsName(this Type tC)
        {
            var result = tC.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Select(mi => mi.Name).Distinct().ToList();

            return result;
        }

         
        public static short? GetUnreliableValue(short? obj, bool defaultZero = false)
        {
            if (defaultZero)
            {
                obj = 0;
            }
            return obj;
        }

        public static float? GetUnreliableValue(float? obj, bool defaultZero = false)
        {
            if (defaultZero)
            {
                obj = 0f;
            }

            return obj;
        }

        public static string GetUnreliableValue(string obj, bool defaultEmpty = false)
        {
            if (defaultEmpty)
            {
                obj = "";
            }

            return obj;
        }

        public static T GetUnreliableValue<T>(object obj, object defaultValue = null)
        {

            if (obj == null)
            {
                obj = defaultValue;
            }
            return (T)obj;
        }

        public static void LogError(this Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        public static void InitForm(this Form form)
        {
            form.AutoScaleMode =  AutoScaleMode.Font;
            form.RightToLeft = RightToLeft.Yes;
            form.WindowState = FormWindowState.Maximized;
            form.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        }

        public static DataTable ToDataTable<T>(this List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }
    }



}
