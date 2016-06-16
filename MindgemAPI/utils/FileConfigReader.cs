using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MindgemAPI.utils
{
    public class FileConfigReader
    {
        public static String CONFIG_FOLDER = @"config/";
        public static String DATA_OBJECTS_URL = @"dataObjects.cfg";
        public FileConfigReader()
        {
        }

        public Dictionary<String, String> loadDataObjectsConfig(String path)
        {
            Dictionary<String, String> mapObjectUrl = new Dictionary<string, string>();

            String[] lineConfigTab = File.ReadAllLines(path);
            String[] lineSplit;
            foreach(String str in lineConfigTab)
            {
                str.Replace(" ", "");
                lineSplit = str.Split('=');
                mapObjectUrl.Add(lineSplit.ElementAt(0), lineSplit.ElementAt(1));
            }
            return mapObjectUrl;
        }

    }
}