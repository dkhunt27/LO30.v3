using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.PlayerStats
{
  public class PlayerStatsSeasonController : ApiController
  {
    public PlayerStatsSeasonController()
    {
    }

    public List<PlayerStatSeason> GetPlayerStatsSeason()
    {
      var results = new List<PlayerStatSeason>();

      using (var context = new LO30Context())
      {
        results = context.PlayerStatSeasons
                          .IncludeAll()
                          .ToList();
      }

      return results.OrderBy(x => x.Player.LastName)
                    .ThenBy(x => x.Player.FirstName)
                    .ToList();
    }

    public List<PlayerStatSeason> GetPlayerStatsSeasonByPlayerId(int playerId)
    {
      var results = new List<PlayerStatSeason>();

      using (var context = new LO30Context())
      {
        results = context.PlayerStatSeasons
                          .Where(x => x.PlayerId == playerId)
                          .IncludeAll()
                          .ToList();
      }

      return results.OrderBy(x => x.Player.LastName)
                    .ThenBy(x => x.Player.FirstName)
                    .ToList();
    }

    public List<PlayerStatSeason> GetPlayerStatsSeasonByPlayerIdSeasonId(int playerId, int seasonId)
    {
      var results = new List<PlayerStatSeason>();

      using (var context = new LO30Context())
      {
        results = context.PlayerStatSeasons
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
