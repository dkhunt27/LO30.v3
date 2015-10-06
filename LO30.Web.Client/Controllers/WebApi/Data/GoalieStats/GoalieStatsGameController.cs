using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.GoalieStats
{
  public class GoalieStatsGameController : ApiController
  {
    public GoalieStatsGameController()
    {
    }

    public List<GoalieStatGame> GetGoalieStatsGame()
    {
      var results = new List<GoalieStatGame>();

      using (var context = new LO30Context())
      {
        results = context.GoalieStatGames
                          .IncludeAll()
                          .ToList();
      }
      return results.OrderBy(x => x.Player.LastName)
                  .ThenBy(x => x.Player.FirstName)
                  .ToList();
    }

    public List<GoalieStatGame> GetGoalieStatsGameByGameId(int gameId)
    {
      var results = new List<GoalieStatGame>();

      using (var context = new LO30Context())
      {
        results = context.GoalieStatGames
                          .Where(x => x.GameId == gameId)
                          .IncludeAll()
                          .ToList();
      }
      return results.OrderBy(x => x.Player.LastName)
                 .ThenBy(x => x.Player.FirstName)
                 .ToList();
    }

    public List<GoalieStatGame> GetGoalieStatsGameByPlayerIdSeasonId(int playerId, int seasonId)
    {
      var results = new List<GoalieStatGame>();

      using (var context = new LO30Context())
      {
        results = context.GoalieStatGames
                          .Where(x => x.PlayerId == playerId && x.SeasonId == seasonId)
                          .IncludeAll()
                          .ToList();
      }
      return results.OrderBy(x => x.Player.LastName)
               .ThenBy(x => x.Player.FirstName)
               .ToList();
    }
  }
}
