﻿using Newtonsoft.Json;

namespace Puma.Prey.Common.Deserialize
{
    public class DeserializeJson
    {
        public static T GetObject<T>(string json) where T : new()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
    }
}