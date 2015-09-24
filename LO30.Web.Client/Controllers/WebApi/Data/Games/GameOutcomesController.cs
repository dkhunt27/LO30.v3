using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.Games
{
  public class GameOutcomesController : ApiController
  {
    public GameOutcomesController()
    {
    }

    //public List<GameOutcome> GetGameOutcomes(bool fullDetail = true)
    //{
    //  var results = _repo.GetGameOutcomes(fullDetail);
    //  return results.OrderByDescending(x => x.GameTeam.GameId)
    //                .ToList();
    //}

    //public List<GameOutcome> GetGameOutcomesByGameId(int gameId, bool fullDetail = true)
    //{
    //  var results = _repo.GetGameOutcomesByGameId(gameId, fullDetail);
    //  return results.OrderByDescending(x => x.GameTeam.GameId)
    //                .ToList();
    //}

  }
}
