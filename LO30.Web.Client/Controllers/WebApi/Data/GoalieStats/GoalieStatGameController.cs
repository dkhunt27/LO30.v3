using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.GoalieStats
{
  public class GoalieStatGameController : ApiController
  {
    public GoalieStatGameController()
    {
    }

    public GoalieStatGame GetGoalieStatGameByPlayerIdGameId(int playerId, int gameId)
    {
      var results = new GoalieStatGame();

      using (var context = new LO30Context())
      {
        results = context.GoalieStatGames
                          .Where(x => x.PlayerId == playerId && x.GameId == gameId)
                          .IncludeAll()
                          .SingleOrDefault();
      }
      return results;
    }
  }
}
