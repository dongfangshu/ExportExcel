using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace ExportExcel
{
    public class Setting
    {
        public static Setting Instance;
        public string ClientBytesPath { get; private set; }
        public string ClientCodePath { get; private set; }
        public string TableTemplatePath { get; private set; }
        public string TableTemplateContainerPath { get; private set; }
        public string TableTemplateDockerPath { get; private set; }
        public string TablePath { get; private set; }
        public static void Init(string configPath)
        {
            if (!File.Exists(configPath))
            {
                Logger.Err("配置文件不存在");
            }
            Instance = new Setting();
            XmlDocument xml = new XmlDocument();
            XmlReaderSettings xmlReaderSetting = new XmlReaderSettings();
            xmlReaderSetting.IgnoreComments = true;
            XmlReader xmlReader = XmlReader.Create(configPath,xmlReaderSetting);
            xml.Load(xmlReader);
            XmlNode xmlNode = xml.SelectSingleNode("config");
            Instance.ClientBytesPath= xmlNode.SelectNodes("client_bytes_path").Item(0).InnerText;
            Instance.ClientCodePath= xmlNode.SelectNodes("client_code_path").Item(0).InnerText;
            Instance.TablePath= xmlNode.SelectNodes("table_path").Item(0).InnerText;
            Instance.TableTemplatePath= xmlNode.SelectNodes("table_template").Item(0).InnerText;
            Instance.TableTemplateContainerPath = xmlNode.SelectNodes("table_container_template").Item(0).InnerText;
            Instance.TableTemplateDockerPath = xmlNode.SelectNodes("table_docker_template").Item(0).InnerText;
            //Logger.Log(Instance.ToString());
            //ExportExcel.Helper.ExportHelper.InitPath();
        }
        public override string ToString()
        {
            return $"配置表路径：{TablePath}\n客户端导表输出路径：{ClientBytesPath}\n客户端代码输出路径：{ClientCodePath}";
        }
    }
}
