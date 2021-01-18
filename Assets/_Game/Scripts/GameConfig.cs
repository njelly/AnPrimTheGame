using System;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using UnityEngine;

namespace Tofunaut.AnPrimTheGame
{
    //[CreateAssetMenu(fileName = "AppConfig", menuName = "Tofunaut/AppConfig", order = 1)]
    public class GameConfig : ScriptableObject
    {
        [Serializable]
        public class Config
        {
            [Serializable]
            public class Actor
            {
                public string Name;
                public Vector2Int ColliderSize;
                public float MoveSpeed;
            }

            public Actor[] Actors;
        }

        public Config config;
        
        #if UNITY_EDITOR
        [UnityEditor.MenuItem("Tofunaut/Import Config")]
        public static async void Import()
        {
            const string key = "9ae209d5-894d-4257-9297-844a45202f43";
            const string sheetId = "AKfycbya_ViKMizgixwlJbkN6Nv8xutgwJ7vzOvXIaxdSPXV6PkACPzh4fUq";
            
            var httpWebRequest = (HttpWebRequest) WebRequest.Create($"https://script.google.com/macros/s/{sheetId}/exec?key={key}");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "*/*";
            httpWebRequest.Method = "GET";

            var response = await httpWebRequest.GetResponseAsync();
            var responseStream = response.GetResponseStream();
            if (responseStream == null)
            {
                Debug.LogError("response stream is null");
                return;
            }

            var streamReader = new StreamReader(responseStream);
            var content = await streamReader.ReadToEndAsync();

            var asset = UnityEditor.AssetDatabase.FindAssets("t:AppConfig").Select(x =>
                UnityEditor.AssetDatabase.LoadAssetAtPath<GameConfig>(UnityEditor.AssetDatabase.GUIDToAssetPath(x))).FirstOrDefault();

            if (!asset)
            {
                Debug.LogError("no app config asset in project");
                return;
            }

            var sheets = JsonConvert.DeserializeObject<object[]>(content);
            asset.config.Actors = JsonConvert.DeserializeObject<Config.Actor[]>(sheets[0].ToString(), new Vector2IntConverter());
            UnityEditor.EditorUtility.SetDirty(asset);
            UnityEditor.AssetDatabase.SaveAssets();
            
            Debug.Log("import complete");
        }
        #endif
        
        private class Vector2IntConverter : JsonConverter<Vector2Int>
        {
            public override void WriteJson(JsonWriter writer, Vector2Int value, JsonSerializer serializer)
            {
                writer.WriteValue($"({value.x},{value.y})");
            }

            public override Vector2Int ReadJson(JsonReader reader, Type objectType, Vector2Int existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                var s = (string) reader.Value;
                
                if(string.IsNullOrEmpty(s))
                    return Vector2Int.zero;
                
                s = s.Trim('(', ')');
                
                var parts = s.Split(',');
                if(parts.Length != 2 || !int.TryParse(parts[0], out var x) || !int.TryParse(parts[1], out var y))
                    return Vector2Int.zero;
                
                return new Vector2Int(x, y);
            }
        }
    }
}