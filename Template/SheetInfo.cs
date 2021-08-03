using System;
using System.Collections.Generic;
using System.Text;


public class SheetInfo
{
    public string excelname { get; set; }
    public string sheetname { get; set; }
    public string sheetdesc { get; set; }
    public List<PropertyInfo> propertyinfos = new List<PropertyInfo>();
}

