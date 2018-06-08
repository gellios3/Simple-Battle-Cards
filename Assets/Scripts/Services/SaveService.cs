using System.IO;
using Models.Arena;
using Models.State;
using Newtonsoft.Json;

namespace Services
{
    public class SaveService
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
        public void SaveData()
        {
            //open file stream
            using (var file = File.CreateText("Assets/Resources/SaveState/save-log.json"))
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
    }
}