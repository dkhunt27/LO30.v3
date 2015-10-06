using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.PlayerStats
{
  public class PlayerStatsGameController : ApiController
  {
    public PlayerStatsGameController()
    {
    }

    public List<PlayerStatGame> GetPlayerStatsGame()
    {
      var results = new List<PlayerStatGame>();

      using (var context = new LO30Context())
      {
        results = context.PlayerStatGames
                          .IncludeAll()
                          .ToList();
      }
      return results.OrderBy(x => x.Player.LastName)
               .ThenBy(x => x.Player.FirstName)
               .ToList();
    }

    public List<PlayerStatGame> GetPlayerStatsGameByPlayerId(int playerId)
    {
      var results = new List<PlayerStatGame>();

      using (var context = new LO30Context())
      {
        results = context.PlayerStatGames
                          .Where(x => x.PlayerId == playerId)
                          .IncludeAll()
                          .ToList();
      }
      return results.OrderBy(x => x.Player.LastName)
               .ThenBy(x => x.Player.FirstName)
               .ToList();
    }

    public List<PlayerStatGame> GetPlayerStatsGameByPlayerIdSeasonId(int playerId, int seasonId)
    {
      var results = new List<PlayerStatGame>();

      using (var context = new LO30Context())
      {
        results = context.PlayerStatGames
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
