using System.IO;
using Models.Arena;
using Models.State;
using Newtonsoft.Json;
using UnityEngine;

namespace Services
{
    public class LoadSaveGameService
    {
        /// <summary>
        /// State service
        /// </summary>
        [Inject]
        public StateService StateService { get; set; }

        /// <summary>
        /// Arena
        /// </summary>
        [Inject]
        public Arena Arena { get; set; }

        /// <summary>
        /// Save data
        /// </summary>
        public void SaveGameSession(string path)
        {
            //open file stream
            using (var file = File.CreateText(path))
            {
                var serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, new StateData
                {
                    YourPlayer = StateService.GetStatePlayer(Arena.YourPlayer),
                    EnemyPlayer = StateService.GetStatePlayer(Arena.EnemyPlayer)
                });
            }
        }

        /// <summary>
        /// Load game session
        /// </summary>
        public StateData LoadGameSession(string path)
        {
            if (!File.Exists(path)) return null;
            // Read the json from the file into a string
            var dataAsJson = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<StateData>(dataAsJson);
        }
    }
}