using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.PlayerStats
{
  public class PlayerStatsTeamController : ApiController
  {
    public PlayerStatsTeamController()
    {
    }

    public List<PlayerStatTeam> GetPlayerStatsTeam()
    {
      var results = new List<PlayerStatTeam>();

      using (var context = new LO30Context())
      {
        results = context.PlayerStatTeams
                          .IncludeAll()
                          .ToList();
      }
      return results.OrderBy(x => x.Player.LastName)
                    .ThenBy(x => x.Player.FirstName)
                    .ToList();
    }

    public List<PlayerStatTeam> GetPlayerStatsTeamByPlayerId(int playerId)
    {
      var results = new List<PlayerStatTeam>();

      using (var context = new LO30Context())
      {
        results = context.PlayerStatTeams
                          .Where(x => x.PlayerId == playerId)
                          .IncludeAll()
                          .ToList();
      }
      return results.OrderBy(x => x.Player.LastName)
                    .ThenBy(x => x.Player.FirstName)
                    .ToList();
    }

    public List<PlayerStatTeam> GetPlayerStatsTeamByPlayerIdSeasonId(int playerId, int seasonId)
    {
      var results = new List<PlayerStatTeam>();

      using (var context = new LO30Context())
      {
        results = context.PlayerStatTeams
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
