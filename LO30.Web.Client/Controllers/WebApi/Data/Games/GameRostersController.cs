using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.Games
{
  public class GameRostersController : ApiController
  {
    public GameRostersController()
    {
    }

    public List<GameRoster> GetGameRosters()
    {
      var results = new List<GameRoster>();

      using (var context = new LO30Context())
      {
        results = context.GameRosters.ToList();
      }
      return results.OrderByDescending(x => x.GameId)
                    .OrderBy(x => x.Line)
                    .OrderByDescending(x => x.Position)
                    .OrderBy(x => x.RatingPrimary)
                    .OrderBy(x => x.RatingSecondary)
                    .ToList();
    }

    public List<GameRoster> GetGameRostersByGameId(int gameId)
    {
      var results = new List<GameRoster>();

      using (var context = new LO30Context())
      {
        results = context.GameRosters.Where(x => x.GameId == gameId).ToList();
      }
      return results.OrderByDescending(x => x.GameId)
                    .ThenBy(x => x.Line)
                    .ThenByDescending(x => x.Position)
                    .ThenBy(x => x.RatingPrimary)
                    .ThenBy(x => x.RatingSecondary)
                    .ToList();
    }

    public List<GameRoster> GetGameRostersByGameIdAndHomeTeam(int gameId, bool homeTeam)
    {
      var results = new List<GameRoster>();

      // TODO ...wire up homeTeam
      using (var context = new LO30Context())
      {
        var gameTeam = context.GameTeams.Where(x => x.GameId == gameId && x.HomeTeam == homeTeam).SingleOrDefault();
        results = context.GameRosters.Where(x => x.GameId == gameId && x.TeamId == gameTeam.TeamId).IncludeAll().ToList();

        /*
        results = context.GameRosters.Join(context.GameTeams,
                        gr => new { gr.GameId, gr.TeamId },
                        gt => new { gt.GameId, gt.TeamId },
                        (gr, gt) => new
                        {
                          Game = gr.Game,
                          GameId = gr.GameId,
                          GameRosterId = gr.GameRosterId,
                          Goalie = gr.Goalie,
                          Line = gr.Line,
                          Player = gr.Player,
                          PlayerId = gr.PlayerId,
                          PlayerNumber = gr.PlayerNumber,
                          Position = gr.Position,
                          RatingPrimary = gr.RatingPrimary,
                          RatingSecondary = gr.RatingSecondary,
                          Season = gr.Season,
                          SeasonId = gr.SeasonId,
                          Sub = gr.Sub,
                          SubbingForPlayer = gr.SubbingForPlayer,
                          SubbingForPlayerId = gr.SubbingForPlayerId,
                          Team = gr.Team,
                          TeamId = gr.TeamId,
                          HomeTeam = gt.HomeTeam
                        }).ToList();
         */
      }
      return results.OrderByDescending(x => x.GameId)
                    .ThenBy(x => x.Line)
                    .ThenByDescending(x => x.Position)
                    .ThenBy(x => x.RatingPrimary)
                    .ThenBy(x => x.RatingSecondary)
                    .ToList();
    }
  }
}
