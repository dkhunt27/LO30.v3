using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.ForWebs
{
  public class ForWebTeamStandingsController : ApiController
  {
    
    public ForWebTeamStandingsController()
    {
      
    }

    public List<ForWebTeamStanding> GetForWebTeamStandings(int seasonId, bool playoffs)
    {
      var results = new List<ForWebTeamStanding>();

      using (var context = new LO30Context())
      {
        results = context.ForWebTeamStandings.Where(x => x.SID == seasonId && x.PFS == playoffs).ToList();
      }
      return results.OrderByDescending(x => x.PTS).ToList();
    }
  }
}
