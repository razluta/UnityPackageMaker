using System.Collections.Generic;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace UnityPackageMaker.Editor.SystemUtilities
{
    public static class JsonUtilities
    {
        // public static Dictionary<> GetData(string path)
        // {
        //     if (!File.Exists(path))
        //     {
        //         return 
        //     }
        //     
        //     var file = File.OpenText(path);
        //     var serializers = new JsonSerializer();
        //     var dictionary = (List<Rule>) serializers.Deserialize(file, typeof(List<Rule>));
        //     file.Close();
        //     return rules;
        // }
        
        public static void SetData(Dictionary<string, object> dictionary, string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(dictionary));
        }
    }
}