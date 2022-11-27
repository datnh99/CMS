using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CMS_SU21_BE.Services.Implements
{
    public class ExcelServiceImpl : ExcelService
    {
        public void WriteData<T>(List<T> exportData, ISheet _sheet, List<string> _headers)
        {
            List<string> _type = new List<string>();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
            {
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                _type.Add(type.Name);
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ??
                                  prop.PropertyType);
                string name = Regex.Replace(prop.Name, "([A-Z])", " $1").Trim();
                //name by caps for header
                _headers.Add(name.ToUpper());
            }

            foreach (T item in exportData)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);

            }

            IRow sheetRow = null;

            for (int i = 0; i < table.Rows.Count; i++)
            {
                sheetRow = _sheet.CreateRow(i + 1);
                _sheet.AutoSizeColumn(i);
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    ICell Row1 = sheetRow.CreateCell(j);
                    string cellvalue = Convert.ToString(table.Rows[i][j]);

                    // TODO: move it to switch case

                    if (string.IsNullOrWhiteSpace(cellvalue))
                    {
                        Row1.SetCellValue(string.Empty);
                    }
                    else if (_type[j].ToLower() == "string")
                    {
                        Row1.SetCellValue(cellvalue);
                    }
                    else if (_type[j].ToLower() == "int32")
                    {
                        Row1.SetCellValue(Convert.ToInt32(table.Rows[i][j]));
                    }
                    else if (_type[j].ToLower() == "double")
                    {
                        Row1.SetCellValue(Convert.ToDouble(table.Rows[i][j]));
                    }
                    else if (_type[j].ToLower() == "datetime")
                    {
                        Row1.SetCellValue(Convert.ToDateTime
                             (table.Rows[i][j]).ToString("dd/MM/yyyy hh:mm:ss"));
                    }
                    else
                    {
                        Row1.SetCellValue(string.Empty);
                    }
                }
            }
        }
    }
}