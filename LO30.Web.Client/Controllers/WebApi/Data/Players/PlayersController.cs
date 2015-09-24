using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.Players
{
  public class PlayersController : ApiController
  {
    public PlayersController()
    {
    }

    //public List<Player> GetPlayers()
    //{
    //  var results = _repo.GetPlayers();
    //  return results.OrderBy(x => x.LastName)
    //                .OrderBy(x => x.FirstName)
    //                .ToList();
    //}
  }
}
