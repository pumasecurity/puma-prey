using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puma.Prey.Common.Deserialize
{
    public class DeserializeJson
    {
        public static T GetObject<T>(string json) where T : new()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.All;
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
    }
}