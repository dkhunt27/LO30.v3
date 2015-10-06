using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.GoalieStats
{
  public class GoalieStatsSeasonController : ApiController
  {
    public GoalieStatsSeasonController()
    {
    }

    public List<GoalieStatSeason> GetGoalieStatsSeason()
    {
      var results = new List<GoalieStatSeason>();

      using (var context = new LO30Context())
      {
        results = context.GoalieStatSeasons
                          .IncludeAll()
                          .ToList();
      }
      return results.OrderBy(x => x.Player.LastName)
                    .ThenBy(x => x.Player.FirstName)
                    .ToList();
    }

    public List<GoalieStatSeason> GetGoalieStatsSeasonByPlayerId(int playerId)
    {
      var results = new List<GoalieStatSeason>();

      using (var context = new LO30Context())
      {
        results = context.GoalieStatSeasons
                          .Where(x=>x.PlayerId == playerId)
                          .IncludeAll()
                          .ToList();
      }
      return results.OrderBy(x => x.Player.LastName)
                    .ThenBy(x => x.Player.FirstName)
                    .ToList();
    }

    public List<GoalieStatSeason> GetGoalieStatsSeasonByPlayerIdSeasonId(int playerId, int seasonId)
    {
      var results = new List<GoalieStatSeason>();

      using (var context = new LO30Context())
      {
        results = context.GoalieStatSeasons
                          .Where(x => x.PlayerId == playerId && x.SeasonId == seasonId)
                          .IncludeAll()
                          .ToList();
      }

      return results.OrderBy(x => x.Player.LastName)
                    .ThenBy(x=>x.Player.FirstName)
                    .ToList();
    }
  }
}
