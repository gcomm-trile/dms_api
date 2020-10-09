using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace albus_api
{
    public static class ConvertHelper
    {
        public static List<T> ToModel<T>(this DataTable dt)
        {
            var columns = (from DataColumn dc in dt.Columns select dc.ColumnName).ToList();

            var fields = typeof(T).GetFields();
            var properties = typeof(T).GetProperties();

            var lst = new List<T>();

            foreach (DataRow dr in dt.Rows)
            {
                var ob = Activator.CreateInstance<T>();

                foreach (var fieldInfo in fields.Where(fieldInfo => columns.Contains(fieldInfo.Name)))
                {
                    Debug.WriteLine(fieldInfo.Name);
                    fieldInfo.SetValue(ob,
                        !dr.IsNull(fieldInfo.Name)
                            ? dr[fieldInfo.Name]
                            : fieldInfo.FieldType.IsValueType
                                ? Activator.CreateInstance(fieldInfo.FieldType)
                                : null);
                }
                //var x = properties.Where(propertyInfo => columns.Contains(propertyInfo.Name));
                foreach (var propertyInfo in properties.Where(propertyInfo => columns.Contains(propertyInfo.Name)))
                {
                    //var value = !dr.IsNull(propertyInfo.Name)
                    //        ? dr[propertyInfo.Name]
                    //        : propertyInfo.PropertyType.IsValueType
                    //            ? Activator.CreateInstance(propertyInfo.PropertyType)
                    //            : null;
                    //if(propertyInfo.Name== "ParentGroupRandom" ||  propertyInfo.Name== "ParentGroupRandomChirldren")
                    //{

                    //}
                    Debug.WriteLine(propertyInfo.Name);
                    propertyInfo.SetValue(ob,
                        !dr.IsNull(propertyInfo.Name)
                            ? dr[propertyInfo.Name]
                            : propertyInfo.PropertyType.IsValueType
                                ? Activator.CreateInstance(propertyInfo.PropertyType)
                                : null);
                }
                lst.Add(ob);
            }

            return lst;
        }
    }
}
