using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.Games
{
  public class GameRostersController : ApiController
  {
    public GameRostersController()
    {
    }

    //public List<GameRoster> GetGameRosters()
    //{
    //  var results = _repo.GetGameRosters();
    //  return results.OrderByDescending(x => x.GameTeam.GameId)
    //                .OrderBy(x => x.Line)
    //                .OrderByDescending(x => x.Position)
    //                .OrderBy(x => x.RatingPrimary)
    //                .OrderBy(x => x.RatingSecondary)
    //                .ToList();
    //}

    //public List<GameRoster> GetGameRostersByGameId(int gameId)
    //{
    //  var results = _repo.GetGameRostersByGameId(gameId);
    //  return results.OrderByDescending(x => x.GameTeam.GameId)
    //                .OrderBy(x=>x.Line)
    //                .OrderByDescending(x=>x.Position)
    //                .OrderBy(x=>x.RatingPrimary)
    //                .OrderBy(x=>x.RatingSecondary)
    //                .ToList();
    //}

    //public List<GameRoster> GetGameRostersByGameIdAndHomeTeam(int gameId, bool homeTeam)
    //{
    //  var results = _repo.GetGameRostersByGameIdAndHomeTeam(gameId, homeTeam);
    //  return results.OrderByDescending(x => x.GameTeam.GameId)
    //                .OrderBy(x => x.Line)
    //                .OrderByDescending(x => x.Position)
    //                .OrderBy(x => x.RatingPrimary)
    //                .OrderBy(x => x.RatingSecondary)
    //                .ToList();
    //}
  }
}
