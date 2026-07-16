using System;
using System.IO;
using _Project.Core.Config;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Infrastructure.UnityServices
{
    public class ResourcesConfigProvider : IConfigProvider
    {
        public T GetConfig<T>(string path) where T : IConfig
        {
            TextAsset jsonConfig = Resources.Load<TextAsset>(path);
            if (jsonConfig == null)
            {
                throw new FileNotFoundException($"Config file \"{path}\" could not be found.");
            }
            try
            {
                T config = JsonConvert.DeserializeObject<T>(jsonConfig.text);

                if (config == null)
                {
                    throw new JsonSerializationException($"Config file \"{path}\" could not be deserialized.");
                }
                return config;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Config file \"{path}\" could not be deserialized. Check JSON syntax.", e);
            }
        }
    }
}