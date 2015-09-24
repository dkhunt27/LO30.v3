using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.GoalieStats
{
  public class GoalieStatsGameController : ApiController
  {
    public GoalieStatsGameController()
    {
    }

    //public List<GoalieStatGame> GetGoalieStatsGame()
    //{
    //  var results = _repo.GetGoalieStatsGame();
    //  return results.ToList();
    //}

    //public List<GoalieStatGame> GetGoalieStatsGameByGameId(int gameId)
    //{
    //  var results = _repo.GetGoalieStatsGameByGameId(gameId);
    //  return results;
    //}

    //public List<GoalieStatGame> GetGoalieStatsGameByPlayerIdSeasonId(int playerId, int seasonId)
    //{
    //  var results = _repo.GetGoalieStatsGameByPlayerIdSeasonId(playerId, seasonId);
    //  return results;
    //}
  }
}
