using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.GoalieStats
{
  public class GoalieStatsTeamController : ApiController
  {
    public GoalieStatsTeamController()
    {
    }

    public List<GoalieStatTeam> GetGoalieStatsTeam()
    {
      var results = new List<GoalieStatTeam>();

      using (var context = new LO30Context())
      {
        results = context.GoalieStatTeams
                          .IncludeAll()
                          .ToList();
      }
      return results.OrderBy(x => x.Player.LastName)
                    .ThenBy(x=>x.Player.FirstName)
                    .ToList();
    }

    public List<GoalieStatTeam> GetGoalieStatsTeamByPlayerId(int playerId)
    {
      var results = new List<GoalieStatTeam>();

      using (var context = new LO30Context())
      {
        results = context.GoalieStatTeams
                          .Where(x=>x.PlayerId == playerId)
                          .IncludeAll()
                          .ToList();
      }
      return results.OrderBy(x => x.Player.LastName)
                    .ThenBy(x => x.Player.FirstName)
                    .ToList();
    }

    public List<GoalieStatTeam> GetGoalieStatsTeamByPlayerIdSeasonId(int playerId, int seasonId)
    {
      var results = new List<GoalieStatTeam>();

      using (var context = new LO30Context())
      {
        results = context.GoalieStatTeams
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
