using System;
using System.Collections.Generic;
using Models.State;
using UnityEngine;

namespace Services
{
    public class JsonWorkService
    {
        /// <summary>
        /// Creates the list and serializes it to a Json string
        /// </summary>
        /// <returns>Serialized Json string</returns>
        public string SerializeToJson<T>(T seriazeData)
        {
            var container = new ListContainer<T>(seriazeData);
            var json = "";
            try
            {
                json = JsonUtility.ToJson(container);
                Debug.Log(json);
            }
            catch (Exception error)
            {
                Debug.LogError(error.Message);
            }

            return json;
        }
    }
}