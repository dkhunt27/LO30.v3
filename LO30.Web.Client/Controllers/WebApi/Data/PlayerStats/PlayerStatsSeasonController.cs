using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;
using LO30.Data.Objects;

namespace LO30.Controllers.Data.PlayerStats
{
  public class PlayerStatsSeasonController : ApiController
  {
    public PlayerStatsSeasonController()
    {
    }

    public List<PlayerStatSeason> GetPlayerStatsSeason()
    {
      var results = new List<PlayerStatSeason>();

      using (var context = new LO30Context())
      {

          results = context.PlayerStatSeasons
                            .IncludeAll()
                            .ToList();
      }

      return results.OrderByDescending(x => x.SeasonId)
                    .ThenBy(x => x.PlayerId)
                    .ToList();
    }

    public List<PlayerStatSeason> GetPlayerStatsSeasonByPlayerId(int playerId)
    {
      var results = new List<PlayerStatSeason>();

      using (var context = new LO30Context())
      {

        results = context.PlayerStatSeasons
                            .IncludeAll()
                            .Where(x => x.PlayerId == playerId)
                            .ToList();

      }

      return results.OrderByDescending(x => x.SeasonId)
                    .ThenBy(x => x.PlayerId)
                    .ToList();
    }

    public List<PlayerStatSeason> GetPlayerStatsSeasonByPlayerIdSeasonId(int playerId, int seasonId)
    {
      var results = new List<PlayerStatSeason>();

      using (var context = new LO30Context())
      {

        results = context.PlayerStatSeasons
                            .IncludeAll()
                            .Where(x => x.PlayerId == playerId && x.SeasonId == seasonId)
                            .ToList();

      }

      return results.OrderByDescending(x => x.SeasonId)
                    .ThenBy(x => x.PlayerId)
                    .ToList();
    }

    /*
    public List<PlayerStatSeason> GetPlayerStatsSeason(bool totals)
    {
      var results = new List<PlayerStatSeason>();

      using (var context = new LO30Context())
      {
        if (totals)
        {
          results = context.PlayerStatSeasons
              .IncludeAll()
              .GroupBy(x => new { x.PlayerId, x.Player, x.SeasonId, x.Season })
              .Select(grp => new PlayerStatSeason
              {
                PlayerId = grp.Key.PlayerId,
                Player = grp.Key.Player,
                SeasonId = grp.Key.SeasonId,
                Season = grp.Key.Season,
                Games = grp.Sum(x => x.Games),
                Goals = grp.Sum(x => x.Goals),
                Assists = grp.Sum(x => x.Assists),
                Points = grp.Sum(x => x.Points),
                PenaltyMinutes = grp.Sum(x => x.PenaltyMinutes),
                PowerPlayGoals = grp.Sum(x => x.PowerPlayGoals),
                ShortHandedGoals = grp.Sum(x => x.ShortHandedGoals),
                GameWinningGoals = grp.Sum(x => x.GameWinningGoals)
              })
              .ToList();
        }
        else
        {
          results = context.PlayerStatSeasons
                            .IncludeAll()
                            .ToList();
        }
      }

      return results.OrderBy(x => x.Player.LastName)
                    .ThenBy(x => x.Player.FirstName)
                    .ToList();
    }

    public List<PlayerStatSeason> GetPlayerStatsSeasonByPlayerId(bool totals, int playerId)
    {
      var results = new List<PlayerStatSeason>();

      using (var context = new LO30Context())
      {
        if (totals)
        {
          results = context.PlayerStatSeasons
                        .IncludeAll()
                        .Where(x => x.PlayerId == playerId)
                        .GroupBy(x => new { x.PlayerId, x.Player, x.SeasonId, x.Season })
                        .Select(grp => new PlayerStatSeason
                        {
                          PlayerId = grp.Key.PlayerId,
                          Player = grp.Key.Player,
                          SeasonId = grp.Key.SeasonId,
                          Season = grp.Key.Season,
                          Games = grp.Sum(x => x.Games),
                          Goals = grp.Sum(x => x.Goals),
                          Assists = grp.Sum(x => x.Assists),
                          Points = grp.Sum(x => x.Points),
                          PenaltyMinutes = grp.Sum(x => x.PenaltyMinutes),
                          PowerPlayGoals = grp.Sum(x => x.PowerPlayGoals),
                          ShortHandedGoals = grp.Sum(x => x.ShortHandedGoals),
                          GameWinningGoals = grp.Sum(x => x.GameWinningGoals)
                        })
                        .ToList();
        } 
        else
        {
          results = context.PlayerStatSeasons
                  .Where(x => x.PlayerId == playerId)
                  .IncludeAll()
                  .ToList();
        }
      }

      return results.OrderBy(x => x.Player.LastName)
                    .ThenBy(x => x.Player.FirstName)
                    .ToList();
    }

    public List<PlayerStatSeason> GetPlayerStatsSeasonByPlayerIdSeasonId(bool totals, int playerId, int seasonId)
    {
      var results = new List<PlayerStatSeason>();

      using (var context = new LO30Context())
      {
        if (totals)
        {
          results = context.PlayerStatSeasons
                        .IncludeAll()
                        .Where(x => x.PlayerId == playerId && x.SeasonId == seasonId)
                        .GroupBy(x => new { x.PlayerId, x.Player, x.SeasonId, x.Season })
                        .Select(grp => new PlayerStatSeason
                        {
                          PlayerId = grp.Key.PlayerId,
                          Player = grp.Key.Player,
                          SeasonId = grp.Key.SeasonId,
                          Season = grp.Key.Season,
                          Games = grp.Sum(x => x.Games),
                          Goals = grp.Sum(x => x.Goals),
                          Assists = grp.Sum(x => x.Assists),
                          Points = grp.Sum(x => x.Points),
                          PenaltyMinutes = grp.Sum(x => x.PenaltyMinutes),
                          PowerPlayGoals = grp.Sum(x => x.PowerPlayGoals),
                          ShortHandedGoals = grp.Sum(x => x.ShortHandedGoals),
                          GameWinningGoals = grp.Sum(x => x.GameWinningGoals)
                        })
                        .ToList();
        }
        else
        {
          results = context.PlayerStatSeasons
                  .Where(x => x.PlayerId == playerId && x.SeasonId == seasonId)
                  .IncludeAll()
                  .ToList();
        }
      }

      return results.OrderBy(x => x.Player.LastName)
                    .ThenBy(x => x.Player.FirstName)
                    .ToList();
    }
     */
  }
}
