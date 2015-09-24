using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.ForWebs
{
  public class ForWebPlayerStatsController : ApiController
  {
    public ForWebPlayerStatsController()
    {
    }

    public List<ForWebPlayerStat> GetForWebPlayerStats(int seasonId, bool playoffs)
    {
      var results = new List<ForWebPlayerStat>();

      using (var context = new LO30Context())
      {
        results = context.ForWebPlayerStats.Where(x => x.SID == seasonId && x.PFS == playoffs).ToList();
      }
      return results.OrderByDescending(x => x.P).ToList();
    }
  }
}
