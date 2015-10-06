using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.PlayerStats
{
  public class PlayerStatCareerController : ApiController
  {
    public PlayerStatCareerController()
    {
    }

    public PlayerStatCareer GetPlayerStatCareerByPlayerIdPlayoffs(int playerId, bool playoffs)
    {
      var results = new PlayerStatCareer();

      using (var context = new LO30Context())
      {
        results = context.PlayerStatCareers
                          .Where(x => x.PlayerId == playerId && x.Playoffs == playoffs)
                          .IncludeAll()
                          .SingleOrDefault();
      }
      return results;
    }

  }
}
