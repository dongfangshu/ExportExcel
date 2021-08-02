using System;
using System.Collections.Generic;
using System.Text;


public class SheetInfo
{
    public string ExcelName { get; set; }
    public string SheetName { get; set; }
    public string SheetDesc { get; set; }
    public List<PropertyInfo> propertyInfos = new List<PropertyInfo>();
}

