using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using Scriban;

namespace ExportExcel.Helper
{
    public class ExportHelper
    {
        public static void ClearPath()
        {
            if (!Directory.Exists(Setting.Instance.ClientBytesPath))
            {
                Directory.CreateDirectory(Setting.Instance.ClientBytesPath);
            }
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(Setting.Instance.ClientBytesPath);
                foreach (System.IO.FileInfo file in directoryInfo.GetFiles()) file.Delete();
            }
            
            if (!Directory.Exists(Setting.Instance.ClientCodePath))
            {
                Directory.CreateDirectory(Setting.Instance.ClientCodePath);
            }
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(Setting.Instance.ClientCodePath);
                foreach (System.IO.FileInfo file in directoryInfo.GetFiles()) file.Delete();
            }

        }
        public static void Export(List<string> files)
        {
            DockerInfo dockerInfo = new DockerInfo();
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
                        sheetInfo.sheetdesc = Sheet.GetRow(0).GetCell(0).StringCellValue;
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
                            var descCell = PropertyDescRow.GetCell(j);
                            if (descCell == null)
                            {
                                cellProperty.propertydesc = "";
                            }
                            else
                            {
                                cellProperty.propertydesc = descCell.StringCellValue;
                            }

                            cellProperty.propertyname = "t_" + cell.StringCellValue;
                            cellProperty.propertytype = PropertyTypeRow.GetCell(j).StringCellValue;
                            sheetInfo.propertyinfos.Add(cellProperty);
                            validMap.Add(j, PropertyTypeRow.GetCell(j).StringCellValue);
                        }
                        for (int k = 4; k <= Sheet.LastRowNum; k++)
                        {
                            IRow SheetRow = Sheet.GetRow(k);
                            for (int m = 0; m < SheetRow.LastCellNum; m++)
                            {
                                if (!validMap.ContainsKey(m))
                                    continue;
                                ICell CurrentCell = SheetRow.GetCell(m);
                                string Type = validMap[m];
                                if (Type == "int")
                                {
                                    BytesBuffer.WriteIntBytes((int)CurrentCell.NumericCellValue, BytesDate, ref offSize);
                                }
                                else if (Type == "string")
                                {
                                    string temp;
                                    if (CurrentCell.CellType==CellType.Numeric)
                                    {
                                        temp = CurrentCell.NumericCellValue.ToString();
                                    }
                                    else
                                    {
                                        temp = CurrentCell.StringCellValue;
                                    }

                                    BytesBuffer.WriteStringBytes(temp, BytesDate, ref offSize);
                                }
                                else if (Type == "textformat")
                                {
                                    BytesBuffer.WriteIntBytes((int)CurrentCell.NumericCellValue,BytesDate,ref offSize);
                                }
                                else if(Type=="short")
                                {
                                    BytesBuffer.WriteShortBytes((short)CurrentCell.NumericCellValue,BytesDate,ref offSize);
                                }
                                else if (Type=="long")
                                {
                                    BytesBuffer.WriteLongBytes((long)CurrentCell.NumericCellValue, BytesDate, ref offSize);
                                }
                            }
                        }


                        //Export Bytes
                        byte[] practicalBytes = new byte[offSize];
                        Array.Copy(BytesDate,0,practicalBytes,0,offSize);
                        File.WriteAllBytes(Setting.Instance.ClientBytesPath+"/"+ sheetInfo.sheetname+".bytes",practicalBytes);
                        Logger.Log(sheetInfo.sheetname+"导出成功");

                        //Export TemplateCode
                        ExportTemplate(sheetInfo);
                        dockerInfo.sheets.Add(sheetInfo);
                    }
                }
            }

            string dockerCode = File.ReadAllText(Setting.Instance.TableTemplateDockerPath);
            Template dockerTemplate = Template.Parse(dockerCode);
            string scripts = dockerTemplate.Render(dockerInfo);
            File.WriteAllText(Setting.Instance.ClientCodePath + "/" +"TableDock" + ".cs", scripts);
        }
        public static void ExportTemplate(SheetInfo sheetInfo)
        {
            string tableCode = File.ReadAllText(Setting.Instance.TableTemplatePath);
            string tableContainerCode = File.ReadAllText(Setting.Instance.TableTemplateContainerPath);
            Template tableTemplate = Template.Parse(tableCode);
            Template tableContainerTemplate = Template.Parse(tableContainerCode);
            string table= tableTemplate.Render(sheetInfo);
            string container= tableContainerTemplate.Render(sheetInfo);
            File.WriteAllText(Setting.Instance.ClientCodePath+"/"+sheetInfo.sheetname+".cs",table);
            File.WriteAllText(Setting.Instance.ClientCodePath+"/"+sheetInfo.sheetname+"Container.cs",container);
            Logger.Log(sheetInfo.sheetname + "导出成功");
        }
        public static void ExportAll()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Setting.Instance.TablePath);
            if (!directoryInfo.Exists)
            {
                Logger.Err("配置表不存在");
            }
            FileInfo[] xlslFiles= directoryInfo.GetFiles("*.xlsx");
            List<string> files= xlslFiles.Select((f)=> {return f.FullName; }).ToList();
            Export(files);
        }
    }
}
