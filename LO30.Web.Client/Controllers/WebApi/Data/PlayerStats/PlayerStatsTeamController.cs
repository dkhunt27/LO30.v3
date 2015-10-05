using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.PlayerStats
{
  public class PlayerStatsTeamController : ApiController
  {
    public PlayerStatsTeamController()
    {
    }

    //public List<PlayerStatTeam> GetPlayerStatsTeam()
    //{
    //  var results = _repo.GetPlayerStatsTeam();
    //  return results.ToList();
    //}

    //public List<PlayerStatTeam> GetPlayerStatsTeamByPlayerId(int playerId)
    //{
    //  var results = _repo.GetPlayerStatsTeamByPlayerId(playerId);
    //  return results;
    //}

    //public List<PlayerStatTeam> GetPlayerStatsTeamByPlayerIdSeasonId(int playerId, int seasonId)
    //{
    //  var results = _repo.GetPlayerStatsTeamByPlayerIdSeasonId(playerId, seasonId);
    //  return results;
    //}
  }
}
