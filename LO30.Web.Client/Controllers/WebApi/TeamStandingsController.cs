using LO30.Data;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers
{
  public class TeamStandingsController : ApiController
  {
    
    public TeamStandingsController()
    {
      
    }

    //public IEnumerable<TeamStanding> Get()
    //{
    //  var results = _repo.GetTeamStandings();
    //  var standings = results.OrderBy(t => t.Rank).ToList();

    //  return standings;
    //}
  }
}
