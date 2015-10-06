using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.Games
{
  public class GameTeamsController : ApiController
  {
    public GameTeamsController()
    {
    }

    public List<GameTeam> GetGameTeams()
    {
      var results = new List<GameTeam>();

      using (var context = new LO30Context())
      {
        results = context.GameTeams
                          .IncludeAll()
                          .ToList();
      }
      return results.OrderByDescending(x => x.GameId)
                    .ToList();
    }

    public List<GameTeam> GetGameTeamsByGameId(int gameId)
    {
      var results = new List<GameTeam>();

      using (var context = new LO30Context())
      {
        results = context.GameTeams
                          .Where(x=>x.GameId == gameId)
                          .IncludeAll()
                          .ToList();
      }
      return results;
    }
  }
}
