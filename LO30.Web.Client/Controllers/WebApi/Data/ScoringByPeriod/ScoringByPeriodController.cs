using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;
using LO30.Data.Objects;

namespace LO30.Controllers.Data.Games
{
  public class ScoringByPeriodController : ApiController
  {
    public ScoringByPeriodController()
    {
    }

    public List<ScoringByPeriod> GetScoringByPeriodByGame(int gameId)
    {
      var results = new List<ScoringByPeriod>();
      using (var context = new LO30Context())
      {
        var game = context.Games
                          .Where(x => x.GameId == gameId)
                          .IncludeAll()
                          .FirstOrDefault();

        var gameTeamHome = context.GameTeams
                                    .Where(x => x.GameId == gameId && x.HomeTeam == true)
                                    .IncludeAll()
                                    .FirstOrDefault();

        var gameTeamAway = context.GameTeams
                                    .Where(x => x.GameId == gameId && x.HomeTeam == false)
                                    .IncludeAll()
                                    .FirstOrDefault();

        var gameOutcomeHome = context.GameOutcomes
                                    .Where(x => x.GameId == gameId && x.HomeTeam == true)
                                    .IncludeAll()
                                    .FirstOrDefault();

        var gameOutcomeAway = context.GameOutcomes
                                    .Where(x => x.GameId == gameId && x.HomeTeam == false)
                                    .IncludeAll()
                                    .FirstOrDefault();

        var gameScoresHome = context.GameScores
                                    .Where(x => x.GameId == gameId && x.TeamId == gameTeamHome.TeamId)
                                    .IncludeAll()
                                    .ToList();

        var gameScoresAway = context.GameScores
                                    .Where(x => x.GameId == gameId && x.TeamId == gameTeamAway.TeamId)
                                    .IncludeAll()
                                    .ToList();

        var scoringByPeriodHome = new ScoringByPeriod();
        var scoringByPeriodAway = new ScoringByPeriod();

        scoringByPeriodHome.GameId = game.GameId;
        scoringByPeriodHome.GameDateTime = game.GameDateTime;
        scoringByPeriodHome.TeamId = gameTeamHome.TeamId;
        scoringByPeriodHome.TeamCode = gameTeamHome.Team.TeamCode;
        scoringByPeriodHome.TeamNameShort = gameTeamHome.Team.TeamNameShort;
        scoringByPeriodHome.TeamNameLong = gameTeamHome.Team.TeamNameLong;
        scoringByPeriodHome.HomeTeam = gameTeamHome.HomeTeam;
        scoringByPeriodHome.Outcome = gameOutcomeHome.Outcome;
        scoringByPeriodHome.Period1 = gameScoresHome.Where(x => x.Period == 1).Single().Score;
        scoringByPeriodHome.Period2 = gameScoresHome.Where(x => x.Period == 2).Single().Score;
        scoringByPeriodHome.Period3 = gameScoresHome.Where(x => x.Period == 3).Single().Score;
        var scoringByPeriodHomeOt = gameScoresHome.Where(x => x.Period == 4).SingleOrDefault();
        var scoringByPeriodHomeOtScore = 0;
        if (scoringByPeriodHomeOt != null)
        {
          scoringByPeriodHomeOtScore = scoringByPeriodHomeOt.Score;
        }

        scoringByPeriodHome.Period4 = scoringByPeriodHomeOtScore;
        scoringByPeriodHome.Final = gameOutcomeHome.GoalsFor;


        scoringByPeriodAway.GameId = game.GameId;
        scoringByPeriodAway.GameDateTime = game.GameDateTime;
        scoringByPeriodAway.TeamId = gameTeamAway.TeamId;
        scoringByPeriodAway.TeamCode = gameTeamAway.Team.TeamCode;
        scoringByPeriodAway.TeamNameShort = gameTeamAway.Team.TeamNameShort;
        scoringByPeriodAway.TeamNameLong = gameTeamAway.Team.TeamNameLong;
        scoringByPeriodAway.HomeTeam = gameTeamAway.HomeTeam;
        scoringByPeriodAway.Outcome = gameOutcomeAway.Outcome;
        scoringByPeriodAway.Period1 = gameScoresAway.Where(x => x.Period == 1).Single().Score;
        scoringByPeriodAway.Period2 = gameScoresAway.Where(x => x.Period == 2).Single().Score;
        scoringByPeriodAway.Period3 = gameScoresAway.Where(x => x.Period == 3).Single().Score;
        var scoringByPeriodAwayOt = gameScoresAway.Where(x => x.Period == 4).SingleOrDefault();
        var scoringByPeriodAwayOtScore = 0;
        if (scoringByPeriodAwayOt != null)
        {
          scoringByPeriodAwayOtScore = scoringByPeriodAwayOt.Score;
        }

        scoringByPeriodAway.Period4 = scoringByPeriodHomeOtScore;
        scoringByPeriodAway.Final = gameOutcomeAway.GoalsFor;

        results.Add(scoringByPeriodHome);
        results.Add(scoringByPeriodAway);
      }

      return results;

    }
  }
}
