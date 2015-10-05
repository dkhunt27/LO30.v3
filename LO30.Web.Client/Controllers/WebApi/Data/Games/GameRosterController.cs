using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.Games
{
  public class GameRosterController : ApiController
  {
    public GameRosterController()
    {
    }

    public GameRoster GetByGameRosterId(int gameRosterId)
    {
      var results = new GameRoster();

      using (var context = new LO30Context())
      {
        results = context.GameRosters.Where(x => x.GameRosterId == gameRosterId).FirstOrDefault();
      }
      return results;
    }

    public GameRoster GetGameRosterByGameIdTeamIdAndPlayerNumber(int gameId, int teamId, string playerNumber)
    {
      var results = new GameRoster();

      using (var context = new LO30Context())
      {
        results = context.GameRosters.Where(x=>x.GameId == gameId && x.TeamId == teamId && x.PlayerNumber == playerNumber).FirstOrDefault();
      }
      return results;
    }
  }
}
