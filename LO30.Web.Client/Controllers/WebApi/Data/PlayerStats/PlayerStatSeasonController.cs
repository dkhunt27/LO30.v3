using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.PlayerStats
{
  public class PlayerStatSeasonController : ApiController
  {
    public PlayerStatSeasonController()
    {
    }

    public PlayerStatSeason GetPlayerStatSeasonByPlayerIdSeasonIdPlayoffs(int playerId, int seasonId, bool playoffs)
    {
      var results = new PlayerStatSeason();

      using (var context = new LO30Context())
      {
        results = context.PlayerStatSeasons
                          .Where(x => x.PlayerId == playerId && x.SeasonId == seasonId && x.Playoffs == playoffs)
                          .IncludeAll()
                          .SingleOrDefault();
      }
      return results;
    }
  }
}
