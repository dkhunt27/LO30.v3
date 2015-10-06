using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.Games
{
  public class GameScoresController : ApiController
  {
    public GameScoresController()
    {
    }

    public List<GameScore> GetGameScores(bool fullDetail = true)
    {
      var results = new List<GameScore>();

      using (var context = new LO30Context())
      {
        results = context.GameScores
                          .IncludeAll()
                          .ToList();
      }
      return results.OrderByDescending(x => x.GameId)
                    .ThenBy(x=>x.Period)
                    .ThenBy(x=>x.TeamId)
                    .ToList();
    }

    public List<GameScore> GetGameScoresByGameId(int gameId, bool fullDetail = true)
    {
      var results = new List<GameScore>();

      using (var context = new LO30Context())
      {
        results = context.GameScores
                          .Where(x=>x.GameId == gameId)
                          .IncludeAll()
                          .ToList();
      }
      return results.OrderByDescending(x => x.GameId)
                    .ThenBy(x => x.Period)
                    .ThenBy(x => x.TeamId)
                    .ToList();
    }
  }
}
