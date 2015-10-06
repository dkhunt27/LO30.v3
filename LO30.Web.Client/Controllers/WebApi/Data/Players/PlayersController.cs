using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.Players
{
  public class PlayersController : ApiController
  {
    public PlayersController()
    {
    }

    public List<Player> GetPlayers()
    {
      var results = new List<Player>();

      using (var context = new LO30Context())
      {
        results = context.Players
                          .IncludeAll()
                          .ToList();
      }
      return results.OrderBy(x => x.LastName)
               .ThenBy(x => x.FirstName)
               .ToList();
    }
  }
}
