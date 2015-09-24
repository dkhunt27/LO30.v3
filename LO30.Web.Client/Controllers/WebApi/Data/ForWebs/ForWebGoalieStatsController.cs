using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.ForWebs
{
  public class ForWebGoalieStatsController : ApiController
  {
    public ForWebGoalieStatsController()
    {
    }

    public List<ForWebGoalieStat> GetForWebGoalieStats(int seasonId, bool playoffs)
    {
      var results = new List<ForWebGoalieStat>();

      using (var context = new LO30Context())
      {
        results = context.ForWebGoalieStats.Where(x => x.SID == seasonId && x.PFS == playoffs).ToList();
      }
      return results.OrderBy(x => x.GAA).ToList();
    }
  }
}
