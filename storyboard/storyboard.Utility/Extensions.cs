using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Data;
using System.Reflection;
using System.Globalization;

namespace System
{
    public static class MyExtensionMethods
    {
        /// <summary>
        /// Method to avoid calling StringHelper.ToString()
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string ToStringSafe(this object s)
        {
            if (s == null) return string.Empty;
            else
            {
                string result = s.ToString();
                if (result == null) return string.Empty;
                else return result;
            }
        }

        /// <summary>
        /// Determines whether [is not empty] [the specified s].
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>
        /// 	<c>true</c> if [is not empty] [the specified s]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNotEmpty(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// Converts the string to proper case.
        /// </summary>
        /// <param name="TextToFormat">The text to format.</param>
        /// <returns></returns>
        public static string ToProperCase(this string TextToFormat)
        {
            string properCase = string.Empty;
            if (TextToFormat != null)
            {
                properCase = new CultureInfo("en").TextInfo.ToTitleCase(TextToFormat.ToLower());
            }
            return properCase;
        }

        /// <summary>
        /// Converts the string to int
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static int ToIntSafe(this object s)
        {
            int outParameter = -1;
            if (s != null)
            {
                int.TryParse(s.ToString().Trim(), out outParameter);
            }

            return outParameter;
        }

        /// <summary>
        /// Toes the decimal safe.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <returns></returns>
        public static decimal ToDecimalSafe(this object val)
        {
            decimal outParameter = 0M;
            if (val != null)
            {
                decimal.TryParse(val.ToStringSafe(), out outParameter);
            }
            return outParameter;
        }

        public static bool ToBooleanSafe(this object val)
        {
            bool result;
            if (val != null)
            {
                bool.TryParse(val.ToStringSafe(), out result);
            }
            else
            {
                result = false;
            }
            return result;
        }

        public static IEnumerable<Control> GetAllControls(this Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                yield return control;
                foreach (Control descendant in control.GetAllControls())
                {
                    yield return descendant;
                }
            }
        }
        /// <summary>
        /// Log this exception to log4net and send email to developers
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="oType">type of method</param>
        public static void Log(this Exception ex, Type oType)
        {
            //Log exception in Exception Logger.
            log4net.LogManager.GetLogger(oType).Error(ex.Message, ex);
        }
        /// <summary>
        /// Truncate string after some length
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string TruncateAtWord(this string input, int length)
        {
            if (input == null || input.Length < length)
                return input;
            int iNextSpace = input.LastIndexOf(" ", length);
            return string.Format("{0}...", input.Substring(0, (iNextSpace > 0) ? iNextSpace : length).Trim());
        }

        /// <summary>
        /// Convert datatable to entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> ConvertToList<T>(this DataTable table) where T : new()
        {
            Type t = typeof(T);

            // Create a list of the entities we want to return
            List<T> returnObject = new List<T>();

            // Iterate through the DataTable's rows
            foreach (DataRow dr in table.Rows)
            {
                // Convert each row into an entity object and add to the list
                T newRow = dr.ConvertToEntity<T>();
                returnObject.Add(newRow);
            }

            // Return the finished list
            return returnObject;
        }

        /// <summary>
        /// Convert datarow to entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableRow"></param>
        /// <returns></returns>
        public static T ConvertToEntity<T>(this DataRow tableRow) where T : new()
        {
            // Create a new type of the entity I want
            Type t = typeof(T);
            T returnObject = new T();

            foreach (DataColumn col in tableRow.Table.Columns)
            {
                string colName = col.ColumnName;

                // Look for the object's property with the columns name, ignore case
                PropertyInfo pInfo = t.GetProperty(colName.ToLower(),
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                // did we find the property ?
                if (pInfo != null)
                {
                    object val = tableRow[colName];

                    // is this a Nullable<> type
                    bool IsNullable = (Nullable.GetUnderlyingType(pInfo.PropertyType) != null);
                    if (IsNullable)
                    {
                        if (val is System.DBNull)
                        {
                            val = null;
                        }
                        else
                        {
                            // Convert the db type into the T we have in our Nullable<T> type
                            val = Convert.ChangeType
                    (val, Nullable.GetUnderlyingType(pInfo.PropertyType));
                        }
                    }
                    else
                    {
                        // Convert the db type into the type of the property in our entity
                        val = Convert.ChangeType(val, pInfo.PropertyType);
                    }
                    // Set the value of the property with the value from the db
                    pInfo.SetValue(returnObject, val, null);
                }
            }

            // return the entity object with values
            return returnObject;
        }

        /// <summary>
        /// To check whether the is numberic or not
        /// </summary>
        /// <param name="s">THe s.</param>
        /// <returns></returns>
        public static bool IsNumeric(this string s)
        {
            float output;
            return float.TryParse(s, out output);
        }
        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        public static string StripHTMLTags(this string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }


        public static string StripCarriageCharacter(this string str)
        {
            str = string.Join(" ", System.Text.RegularExpressions.Regex.Split(str, @"(?:\r\n|\n|\r)"));
            return str;
        }
    }
}
