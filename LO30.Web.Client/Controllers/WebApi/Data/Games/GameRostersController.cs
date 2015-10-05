using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.Games
{
  public class GameRostersController : ApiController
  {
    public GameRostersController()
    {
    }

    public List<GameRoster> GetGameRosters()
    {
      var results = new List<GameRoster>();

      using (var context = new LO30Context())
      {
        results = context.GameRosters.ToList();
      }
      return results.OrderByDescending(x => x.GameId)
                    .OrderBy(x => x.Line)
                    .OrderByDescending(x => x.Position)
                    .OrderBy(x => x.RatingPrimary)
                    .OrderBy(x => x.RatingSecondary)
                    .ToList();
    }

    public List<GameRoster> GetGameRostersByGameId(int gameId)
    {
      var results = new List<GameRoster>();

      using (var context = new LO30Context())
      {
        results = context.GameRosters.Where(x=>x.GameId == gameId).ToList();
      }
      return results.OrderByDescending(x => x.GameId)
                    .OrderBy(x => x.Line)
                    .OrderByDescending(x => x.Position)
                    .OrderBy(x => x.RatingPrimary)
                    .OrderBy(x => x.RatingSecondary)
                    .ToList();
    }

    public List<GameRoster> GetGameRostersByGameIdAndHomeTeam(int gameId, bool homeTeam)
    {
      var results = new List<GameRoster>();

      // TODO ...wire up homeTeam
      using (var context = new LO30Context())
      {
        results = context.GameRosters.Where(x=>x.GameId == gameId).ToList();
      }
      return results.OrderByDescending(x => x.GameId)
                    .OrderBy(x => x.Line)
                    .OrderByDescending(x => x.Position)
                    .OrderBy(x => x.RatingPrimary)
                    .OrderBy(x => x.RatingSecondary)
                    .ToList();
    }
  }
}
