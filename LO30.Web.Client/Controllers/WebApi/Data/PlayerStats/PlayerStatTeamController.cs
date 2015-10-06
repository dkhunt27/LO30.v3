using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.PlayerStats
{
  public class PlayerStatTeamController : ApiController
  {
    public PlayerStatTeamController()
    {
    }

    public PlayerStatTeam GetPlayerStatTeamByPlayerIdTeamId(int playerId, int teamId, bool playoffs)
    {
      var results = new PlayerStatTeam();

      using (var context = new LO30Context())
      {
        results = context.PlayerStatTeams
                          .Where(x => x.PlayerId == playerId && x.TeamId == teamId && x.Playoffs == playoffs)
                          .IncludeAll()
                          .SingleOrDefault();
      }
      return results;
    }
  }
}
