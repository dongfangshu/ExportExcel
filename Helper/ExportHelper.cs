using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using NPOI.XSSF;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using Scriban;

namespace ExportExcel.Helper
{
    public class ExportHelper
    {
        public static void InitPath()
        {
            if (!Directory.Exists(Setting.Instance.ClientBytesPath))
            {
                Directory.CreateDirectory(Setting.Instance.ClientBytesPath);
            }
            if (!Directory.Exists(Setting.Instance.ClientCodePath))
            {
                Directory.CreateDirectory(Setting.Instance.ClientCodePath);
            }

        }
        public static void Export(List<string> files,string bytesPath,string codePath)
        {
            foreach (var file in files)
            {
                using (var stream=File.Open(file,FileMode.Open))
                {
                    XSSFWorkbook book = new XSSFWorkbook(stream);
                    for (int i = 0; i < book.NumberOfSheets; i++)
                    {
                        ISheet Sheet= book.GetSheetAt(i);
                        if (!Sheet.SheetName.Contains("t_"))
                            continue;
                        byte[] BytesDate = new byte[1024];
                        int offSize = 0;
                        SheetInfo sheetInfo = new SheetInfo();
                        sheetInfo.sheetname = Sheet.SheetName;
                        string excelName= Path.GetFileNameWithoutExtension(file);
                        sheetInfo.excelname = excelName;
                        IRow PropertyDescRow = Sheet.GetRow(1);//属性描述
                        IRow PropertyNameRow = Sheet.GetRow(2);//属性行
                        IRow PropertyTypeRow = Sheet.GetRow(3);//属性类型
                        Dictionary<int, string> validMap = new Dictionary<int, string>();
                        for (int j = 0; j < PropertyNameRow.LastCellNum; j++)
                        {
                            ICell cell = PropertyNameRow.GetCell(j);
                            if (cell == null)
                                continue;
                            PropertyInfo cellProperty = new PropertyInfo();
                            cellProperty.propertydesc = PropertyDescRow.GetCell(j).StringCellValue;
                            cellProperty.propertyname = "t_" + cell.StringCellValue;
                            cellProperty.propertytype = PropertyTypeRow.GetCell(j).StringCellValue;
                            sheetInfo.propertyinfos.Add(cellProperty);
                            validMap.Add(j, PropertyTypeRow.GetCell(j).StringCellValue);
                        }
                        for (int k = 4; k < Sheet.LastRowNum; k++)
                        {
                            IRow SheetRow = Sheet.GetRow(k);
                            for (int m = 0; m < SheetRow.LastCellNum; m++)
                            {
                                if (!validMap.ContainsKey(m))
                                    continue;
                                ICell CurrentCell = SheetRow.GetCell(m);
                                string Type = validMap[m];
                                if (Type=="int")
                                {
                                    BytesBuffer.WriteIntBytes((int)CurrentCell.NumericCellValue,BytesDate,ref offSize);
                                }
                                else if (Type=="string")
                                {
                                    string temp;
                                    if ((CurrentCell.CellType & CellType.Numeric) == 0)
                                    {
                                        temp = CurrentCell.NumericCellValue.ToString();
                                    }
                                    else
                                    {
                                        temp = CurrentCell.StringCellValue;
                                    }

                                    BytesBuffer.WriteStringBytes(temp, BytesDate,ref offSize);
                                }
                            }
                        }


                        //Export Bytes
                        byte[] practicalBytes = new byte[offSize];
                        Array.Copy(BytesDate,0,practicalBytes,0,offSize);
                        File.WriteAllBytes(Setting.Instance.ClientBytesPath+"/"+ sheetInfo.sheetname+"Bean.bytes",practicalBytes);
                        Logger.Log(sheetInfo.sheetname+"导出成功");

                        //Export TemplateCode
                        ExportTemplate(sheetInfo);
                    }
                }
            }


        }
        public static void ExportTemplate(SheetInfo sheetInfo)
        {
            string beanCode = File.ReadAllText(Setting.Instance.BeanTemplatePath);
            Template beanTemplate = Template.Parse(beanCode);
            string Code= beanTemplate.Render(sheetInfo);
            File.WriteAllText(Setting.Instance.ClientCodePath+"/"+sheetInfo.sheetname+".cs",Code);
            Logger.Log(sheetInfo.sheetname + "导出成功");
        }
        public static void ExportAll()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Setting.Instance.InputPath);
            if (!directoryInfo.Exists)
            {
                Logger.Err("配置表不存在");
            }
            FileInfo[] xlslFiles= directoryInfo.GetFiles("*.xlsx");
            List<string> files= xlslFiles.Select((f)=> {return f.FullName; }).ToList();
            Export(files,Setting.Instance.ClientBytesPath, Setting.Instance.ClientCodePath);
        }
    }
}
