using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

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
    //}

  }
}
