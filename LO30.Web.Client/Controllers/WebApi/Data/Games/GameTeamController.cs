using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.Games
{
  public class GameTeamController : ApiController
  {
    public GameTeamController()
    {
    }

    public GameTeam GetGameTeamByGameAndTeamId(int gameId, int teamId)
    {
      var results = new GameTeam();

      using (var context = new LO30Context())
      {
        results = context.GameTeams.Where(x=>x.GameId == gameId && x.TeamId == teamId).FirstOrDefault();
      }
      return results;
    }

    public GameTeam GetGameTeamByGameIdAndHomeTeam(int gameId, bool homeTeam)
    {
      var results = new GameTeam();

      using (var context = new LO30Context())
      {
        results = context.GameTeams.Where(x => x.GameId == gameId && x.HomeTeam == homeTeam).FirstOrDefault();
      }
      return results;
    }
  }
}
