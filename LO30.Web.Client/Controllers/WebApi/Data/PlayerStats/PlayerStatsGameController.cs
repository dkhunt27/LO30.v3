using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.PlayerStats
{
  public class PlayerStatsGameController : ApiController
  {
    public PlayerStatsGameController()
    {
    }

    //public List<PlayerStatGame> GetPlayerStatsGame()
    //{
    //  var results = _repo.GetPlayerStatsGame();
    //  return results.ToList();
    //}

    //public List<PlayerStatGame> GetPlayerStatsGameByPlayerId(int playerId)
    //{
    //  var results = _repo.GetPlayerStatsGameByPlayerId(playerId);
    //  return results;
    //}

    //public List<PlayerStatGame> GetPlayerStatsGameByPlayerIdSeasonId(int playerId, int seasonId)
    //{
    //  var results = _repo.GetPlayerStatsGameByPlayerIdSeasonId(playerId, seasonId);
    //  return results;
    //}
  }
}
