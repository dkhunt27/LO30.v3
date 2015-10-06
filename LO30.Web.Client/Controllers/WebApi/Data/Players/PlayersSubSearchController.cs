using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.Players
{
  public class PlayersSubSearchController : ApiController
  {
    public PlayersSubSearchController()
    {
    }

    //public List<PlayerSubSearch> GetPlayersSubSearch(string position, string ratingMin, string ratingMax)
    //{
    //  var results = _repo.GetPlayersSubSearch(position, ratingMin, ratingMax);
    //  return results.OrderBy(x => x.Rating)
    //                .ToList();

    //  var results = new List<PlayerStatCareer>();

    //  using (var context = new LO30Context())
    //  {
    //    results = context.PlayerStatCareers
    //                      .Where(x => x.PlayerId == playerId)
    //                      .IncludeAll()
    //                      .ToList();
    //  }
    //  return results.OrderBy(x => x.Player.LastName)
    //           .ThenBy(x => x.Player.FirstName)
    //           .ToList();
    //}

  }
}
