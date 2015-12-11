using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;
using LO30.Data.Models;

namespace LO30.Controllers.Data.Players
{
  public class PlayerCompositesController : ApiController
  {
    public PlayerCompositesController()
    {
    }

    public List<PlayerComposite> GetPlayerComposites(int yyyymmdd, bool active)
    {
      var results = new List<PlayerComposite>();

      using (var context = new LO30Context())
      {

        var joinWithPlayerStatuses = context.Players
                          .Join(context.PlayerStatuses,
                                x => x.PlayerId,
                                ps => ps.PlayerId,
                                (x, ps) => new { x, ps })
                          .Select(m => new
                          {
                            PlayerId = m.x.PlayerId,
                            FirstName = m.x.FirstName,
                            LastName = m.x.LastName,
                            Suffix = m.x.Suffix,
                            PreferredPosition = m.x.PreferredPosition,
                            Shoots = m.x.Shoots,
                            CurrentStatus = m.ps.CurrentStatus,
                            PlayerStatusTypeId = m.ps.PlayerStatusTypeId
                          })
                          .Where(m => m.CurrentStatus == true);

        List<string> statuses;

        if (active)
        {
          statuses = new List<string>() { "League Member", "Invited Into League", "One Year Sub", "Quick Sub" };
        } 
        else
        {
          statuses = new List<string>() { "League Member", "Invited Into League", "One Year Sub", "Quick Sub", "On Leave", "Not In League", "Retired From League", "For Historical Stats" };
        }

        var joinWithPlayerStatusTypes = joinWithPlayerStatuses
                  .Join(context.PlayerStatusTypes,
                        x => x.PlayerStatusTypeId,
                        pst => pst.PlayerStatusTypeId,
                        (x, pst) => new { x, pst })
                  .Select(m => new
                  {
                    PlayerId = m.x.PlayerId,
                    FirstName = m.x.FirstName,
                    LastName = m.x.LastName,
                    Suffix = m.x.Suffix,
                    PreferredPosition = m.x.PreferredPosition,
                    Shoots = m.x.Shoots,
                    CurrentStatus = m.x.CurrentStatus,
                    PlayerStatusTypeId = m.x.PlayerStatusTypeId,
                    PlayerStatusTypeName = m.pst.PlayerStatusTypeName
                  })
                  .Where(m => statuses.Contains(m.PlayerStatusTypeName));

        var joinWithTeamRosters = joinWithPlayerStatusTypes
                          .GroupJoin(context.TeamRosters,
                                x => x.PlayerId,
                                tr => tr.PlayerId,
                                (x, tr) => new { x, tr })
                          .SelectMany(m => m.tr.DefaultIfEmpty(), (m, tr) => new
                          {
                            PlayerId = m.x.PlayerId,
                            FirstName = m.x.FirstName,
                            LastName = m.x.LastName,
                            Suffix = m.x.Suffix,
                            PreferredPosition = m.x.PreferredPosition,
                            Shoots = m.x.Shoots,
                            CurrentStatus = m.x.CurrentStatus,
                            TeamId = tr.TeamId,
                            TeamRosterStartYYYYMMDD = tr.StartYYYYMMDD,
                            TeamRosterEndYYYYMMDD = tr.EndYYYYMMDD
                          })
                          .Where(m => m.TeamRosterStartYYYYMMDD == null || ( m.TeamRosterStartYYYYMMDD <= yyyymmdd && yyyymmdd <= m.TeamRosterEndYYYYMMDD));

        var joinWithTeams = joinWithTeamRosters
                          .GroupJoin(context.Teams,
                                x => x.TeamId,
                                t => t.TeamId,
                                (x, t) => new { x, t })
                          .SelectMany(m => m.t.DefaultIfEmpty(), (m, t) => new
                          {
                            PlayerId = m.x.PlayerId,
                            FirstName = m.x.FirstName,
                            LastName = m.x.LastName,
                            Suffix = m.x.Suffix,
                            PreferredPosition = m.x.PreferredPosition,
                            Shoots = m.x.Shoots,
                            CurrentStatus = m.x.CurrentStatus,
                            TeamId = m.x.TeamId,
                            TeamRosterStartYYYYMMDD = m.x.TeamRosterStartYYYYMMDD,
                            TeamRosterEndYYYYMMDD = m.x.TeamRosterEndYYYYMMDD,
                            TeamCode = t.TeamCode,
                            TeamNameLong = t.TeamNameLong,
                            TeamNameShort = t.TeamNameShort
                          });

        /*var joinWithPlayerRatings = joinWithTeams
                         .Join(context.PlayerRatings,
                               p => p.PlayerId,
                               pr => pr.PlayerId,
                               (p, pr) => new { p, pr })
                         .Select(m => new
                         {
                            PlayerId = m.x.PlayerId,
                            FirstName = m.x.FirstName,
                            LastName = m.x.LastName,
                            Suffix = m.x.Suffix,
                            PreferredPosition = m.x.PreferredPosition,
                            Shoots = m.x.Shoots,
                            CurrentStatus = m.x.CurrentStatus,
                            TeamId = m.x.TeamId,
                            TeamRosterStartYYYYMMDD = m.x.TeamRosterStartYYYYMMDD,
                            TeamRosterEndYYYYMMDD = m.x.TeamRosterEndYYYYMMDD,
                            TeamCode = t.TeamCode,
                            TeamNameLong = t.TeamNameLong,
                            TeamNameShort = t.TeamNameShort,
                           RatingStartYYYYMMDD = m.pr.StartYYYYMMDD,
                           RatingEndYYYYMMDD = m.pr.EndYYYYMMDD,
                           RatingPosition = m.pr.Position,
                           RatingPrimary = m.pr.RatingPrimary,
                           RatingSecondary = m.pr.RatingSecondary
                         })
                         .Where(m => m.RatingStartYYYYMMDD <= yyyymmdd && yyyymmdd <= m.RatingEndYYYYMMDD);*/

        results = joinWithTeams
                 .Select(m => new PlayerComposite()
                 {
                   PlayerId = m.PlayerId,
                   FirstName = m.FirstName,
                   LastName = m.LastName,
                   Suffix = m.Suffix,
                   PreferredPosition = m.PreferredPosition,
                   Shoots = m.Shoots,
                   TeamCode = m.TeamCode,
                   TeamNameLong = m.TeamNameLong,
                   TeamNameShort = m.TeamNameShort
                 })
                 .ToList();
      }
      return results.OrderBy(x => x.LastName)
               .ThenBy(x => x.FirstName)
               .ToList();
    }
  }
}
