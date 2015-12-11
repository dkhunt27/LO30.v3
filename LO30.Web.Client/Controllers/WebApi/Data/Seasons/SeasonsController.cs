using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;
using LO30.Data.Objects;
using System;

namespace LO30.Controllers.Data.Seasons
{
  public class SeasonsController : ApiController
  {
    public SeasonsController()
    {
    }

    [System.Web.Http.AcceptVerbs("GET", "POST")]
    [System.Web.Http.HttpGet]
    public List<Season> ListSeasons()
    {
      var results = new List<Season>();
      using (var context = new LO30Context())
      {
        results = context.Seasons
                          .Where(x=>x.SeasonId > 0)
                          .ToList();
      }

      return results.OrderByDescending(x => x.SeasonName).ToList();
    }
  }
}
