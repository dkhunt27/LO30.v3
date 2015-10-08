using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data
{
  public class TeamRostersController : ApiController
  {
    
    public TeamRostersController()
    {
      
    }

    public List<TeamRoster> GetTeamRosters()
    {
      var results = new List<TeamRoster>();

      using (var context = new LO30Context())
      {
        results = context.TeamRosters.IncludeAll().ToList();
      }
      return results.OrderByDescending(x => x.SeasonId)
                    .ThenByDescending(x=> x.TeamId)
                    .ThenBy(x => x.StartYYYYMMDD)
                    .ThenBy(x => x.EndYYYYMMDD)
                    .ThenBy(x => x.PlayerNumber)
                    .ToList();
    }

    public List<TeamRoster> GetTeamRostersByTeamIdAndYYYYMMDD(int teamId, int yyyymmdd)
    {
      var results = new List<TeamRoster>();

      using (var context = new LO30Context())
      {
        results = context.TeamRosters.Where(x => x.TeamId == teamId && x.StartYYYYMMDD <= yyyymmdd && x.EndYYYYMMDD >= yyyymmdd).IncludeAll().ToList();
      }
      return results.OrderByDescending(x => x.SeasonId)
                    .ThenByDescending(x => x.TeamId)
                    .ThenBy(x => x.StartYYYYMMDD)
                    .ThenBy(x => x.EndYYYYMMDD)
                    .ThenBy(x => x.PlayerNumber)
                    .ToList();
    }

    public TeamRoster GetTeamRosterByTeamIdYYYYMMDDAndPlayerId(int teamId, int yyyymmdd, int playerId)
    {
      var results = new TeamRoster();

      using (var context = new LO30Context())
      {
        results = context.TeamRosters.Where(x => x.TeamId == teamId && 
          x.StartYYYYMMDD <= yyyymmdd && 
          x.EndYYYYMMDD >= yyyymmdd &&
          x.PlayerId == playerId).IncludeAll().FirstOrDefault();
      }
      return results;
    }
  }
}
