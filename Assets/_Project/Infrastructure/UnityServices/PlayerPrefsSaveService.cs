using System;
using _Project.Core.Save;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Infrastructure.UnityServices
{
    public class PlayerPrefsSaveService : ISaveService
    {
        public void Save(ISave save, string fileName)
        {
            string json =  JsonConvert.SerializeObject(save);
            PlayerPrefs.SetString(fileName, json);
        }

        public T Load<T>(string fileName) where T : ISave
        {
            if (PlayerPrefs.HasKey(fileName))
            {
                string json = PlayerPrefs.GetString(fileName);
                try
                {
                    T save = JsonConvert.DeserializeObject<T>(json);
                    return save;
                }
                catch (JsonException e)
                {
                    Debug.LogError($"Save file {fileName} could not be deserialized.\n{e}");
                }
            }
            return default;
        }
    }
}