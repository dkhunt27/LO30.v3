using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.Games
{
  public class GameOutcomesBySeasonTeamController : ApiController
  {
    public GameOutcomesBySeasonTeamController()
    {
    }

    //public List<GameOutcome> GetGameOutcomesBySeasonTeamId(int seasonId, bool playoffs, int seasonTeamId, bool fullDetail = true)
    //{
    //  var results = _repo.GetGameOutcomesBySeasonTeamId(seasonId, playoffs, seasonTeamId, fullDetail);
    //  return results.OrderByDescending(x => x.GameTeam.GameId)
    //                .ToList();
    //}
  }
}
