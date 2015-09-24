using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.GoalieStats
{
  public class GoalieStatsSeasonController : ApiController
  {
    public GoalieStatsSeasonController()
    {
    }

    //public List<GoalieStatSeason> GetGoalieStatsSeason()
    //{
    //  var results = _repo.GetGoalieStatsSeason();
    //  return results.ToList();
    //}

    //public List<GoalieStatSeason> GetGoalieStatsSeasonByPlayerId(int playerId)
    //{
    //  var results = _repo.GetGoalieStatsSeasonByPlayerId(playerId);
    //  return results;
    //}

    //public List<GoalieStatSeason> GetGoalieStatsSeasonByPlayerIdSeasonId(int playerId, int seasonId)
    //{
    //  var results = _repo.GetGoalieStatsSeasonByPlayerIdSeasonId(playerId, seasonId);
    //  return results;
    //}
  }
}
