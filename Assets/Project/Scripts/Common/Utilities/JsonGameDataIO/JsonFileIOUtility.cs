using System.IO;
using UnityEngine;

namespace CandyMaster.Project.Scripts.Common.Utilities.JsonGameDataIO
{
    public static class JsonFileIOUtility
    {
        private static string _path;


        public static void Initialize() => 
            _path = Path.Combine(Application.persistentDataPath, "game.json");
        
        public static void Save(GameJsonData data) => 
            File.WriteAllText(_path, JsonUtility.ToJson(data, true));
       
        public static bool TryLoad(out GameJsonData data)
        {
            data = new GameJsonData();
            if (!File.Exists(_path))
                return false;

            data = JsonUtility.FromJson<GameJsonData>(File.ReadAllText(_path));
            return true;
        }
    }
    
}