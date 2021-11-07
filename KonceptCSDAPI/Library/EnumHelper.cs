using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace KonceptSupportLibrary
{
    public static class EnumHelper
    {
        public static List<NamedKeyValuePair> GetNamedConstantAndValueOfEnum(Type type_, char? charToReplace_, char? charToBeReplacedWith_)
        {
            List<NamedKeyValuePair> listOfNamedKeyValuePair = new List<NamedKeyValuePair>();

            foreach (string value in Enum.GetNames(type_))
            {
                if (charToReplace_ != null && charToBeReplacedWith_ != null)
                    listOfNamedKeyValuePair.Add(new NamedKeyValuePair { Key = (byte)Enum.Parse(type_, value), Value = value.Replace(charToReplace_.Value, charToBeReplacedWith_.Value) });
                else
                    listOfNamedKeyValuePair.Add(new NamedKeyValuePair { Key = (object)Enum.Parse(type_, value), Value = value });
            }

            return listOfNamedKeyValuePair;
        }

        public static string GetEnumDescription(Type enumType_, object value_)
        {
            string value = Enum.GetName(enumType_, value_);
            var memInfo = enumType_.GetMember(value);
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return ((DescriptionAttribute)attributes[0]).Description;
        }
        public static IEnumerable<SelectListItem> IEnumerableEnumDescriptionAndValues(Type enumType_, SelectListItem itemToAddAtTop_ = null, SelectListItem itemToRemove_ = null)
        {
            var result = new SortedList<int, SelectListItem>();

            int counter = 0;

            if (itemToAddAtTop_ != null)
                result.Add(-1, itemToAddAtTop_);

            foreach (string value in Enum.GetNames(enumType_))
            {
                var memInfo = enumType_.GetMember(value);
                var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = ((DescriptionAttribute)descriptionAttributes[0]).Description;
                result.Add(counter++, new SelectListItem { Value = ((int)Enum.Parse(enumType_, value, true)).ToString(), Text = description });
            }

            if (itemToRemove_ != null)
            {
                if (result.Values.ToList().Exists(item => item.Value == itemToRemove_.Value))
                    result.Values.ToList().Remove(result.Values.ToList().Find(item => item.Value == itemToRemove_.Value));
            }

            return result.Values.AsEnumerable();
        }
        //Convr datatabe list tomodel list
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }


        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    //in case you have a enum/GUID datatype in your model
                    //We will check field's dataType, and convert the value in it.
                    if (pro.Name == column.ColumnName)
                    {
                        try
                        {
                            if (DBNull.Value != dr[column.ColumnName])
                            {
                                var convertedValue = GetValueByDataType(pro.PropertyType, dr[column.ColumnName]);
                                pro.SetValue(obj, convertedValue, null);
                            }
                            
                        }
                        catch (Exception e)
                        {
                            //ex handle code                   
                            throw;
                        }
                        //pro.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }
        private static object GetValueByDataType(Type propertyType, object o)
        {
            if (o.ToString() == "null")
            {
                return null;
            }
            if (propertyType == (typeof(Guid)) || propertyType == typeof(Guid?))
            {
                return Guid.Parse(o.ToString());
            }
            else if (propertyType == typeof(int) || propertyType.IsEnum)
            {
                return Convert.ToInt32(o);
            }
            else if (propertyType == typeof(decimal))
            {
                return Convert.ToDecimal(o);
            }
            else if (propertyType == typeof(long))
            {
                return Convert.ToInt64(o);
            }
            else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
            {
                return Convert.ToBoolean(o);
            }
            else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
            {
                return Convert.ToDateTime(o);
            }
            return o.ToString();
        }
    }

  
    public class NamedKeyValuePair
    {
        public object Key { get; set; }
        public object Value { get; set; }
    }
}
