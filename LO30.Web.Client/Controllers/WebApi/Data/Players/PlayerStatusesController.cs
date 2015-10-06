using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.Players
{
  public class PlayerStatusesController : ApiController
  {
    public PlayerStatusesController()
    {
    }

    public List<PlayerStatus> GetPlayerStatuses()
    {
      var results = new List<PlayerStatus>();

      using (var context = new LO30Context())
      {
        results = context.PlayerStatuses
                          .IncludeAll()
                          .ToList();
      }
      return results;
    }
  }
}
