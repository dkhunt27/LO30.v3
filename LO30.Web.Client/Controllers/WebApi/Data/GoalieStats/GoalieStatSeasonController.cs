using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.GoalieStats
{
  public class GoalieStatSeasonController : ApiController
  {
    public GoalieStatSeasonController()
    {
    }

    public GoalieStatSeason GetGoalieStatSeasonByPlayerIdSeasonIdSub(int playerId, int seasonId, bool playoffs, bool sub)
    {
      var results = new GoalieStatSeason();

      using (var context = new LO30Context())
      {
        results = context.GoalieStatSeasons
                          .Where(x => x.PlayerId == playerId && x.SeasonId == seasonId && x.Playoffs == playoffs && x.Sub == sub)
                          .IncludeAll()
                          .SingleOrDefault();
      }
      return results;
    }
  }
}
