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

    //public GameRoster GetByGameRosterId(int gameRosterId)
    //{
    //  var result = _repo.GetGameRosterByGameRosterId(gameRosterId);
    //  return result;
    //}

    //public GameRoster GetGameRosterByGameTeamIdAndPlayerNumber(int gameTeamId, string playerNumber)
    //{
    //  var result = _repo.GetGameRosterByGameTeamIdAndPlayerNumber(gameTeamId, playerNumber);
    //  return result;
    //}
  }
}
