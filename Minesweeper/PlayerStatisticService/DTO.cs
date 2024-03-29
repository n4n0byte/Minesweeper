﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Minesweeper.Models;

namespace PlayerStatisticService
{

    /// <summary>
    /// DTO for PlayerStats
    /// </summary>
    [DataContract]
    public class DTO
    {
        public DTO(int Status, string Message, List<PlayerStatModel>  Data)
        {
            this.Status = Status;
            this.Message = Message;
            this.Data = Data;
        }

        public DTO() {}

        [DataMember]
        public int Status { get; set; }
        
        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public List<PlayerStatModel> Data { get; set; }

    }

}