using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.PlayerStats
{
  public class PlayerStatsSeasonController : ApiController
  {
    public PlayerStatsSeasonController()
    {
    }

    //public List<PlayerStatSeason> GetPlayerStatsSeason()
    //{
    //  var results = _repo.GetPlayerStatsSeason();
    //  return results.ToList();
    //}

    //public List<PlayerStatSeason> GetPlayerStatsSeasonByPlayerId(int playerId)
    //{
    //  var results = _repo.GetPlayerStatsSeasonByPlayerId(playerId);
    //  return results;
    //}

    //public List<PlayerStatSeason> GetPlayerStatsSeasonByPlayerIdSeasonId(int playerId, int seasonId)
    //{
    //  var results = _repo.GetPlayerStatsSeasonByPlayerIdSeasonId(playerId, seasonId);
    //  return results;
    //}
  }
}
