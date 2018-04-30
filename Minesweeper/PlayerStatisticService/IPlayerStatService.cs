using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PlayerStatisticService
{
    /// <summary>
    /// Interface For Player Stat Rest service
    /// </summary>
    [ServiceContract]
    public interface IPlayerStatService
    {

        /// <summary>
        /// Returns All PlayerStats in a List 
        /// which is then stored in a DTO
        /// </summary>
        /// <returns>DTO</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAllPlayerStats/")]
        DTO GetAllPlayerStats();

        /// <summary>
        /// Returns A DTO Containing containing a 
        /// game, found by User ID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>DTO</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetPlayerStat/{id}")]
        DTO GetPlayerStat(string id);


    }
}
