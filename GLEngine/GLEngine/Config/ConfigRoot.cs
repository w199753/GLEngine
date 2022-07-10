using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLEngine.Config
{
    public static class ConfigRoot
    {
        static ConfigRoot()
        {
            if (EngineConfigLoader.InitFinish == false) EngineConfigLoader.InitConfig();
        }
        public static EngineConfigLoader.EngineConfigData Editor_Window_Conf
        {
            get => EngineConfigLoader.GetConfigData(ConfigFileConstant.F_Editor_Window_Conf);
        }
    }
}
