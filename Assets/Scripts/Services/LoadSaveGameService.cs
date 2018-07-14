using System.IO;
using Models;
using Models.Arena;
using Models.State;
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
        /// Json Work Service
        /// </summary>
        [Inject]
        public JsonWorkService JsonWorkService { get; set; }

        /// <summary>
        /// Save data
        /// </summary>
        public void SaveGameSession(string path)
        {
            // create state history log
            var stateData = new StateData
            {
                YourPlayer = StateService.GetStatePlayer(Arena.Player),
                EnemyPlayer = StateService.GetStatePlayer(Arena.Opponent)
            };

            var json = JsonWorkService.SerializeToJson(stateData);
            using (var fs = new FileStream(path, FileMode.Create))
            {
                using (var writer = new StreamWriter(fs))
                {
                    writer.Write(json);
                }
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
            return JsonUtility.FromJson<StateData>(dataAsJson);
        }
    }
}