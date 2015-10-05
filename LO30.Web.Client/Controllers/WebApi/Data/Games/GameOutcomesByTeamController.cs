using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.Games
{
  public class GameOutcomesByTeamController : ApiController
  {
    public GameOutcomesByTeamController()
    {
    }

    public List<GameOutcome> GetGameOutcomesByTeamId(int seasonId, bool playoffs, int teamId)
    {
      var results = new List<GameOutcome>();

      using (var context = new LO30Context())
      {
        results = context.GameOutcomes
                          .Where(x => x.SeasonId == seasonId && x.Game.Playoffs == playoffs && x.TeamId == teamId)
                          .IncludeAll()
                          .ToList();
      }
      return results.OrderByDescending(x => x.GameId)
                    .ToList();

    }
  }
}
