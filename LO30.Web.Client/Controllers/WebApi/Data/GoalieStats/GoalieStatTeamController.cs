using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.GoalieStats
{
  public class GoalieStatTeamController : ApiController
  {
    public GoalieStatTeamController()
    {
    }

    public GoalieStatTeam GetGoalieStatTeamByPlayerIdTeamId(int playerId, int teamId, bool playoffs)
    {
      var results = new GoalieStatTeam();

      using (var context = new LO30Context())
      {
        results = context.GoalieStatTeams
                          .Where(x=>x.PlayerId == playerId && x.TeamId == teamId && x.Playoffs == playoffs)
                          .IncludeAll()
                          .SingleOrDefault();
      }
      return results;
    }
  }
}
