using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GLEngine.Config
{

    public static class EngineConfigLoader
    {
        public sealed class EngineConfigData
        {
            public const bool ENABLE_DEBUG_CONSTANT = false;
            private ConfigJsonStruct m_Struct = null;
            public EngineConfigData() { }
            public EngineConfigData(string data)
            {
                m_Struct = JsonConvert.DeserializeObject<ConfigJsonStruct>(data);
                if(ENABLE_DEBUG_CONSTANT)
                {
                    foreach (var item in m_Struct.text.Keys)
                    {
                        var xx = $"public static readonly string {item} = \"{item}\";";
                        Console.WriteLine(xx);
                    }
                }
            }

            public string GetText(string key)
            {
                if (m_Struct != null && m_Struct.text.ContainsKey(key))
                    return m_Struct.text[key];
                return $"找不到value={key},Struct={m_Struct}";
            }

            public Bitmap GetIcon(string key)
            {
                return null;
            }
            class ConfigJsonStruct
            {
                public string iconRelativePath;
                public Dictionary<string, string> text;
                public Dictionary<string, string> icon;
            }
        }



        private static bool m_InitFinish = false;
        public static bool InitFinish { get => m_InitFinish; }
        public static string RootDir = "";
        private static Dictionary<string, EngineConfigData> m_ConfigDataCacheDic = new Dictionary<string, EngineConfigData>(4);

        public static void InitConfig()
        {
            var curDir = Directory.GetCurrentDirectory();
            var root = Path.GetFullPath("../../../", curDir) + "Config";
            DirectoryInfo info = new DirectoryInfo(root);
            var configs = info.GetFiles("*.json");
            foreach (var item in configs)
            {
                var len = item.Name.Length;
                var fileName = item.Name.Substring(0, len - item.Extension.Length);
                string res = string.Empty;
                using (FileStream fs = File.Open(item.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (BufferedStream bs = new BufferedStream(fs))
                using (StreamReader sr = new StreamReader(bs))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        res += line;
                    }
                }
                m_ConfigDataCacheDic.Add(fileName, new EngineConfigData(res));
            }
            m_InitFinish = true;
        }

        public static bool LoadConfData(string fileKey)
        {
            if (m_ConfigDataCacheDic.ContainsKey(fileKey)) return true;
            else
            {
                var curDir = Directory.GetCurrentDirectory();
                var root = Path.GetFullPath("../../../", curDir) + "Config";
                DirectoryInfo info = new DirectoryInfo(root);
                var configs = info.GetFiles("*.json");
                foreach (var item in configs)
                {
                    if (item == null) continue;

                    var len = item.Name.Length;
                    var fileName = item.Name.Substring(0, len - item.Extension.Length);
                    if (fileName == fileKey)
                    {
                        //这种读出来前面有奇怪的字符，之前也遇到过，忘记怎么搞的了
                        //using(FileStream fs = new FileStream(item.FullName, FileMode.OpenOrCreate, FileAccess.Read))
                        //{
                        //    byte[] buffer = new byte[fs.Length];
                        //    int r = fs.Read(buffer, 0, buffer.Length-2);
                        //    Console.WriteLine(System.Text.Encoding.UTF8.GetString(buffer));
                        //    Console.WriteLine(r+"  "+fs.Length);
                        //}
                        string res = string.Empty;
                        using (FileStream fs = File.Open(item.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        using (BufferedStream bs = new BufferedStream(fs))
                        using (StreamReader sr = new StreamReader(bs))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                res += line;
                            }
                        }
                        m_ConfigDataCacheDic.Add(fileKey, new EngineConfigData(res));
                    }
                }
                return true;
            }
        }

        public static EngineConfigData GetConfigData(string fileKey)
        {
            if (m_ConfigDataCacheDic.ContainsKey(fileKey))
            {
                return m_ConfigDataCacheDic[fileKey];
            }
            else
            {
                if (LoadConfData(fileKey)) return m_ConfigDataCacheDic[fileKey];
            }
            throw new Exception("cant load config:" + fileKey);
        }
    }
}
