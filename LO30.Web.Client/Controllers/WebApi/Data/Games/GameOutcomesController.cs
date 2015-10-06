using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.Games
{
  public class GameOutcomesController : ApiController
  {
    public GameOutcomesController()
    {
    }

    public List<GameOutcome> GetGameOutcomes()
    {
      var results = new List<GameOutcome>();

      using (var context = new LO30Context())
      {
        results = context.GameOutcomes
                          .IncludeAll()
                          .ToList();
      }
      return results.OrderByDescending(x => x.GameId)
                    .ToList();
    }

    public List<GameOutcome> GetGameOutcomesByGameId(int gameId, bool fullDetail = true)
    {
      var results = new List<GameOutcome>();

      using (var context = new LO30Context())
      {
        results = context.GameOutcomes
                          .Where(x=>x.GameId == gameId)
                          .IncludeAll()
                          .ToList();
      }
      return results.OrderByDescending(x => x.GameId)
                    .ToList();
    }

  }
}
