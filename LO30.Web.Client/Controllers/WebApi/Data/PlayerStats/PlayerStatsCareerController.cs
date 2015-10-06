using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.PlayerStats
{
  public class PlayerStatsCareerController : ApiController
  {
    public PlayerStatsCareerController()
    {
    }

    public List<PlayerStatCareer> GetPlayerStatsCareer()
    {
      var results = new List<PlayerStatCareer>();

      using (var context = new LO30Context())
      {
        results = context.PlayerStatCareers
                          .IncludeAll()
                          .ToList();
      }
      return results.OrderBy(x => x.Player.LastName)
               .ThenBy(x => x.Player.FirstName)
               .ToList();
    }

    public List<PlayerStatCareer> GetPlayerStatsCareerByPlayerId(int playerId)
    {
      var results = new List<PlayerStatCareer>();

      using (var context = new LO30Context())
      {
        results = context.PlayerStatCareers
                          .Where(x => x.PlayerId == playerId)
                          .IncludeAll()
                          .ToList();
      }
      return results.OrderBy(x => x.Player.LastName)
               .ThenBy(x => x.Player.FirstName)
               .ToList();
    }
  }
}
