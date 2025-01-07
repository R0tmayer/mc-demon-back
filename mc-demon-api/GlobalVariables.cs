using System;
using System.Collections.Generic;
using System.IO;

namespace mc_demon_api
{
    public static class GlobalVariables
    {
        private static readonly string CONFIG_PATH = "/opt/mcdemon-vpn-config/config.ini";
        private static readonly string TEST_CONFIG_PATH = "sources/config.ini";
        private static Dictionary<string, string> _configValues;

        private static string ConfigPath
        {
            get
            {
                if (File.Exists(CONFIG_PATH))
                {
                    return CONFIG_PATH;
                }

                if (File.Exists(TEST_CONFIG_PATH))
                {
                    return TEST_CONFIG_PATH;
                }

                throw new FileNotFoundException("Configuration file not found.", TEST_CONFIG_PATH);
            }
        }

        static GlobalVariables()
        {
            LoadConfig();
        }

        private static void LoadConfig()
        {
            _configValues = new Dictionary<string, string>();

            foreach (var line in File.ReadAllLines(ConfigPath))
            {
                var parts = line.Split(new[] {'='}, 2);
                if (parts.Length == 2)
                {
                    _configValues[parts[0].Trim()] = parts[1].Trim().Trim('"');
                }
            }

            if (!_configValues.ContainsKey("PROFILES_CSV_PATH") ||
                !_configValues.ContainsKey("TEMPLATES_CSV_PATH") ||
                !_configValues.ContainsKey("TEMPLATES_FOLDER_PATH") ||
                !_configValues.ContainsKey("CONFIGS_FOLDER_PATH"))
            {
                throw new InvalidOperationException("One or more required configuration values are missing.");
            }
        }

        public static string PROFILES_CSV_PATH => _configValues["PROFILES_CSV_PATH"];
        public static string TEMPLATES_CSV_PATH => _configValues["TEMPLATES_CSV_PATH"];
        public static string TEMPLATES_FOLDER_PATH => _configValues["TEMPLATES_FOLDER_PATH"];
        public static string CONFIGS_FOLDER_PATH => _configValues["CONFIGS_FOLDER_PATH"];
    }
}