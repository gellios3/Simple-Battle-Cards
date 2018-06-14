﻿using System;

namespace Models.State
{
    [Serializable]
    public class StateTrate : StateItem
    {
        /// <summary>
        /// Defence
        /// </summary>
        public int Defence;

        /// <summary>
        /// Attack
        /// </summary>
        public int Health;
    }
}