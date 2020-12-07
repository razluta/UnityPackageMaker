using System.Collections.Generic;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace UnityPackageMaker.Editor.SystemUtilities
{
    public static class JsonUtilities
    {
        public static Dictionary<string, object> GetData(string path)
        {
            if (!File.Exists(path))
            {
                return new Dictionary<string, object>();
            }
            
            var file = File.OpenText(path);
            var serializers = new JsonSerializer();
            var dictionary = (Dictionary<string, object>) serializers.Deserialize(file, typeof(Dictionary<string, object>));
            file.Close();
            return dictionary;
        }
        
        public static void SetData(Dictionary<string, object> dictionary, string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(dictionary));
        }
    }
}