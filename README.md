# ExportExcel
导表工具<br>
使用NPOI读取Excel文件导出bytes文件<br>
使用Scriban生成模板代码<br>

读取某个ID的表
var table = TableDock.Instanced.GetTable<t_languageTable>(1);
