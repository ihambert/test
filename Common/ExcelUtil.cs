using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Common
{
    /// <summary>
    ///     Excel2007/2003数据导入导出助手类
    /// </summary>
    public class ExcelUtil
    {
        /// <summary>
        ///     Excel转换成DataSet
        /// </summary>
        /// <param name="excelPath">excel文件路径</param>
        /// <returns>返回DataSet</returns>
        public static DataSet ExcelToDataSet(string excelPath)
        {
            return ExcelToDataSet(excelPath, true);
        }

        /// <summary>
        ///     Excel转换成DataSet
        /// </summary>
        /// <param name="excelPath">excel文件路径</param>
        /// <param name="firstRowAsHeader">excel第一行是否表头</param>
        /// <returns>返回DataSet</returns>
        public static DataSet ExcelToDataSet(string excelPath, bool firstRowAsHeader)
        {
            int sheetCount;
            return ExcelToDataSet(excelPath, firstRowAsHeader, out sheetCount);
        }

        /// <summary>
        ///     Excel转换成DataSet
        /// </summary>
        /// <param name="excelPath">excel文件路径</param>
        /// <param name="firstRowAsHeader">excel第一行是否表头</param>
        /// <param name="sheetCount">sheet的个数</param>
        /// <returns>返回DataSet</returns>
        public static DataSet ExcelToDataSet(string excelPath, bool firstRowAsHeader, out int sheetCount)
        {
            using (var ds = new DataSet())
            {
                using (var fileStream = new FileStream(excelPath, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook = WorkbookFactory.Create(fileStream);
                    IFormulaEvaluator evaluator = WorkbookFactory.CreateFormulaEvaluator(workbook);
                    sheetCount = workbook.NumberOfSheets;
                    for (int i = 0; i < sheetCount; ++i)
                    {
                        ISheet sheet = workbook.GetSheetAt(i);
                        DataTable dt = ExcelToDataTable(sheet, evaluator, firstRowAsHeader);
                        ds.Tables.Add(dt);
                    }

                    return ds;
                }
            }
        }

        /// <summary>
        ///     excel转换成DataTable
        /// </summary>
        /// <param name="excelPath">excel文件的路径</param>
        /// <param name="sheetName">sheet的名称</param>
        /// <returns>返回Datatable</returns>
        public static DataTable ExcelToDataTable(string excelPath, string sheetName)
        {
            return ExcelToDataTable(excelPath, sheetName, true);
        }

        /// <summary>
        ///     excel转换成DataTable
        /// </summary>
        /// <param name="excelPath">excel文件的路径</param>
        /// <param name="sheetName">sheet的名称</param>
        /// <param name="firstRowAsHeader">excel第一行是否表头</param>
        /// <returns>返回Datatable</returns>
        public static DataTable ExcelToDataTable(string excelPath, string sheetName, bool firstRowAsHeader)
        {
            using (var fileStream = new FileStream(excelPath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook = WorkbookFactory.Create(fileStream);
                IFormulaEvaluator evaluator = WorkbookFactory.CreateFormulaEvaluator(workbook);
                ISheet sheet = workbook.GetSheet(sheetName);
                return ExcelToDataTable(sheet, evaluator, firstRowAsHeader);
            }
        }

        /// <summary>
        ///     将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public static DataTable ExcelToDataTable(string fileName, bool isFirstRowColumn)
        {
            IWorkbook workbook = null;
            FileStream fs = null;
            ISheet sheet = null;
            var data = new DataTable();
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);


                sheet = workbook.GetSheetAt(0);


                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    var column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("导入失败,请检查文件是否被其他应用占用。");
                return null;
            }
        }

        /// <summary>
        ///     DataTable导出到Excel2007或者Excel2003文件，根据保存格式自动识别
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strFileName">保存位置</param>
        public static void Export(DataTable dtSource, string strFileName)
        {
            string fileType = strFileName.Substring(strFileName.LastIndexOf('.'));

            using (MemoryStream ms = Export(dtSource, fileType.ToLower().Equals(".xlsx")))
            {
                using (var fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        /// <summary>
        ///     IList泛型导出到Excel2007或者Excel2003文件，根据保存格式自动识别
        /// </summary>
        /// <param name="dataList">源dataList</param>
        /// <param name="columnHeader">列头</param>
        public static void Export<T>(IList<T> dataList, string strFileName, string[] propertyName = null)
        {
            DataTable dataTable = ToDataTable(dataList, propertyName);
            Export(dataTable, strFileName);
        }

        /// <summary>
        ///     DataTable导出Excel2007或者Excel2003的MemoryStream，根据保存格式自动识别
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="is2007">是否excel2007</param>
        public static MemoryStream Export(DataTable dtSource, bool is2007 = false)
        {
            IWorkbook workbook;
            if (is2007)
            {
                workbook = new XSSFWorkbook();
            }
            else
            {
                workbook = new HSSFWorkbook();
            }

            ISheet sheet = workbook.CreateSheet();
            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽  
            var arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = 25;
            }

            int rowIndex = 0;

            #region 列头及样式

            {
                IRow headerRow = sheet.CreateRow(0);
                ICellStyle headStyle = workbook.CreateCellStyle();
                headStyle.Alignment = HorizontalAlignment.Center;
                IFont font = workbook.CreateFont();
                font.FontHeightInPoints = 10;
                font.Boldweight = 700;
                headStyle.SetFont(font);

                foreach (DataColumn column in dtSource.Columns)
                {
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                    headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                    //设置列宽  
                    sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1)*256);
                }
            }

            #endregion

            rowIndex++;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 填充内容

                IRow dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    ICell newCell = dataRow.CreateCell(column.Ordinal);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String": //字符串类型  
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime": //日期类型  
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle; //格式化显示  
                            break;
                        case "System.Boolean": //布尔型  
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16": //整型  
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal": //浮点型  
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull": //空值处理  
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                }

                #endregion

                rowIndex++;
            }

            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                return ms;
            }
        }

        private static DataTable ExcelToDataTable(ISheet sheet, IFormulaEvaluator evaluator, bool firstRowAsHeader)
        {
            if (firstRowAsHeader)
            {
                return ExcelToDataTableFirstRowAsHeader(sheet, evaluator);
            }
            return ExcelToDataTable(sheet, evaluator);
        }

        private static DataTable ExcelToDataTableFirstRowAsHeader(ISheet sheet, IFormulaEvaluator evaluator)
        {
            using (var dt = new DataTable())
            {
                IRow firstRow = sheet.GetRow(0);
                int cellCount = GetCellCount(sheet);

                for (int i = 0; i < cellCount; i++)
                {
                    if (firstRow.GetCell(i) != null)
                    {
                        dt.Columns.Add(firstRow.GetCell(i).StringCellValue ?? string.Format("F{0}", i + 1),
                            typeof (string));
                    }
                    else
                    {
                        dt.Columns.Add(string.Format("F{0}", i + 1), typeof (string));
                    }
                }

                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dr = dt.NewRow();
                    FillDataRowBySheetRow(row, evaluator, ref dr);
                    dt.Rows.Add(dr);
                }

                dt.TableName = sheet.SheetName;
                return dt;
            }
        }

        private static DataTable ExcelToDataTable(ISheet sheet, IFormulaEvaluator evaluator)
        {
            using (var dt = new DataTable())
            {
                if (sheet.LastRowNum != 0)
                {
                    int cellCount = GetCellCount(sheet);

                    for (int i = 0; i < cellCount; i++)
                    {
                        dt.Columns.Add(string.Format("F{0}", i), typeof (string));
                    }

                    for (int i = 0; i < sheet.FirstRowNum; ++i)
                    {
                        DataRow dr = dt.NewRow();
                        dt.Rows.Add(dr);
                    }

                    for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        DataRow dr = dt.NewRow();
                        FillDataRowBySheetRow(row, evaluator, ref dr);
                        dt.Rows.Add(dr);
                    }
                }

                dt.TableName = sheet.SheetName;
                return dt;
            }
        }

        private static void FillDataRowBySheetRow(IRow row, IFormulaEvaluator evaluator, ref DataRow dr)
        {
            if (row != null)
            {
                for (int j = 0; j < dr.Table.Columns.Count; j++)
                {
                    ICell cell = row.GetCell(j);

                    if (cell != null)
                    {
                        switch (cell.CellType)
                        {
                            case CellType.Blank:
                                dr[j] = DBNull.Value;
                                break;
                            case CellType.Boolean:
                                dr[j] = cell.BooleanCellValue;
                                break;
                            case CellType.Numeric:
                                if (DateUtil.IsCellDateFormatted(cell))
                                {
                                    dr[j] = cell.DateCellValue;
                                }
                                else
                                {
                                    dr[j] = cell.NumericCellValue;
                                }
                                break;
                            case CellType.String:
                                dr[j] = cell.StringCellValue;
                                break;
                            case CellType.Error:
                                dr[j] = cell.ErrorCellValue;
                                break;
                            case CellType.Formula:
                                cell = evaluator.EvaluateInCell(cell);
                                dr[j] = cell.ToString();
                                break;
                            default:
                                throw new NotSupportedException(string.Format("Catched unhandle CellType[{0}]",
                                    cell.CellType));
                        }
                    }
                }
            }
        }

        private static int GetCellCount(ISheet sheet)
        {
            int firstRowNum = sheet.FirstRowNum;

            int cellCount = 0;

            for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; ++i)
            {
                IRow row = sheet.GetRow(i);

                if (row != null && row.LastCellNum > cellCount)
                {
                    cellCount = row.LastCellNum;
                }
            }

            return cellCount;
        }

        private static DataTable ToDataTable<T>(IEnumerable<T> data, IEnumerable<string> propertyName = null)
        {
            var propertyNameList = new List<string>();
            if (propertyName != null)
            {
                propertyNameList.AddRange(propertyName);
            }
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof (T));
            var table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (propertyNameList.Count == 0)
                {
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }
                else if (propertyNameList.Contains(prop.Name))
                {
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }
            }

            var values = new object[table.Columns.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    values[i] = props[table.Columns[i].ColumnName].GetValue(item);
                }

                table.Rows.Add(values);
            }

            return table;
        }

        #region 读写List

        public static List<List<string>> Read(string fileName)
        {
            var lst = new List<List<string>>();

            using (FileStream fs = File.OpenRead(fileName)) //打开myxls.xls文件
            {
                IWorkbook wk;

                if (fileName.EndsWith(".xlsx"))
                {
                    //office07
                    wk = new XSSFWorkbook(fs);
                }
                else
                {
                    //office03
                    wk = new HSSFWorkbook(fs);
                }

                for (int i = 0; i < wk.NumberOfSheets; i++) //NumberOfSheets是myxls.xls中总共的表数
                {
                    ISheet sheet = wk.GetSheetAt(i); //读取当前表数据
                    for (int j = 0; j <= sheet.LastRowNum; j++) //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j); //读取当前行数据
                        if (row != null)
                        {
                            var lstrow = new List<string>();
                            lst.Add(lstrow);

                            for (int k = 0; k <= row.LastCellNum; k++) //LastCellNum 是当前行的总列数
                            {
                                ICell cell = row.GetCell(k); //当前表格
                                lstrow.Add(cell?.ToString() ?? "");
                            }
                        }
                    }
                }
            }

            return lst;
        }

        public static List<List<string>> ReadLines(string fileName)
        {
            return ReadLines(fileName, Encoding.UTF8);
        }

        public static List<List<string>> ReadLines(string fileName, Encoding encoding)
        {
            string[] x = File.ReadAllLines(fileName, encoding);
            return x.Select(t => t.Split('\t').ToList()).ToList();
        }

        public static void SaveLines<T>(List<List<T>> data, string path, bool append = false)
        {
            const string next = "\t";
            const string line = "\r\n";
            var sb = new StringBuilder();
            foreach (var item in data)
            {
                sb.Append(item[0]);

                for (int i = 1; i < item.Count; i++)
                {
                    sb.Append(next).Append(item[i]);
                }

                sb.Append(line);
            }

            if (append)
            {
                File.AppendAllText(path, sb.ToString());
            }
            else
            {
                File.WriteAllText(path, sb.ToString());
            }
        }

        #endregion
    }
}