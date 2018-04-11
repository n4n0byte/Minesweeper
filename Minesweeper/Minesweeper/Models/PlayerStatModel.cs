using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Minesweeper.Models
{
    /**
     * Stores number of clicks and 
     * time in seconds
     */
    [Serializable]
    [DataContract]
    public class PlayerStatModel
    {
        public PlayerStatModel(int numberOfClicks, int timeSpent) {
            NumberOfClicks = numberOfClicks;
            TimeSpent = timeSpent;
        }

        public PlayerStatModel(int playerId, int numberOfClicks, int timeSpent) {
            PlayerId = playerId;
            NumberOfClicks = numberOfClicks;
            TimeSpent = timeSpent;
        }

        public PlayerStatModel() {
        }

        [DataMember]
        public int PlayerId { get; set; }

        [DataMember]
        public int NumberOfClicks { get; set; }

        [DataMember]
        public int TimeSpent { get; set; }

    }
}