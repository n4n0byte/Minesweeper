using System;
using System.Runtime.Serialization;

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
        public PlayerStatModel(int numberOfClicks, int secondsPlaying) {
            NumberOfClicks = numberOfClicks;
            SecondsPlaying = secondsPlaying;
        }

        public PlayerStatModel(int playerId, int numberOfClicks, int secondsPlaying) {
            PlayerId = playerId;
            NumberOfClicks = numberOfClicks;
            SecondsPlaying = secondsPlaying;
        }

        public PlayerStatModel() {
        }

        [DataMember]
        public int PlayerId { get; set; }

        [DataMember]
        public int NumberOfClicks { get; set; }

        [DataMember]
        public int SecondsPlaying { get; set; }

    }
}