using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.ForWebs
{
  public class ForWebTeamStandingsDataGoodThruController : ApiController
  {
    public ForWebTeamStandingsDataGoodThruController()
    {
    }

    public ForWebGoodThru GetDataGoodThru(int seasonId)
    {
      var results = new ForWebGoodThru() { GT = "n/a" };

      using (var context = new LO30Context())
      {
        var maxGameData = context.GameOutcomes
              .GroupBy(x => new { x.SeasonId, x.Game.Playoffs })
              .Select(grp => new
              {
                SeasonId = grp.Key.SeasonId,
                Playoffs = grp.Key.Playoffs,
                GameId = grp.Max(x => x.GameId),
                GameDateTime = grp.Max(x => x.Game.GameDateTime)
              })
              .Where(x => x.SeasonId == seasonId)
              .OrderByDescending(x => x.GameDateTime)
              .ToList();

        var max = maxGameData.FirstOrDefault();
        if (max != null)
        {
          results = new ForWebGoodThru() { GT = max.GameDateTime.ToShortDateString() };
        }

        return results;
      }
    }
  }
}
