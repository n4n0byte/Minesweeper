using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Minesweeper.Models;

namespace PlayerStatisticService
{

    [DataContract]
    public class DTO
    {
        public DTO(int Status, string Message, List<PlayerStatModel>  Data)
        {
            this.Status = Status;
            this.Message = Message;
            this.Data = Data;
        }

        [DataMember]
        public int Status { get; set; }
        
        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public List<PlayerStatModel> Data { get; set; }

    }

}