using LO30.Data.Contexts;
using LO30.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Data.Services
{
  public class ScoreSheetEntryProcessor
  {
    DateTime _first = DateTime.Now;
    DateTime _last = DateTime.Now;
    TimeSpan _diffFromFirst = new TimeSpan();
    TimeSpan _diffFromLast = new TimeSpan();

    private LogService _logger;
    private LO30Context _context;
    private TimeService _timeService;

    public ScoreSheetEntryProcessor(LogService logger, LO30Context context)
    {
      _logger = logger;
      _context = context;
      _timeService = new TimeService();
    }


    private List<ScoreSheetEntryProcessedGoal> DeriveScoreSheetEntryProcessedGoals(List<ScoreSheetEntryGoal> scoreSheetEntryGoals, List<GameTeam> gameTeams, List<GameRoster> gameRosters)
    {
      var newData = new List<ScoreSheetEntryProcessedGoal>();

      foreach (var scoreSheetEntryGoal in scoreSheetEntryGoals)
      {
        var gameId = scoreSheetEntryGoal.GameId;

        // look up the home and away season team id
        // TODO..do this once per game, not per score sheet entry
        var homeGameTeam = gameTeams.Where(gt => gt.GameId == gameId && gt.HomeTeam == true).FirstOrDefault();
        var awayGameTeam = gameTeams.Where(gt => gt.GameId == gameId && gt.HomeTeam == false).FirstOrDefault();

        // lookup game rosters
        // TODO..do this once per game, not per score sheet entry
        var homeTeamRoster = gameRosters.Where(x => x.SeasonId == homeGameTeam.SeasonId && x.TeamId == homeGameTeam.TeamId && x.GameId == gameId).ToList();
        var awayTeamRoster = gameRosters.Where(x => x.SeasonId == awayGameTeam.SeasonId && x.TeamId == awayGameTeam.TeamId && x.GameId == gameId).ToList();

        var homeTeamFlag = scoreSheetEntryGoal.HomeTeam;
        var goalPlayerNumber = scoreSheetEntryGoal.Goal;
        var assist1PlayerNumber = scoreSheetEntryGoal.Assist1;
        var assist2PlayerNumber = scoreSheetEntryGoal.Assist2;
        var assist3PlayerNumber = scoreSheetEntryGoal.Assist3;

        var gameRosterToUse = awayTeamRoster;
        var gameTeamToUse = awayGameTeam;
        if (homeTeamFlag)
        {
          gameRosterToUse = homeTeamRoster;
          gameTeamToUse = homeGameTeam;
        }

        // lookup player ids
        var goalPlayerId = ConvertPlayerNumberIntoPlayer(gameRosterToUse, goalPlayerNumber);
        var assist1PlayerId = ConvertPlayerNumberIntoPlayer(gameRosterToUse, assist1PlayerNumber);
        var assist2PlayerId = ConvertPlayerNumberIntoPlayer(gameRosterToUse, assist2PlayerNumber);
        var assist3PlayerId = ConvertPlayerNumberIntoPlayer(gameRosterToUse, assist3PlayerNumber);

        // determine type goal
        // TODO improve this logic
        bool shortHandedGoal = scoreSheetEntryGoal.ShortHandedPowerPlay == "SH" ? true : false;
        bool powerPlayGoal = scoreSheetEntryGoal.ShortHandedPowerPlay == "PP" ? true : false;
        bool gameWinningGoal = false;

        newData.Add(new ScoreSheetEntryProcessedGoal(
                          ssegid: scoreSheetEntryGoal.ScoreSheetEntryGoalId,

                          sid: gameTeamToUse.SeasonId,
                          tid: gameTeamToUse.TeamId,
                          gid: scoreSheetEntryGoal.GameId,

                          per: scoreSheetEntryGoal.Period,
                          ht: scoreSheetEntryGoal.HomeTeam,
                          time: scoreSheetEntryGoal.TimeRemaining,

                          gpid: Convert.ToInt32(goalPlayerId),
                          a1pid: assist1PlayerId,
                          a2pid: assist2PlayerId,
                          a3pid: assist3PlayerId,

                          shg: shortHandedGoal,
                          ppg: powerPlayGoal,
                          gwg: gameWinningGoal,

                          upd: DateTime.Now
                  ));
      }

      return newData;
    }

    private List<ScoreSheetEntryProcessedPenalty> DeriveScoreSheetEntryProcessedPenalties(List<ScoreSheetEntryPenalty> scoreSheetEntryPenalties, List<GameTeam> gameTeams, List<GameRoster> gameRosters, List<Penalty> penalties)
    {
      var newData = new List<ScoreSheetEntryProcessedPenalty>();

      foreach (var scoreSheetEntryPenalty in scoreSheetEntryPenalties)
      {
        var gameId = scoreSheetEntryPenalty.GameId;

        // look up the home and away team id
        // TODO..do this once per game, not per score sheet entry
        var homeGameTeam = gameTeams.Where(gt => gt.GameId == gameId && gt.HomeTeam == true).FirstOrDefault();
        var awayGameTeam = gameTeams.Where(gt => gt.GameId == gameId && gt.HomeTeam == false).FirstOrDefault();

        // lookup game rosters
        var homeTeamRoster = gameRosters.Where(x => x.GameId == gameId && x.TeamId == homeGameTeam.TeamId).ToList();
        var awayTeamRoster = gameRosters.Where(x => x.GameId == gameId && x.TeamId == awayGameTeam.TeamId).ToList();

        var homeTeamFlag = scoreSheetEntryPenalty.HomeTeam;
        var playerNumber = scoreSheetEntryPenalty.Player;

        var gameRosterToUse = awayTeamRoster;
        var gameTeamToUse = awayGameTeam;
        if (homeTeamFlag)
        {
          gameRosterToUse = homeTeamRoster;
          gameTeamToUse = homeGameTeam;
        }

        // lookup player id
        var playerId = ConvertPlayerNumberIntoPlayer(gameRosterToUse, playerNumber.ToString());

        // lookup penalty
        var penaltyId = penalties.Where(x => x.PenaltyCode == scoreSheetEntryPenalty.PenaltyCode).FirstOrDefault().PenaltyId;

        newData.Add(new ScoreSheetEntryProcessedPenalty(
                          ssepid: scoreSheetEntryPenalty.ScoreSheetEntryPenaltyId,

                          sid: gameTeamToUse.SeasonId,
                          tid: gameTeamToUse.TeamId,
                          gid: scoreSheetEntryPenalty.GameId,

                          per: scoreSheetEntryPenalty.Period,
                          ht: scoreSheetEntryPenalty.HomeTeam,
                          time: scoreSheetEntryPenalty.TimeRemaining,

                          playid: Convert.ToInt32(playerId),
                          penid: penaltyId,
                          pim: scoreSheetEntryPenalty.PenaltyMinutes,

                          upd: DateTime.Now
                  ));
      }

      return newData;
    }

    private List<ScoreSheetEntryProcessedSub> DeriveScoreSheetEntryProcessedSubs(List<ScoreSheetEntrySub> scoreSheetEntrySubs, List<Game> games, List<GameTeam> gameTeams, List<TeamRoster> teamRosters, List<Player> players)
    {
      var newData = new List<ScoreSheetEntryProcessedSub>();

      foreach (var scoreSheetEntrySub in scoreSheetEntrySubs)
      {
        var gameId = scoreSheetEntrySub.GameId;

        var game = games.Where(x => x.GameId == gameId).FirstOrDefault();
        var gameDateYYYYMMDD = _timeService.ConvertDateTimeIntoYYYYMMDD(game.GameDateTime, ifNullReturnMax: false);

        // look up the home and away season team id
        // TODO..do this once per game, not per score sheet entry
        var homeGameTeam = gameTeams.Where(gt => gt.GameId == gameId && gt.HomeTeam == true).FirstOrDefault();
        var awayGameTeam = gameTeams.Where(gt => gt.GameId == gameId && gt.HomeTeam == false).FirstOrDefault();

        // lookup team rosters
        var homeTeamRoster = teamRosters.Where(x => x.SeasonId == homeGameTeam.SeasonId && x.TeamId == homeGameTeam.TeamId && x.StartYYYYMMDD <= gameDateYYYYMMDD && x.EndYYYYMMDD >= gameDateYYYYMMDD).ToList();
        var awayTeamRoster = teamRosters.Where(x => x.SeasonId == awayGameTeam.SeasonId && x.TeamId == awayGameTeam.TeamId && x.StartYYYYMMDD <= gameDateYYYYMMDD && x.EndYYYYMMDD >= gameDateYYYYMMDD).ToList();

        var homeTeamFlag = scoreSheetEntrySub.HomeTeam;
        var jerseyNumber = scoreSheetEntrySub.JerseyNumber;
        var subPlayerId = scoreSheetEntrySub.SubPlayerId;
        var subbingForPlayerId = scoreSheetEntrySub.SubbingForPlayerId;

        var teamRosterToUse = awayTeamRoster;
        var gameTeamToUse = awayGameTeam;
        if (homeTeamFlag)
        {
          teamRosterToUse = homeTeamRoster;
          gameTeamToUse = homeGameTeam;
        }

        // lookup player ids

        // make sure the subbing for is on the team roster
        var foundSubbingForPlayer = teamRosterToUse.Where(x => x.PlayerId == subbingForPlayerId).FirstOrDefault();
        var foundSubPlayer = players.Where(x => x.PlayerId == subPlayerId).FirstOrDefault();

        if (foundSubbingForPlayer == null || foundSubPlayer == null)
        {
          // todo handle bad data
        }
        else
        {
          newData.Add(new ScoreSheetEntryProcessedSub(
                  ssesid: scoreSheetEntrySub.ScoreSheetEntrySubId,

                  sid: gameTeamToUse.SeasonId,
                  tid: gameTeamToUse.TeamId,
                  gid: gameId,
                  ht: homeTeamFlag,

                  spid: subPlayerId,
                  sfpid: subbingForPlayerId,
                  jer: jerseyNumber,

                  upd: DateTime.Now
          ));
        }
      }

      return newData;
    }

    public List<ScoreSheetEntryEvent> DeriveScoreSheetEntryEvents(List<ScoreSheetEntryProcessedGoal> scoreSheetEntryGoals, List<ScoreSheetEntryProcessedPenalty> scoreSheetEntryPenalties)
    {
      // put all the goals and penalties in order
      List<ScoreSheetEntryEvent> sseEvents = new List<ScoreSheetEntryEvent>();

      // add all the goals to the events
      foreach (var sseGoal in scoreSheetEntryGoals)
      {
        sseEvents.Add(new ScoreSheetEntryEvent()
        {
          GameId = sseGoal.GameId,
          TimeElapsed = sseGoal.TimeElapsed,
          EventType = ScoreSheetEntryEventType.Goal,
          ScoreSheetEntryGoalId = sseGoal.ScoreSheetEntryGoalId,
          ScoreSheetEntryPenaltyId = null,
          HomeTeam = sseGoal.HomeTeam,
          MatchPenalty = false
        }
        );
      }

      // add all the penalties to the events
      foreach (var ssePenalty in scoreSheetEntryPenalties)
      {
        sseEvents.Add(new ScoreSheetEntryEvent()
        {
          GameId = ssePenalty.GameId,
          TimeElapsed = ssePenalty.TimeElapsed,
          EventType = ScoreSheetEntryEventType.PenaltyStart,
          ScoreSheetEntryGoalId = null,
          ScoreSheetEntryPenaltyId = ssePenalty.ScoreSheetEntryPenaltyId,
          HomeTeam = ssePenalty.HomeTeam,
          MatchPenalty = ssePenalty.PenaltyMinutes == 5 ? true : false
        }
        );

        sseEvents.Add(new ScoreSheetEntryEvent()
        {
          GameId = ssePenalty.GameId,
          TimeElapsed = ssePenalty.TimeElapsed.Add(new TimeSpan(0, ssePenalty.PenaltyMinutes, 0)),
          EventType = ScoreSheetEntryEventType.PenaltyEnd,
          ScoreSheetEntryGoalId = null,
          ScoreSheetEntryPenaltyId = ssePenalty.ScoreSheetEntryPenaltyId,
          HomeTeam = ssePenalty.HomeTeam,
          MatchPenalty = ssePenalty.PenaltyMinutes == 5 ? true : false
        }
        );
      }

      // VERY IMPORTANT, order the events by time elapsed then event type
      sseEvents = sseEvents.OrderBy(x => x.TimeElapsed).ThenBy(x => x.EventType).ToList();

      return sseEvents;
    }

    public List<ScoreSheetEntryGoalType> ProcessOneGameScoreSheetEntryEvents(List<ScoreSheetEntryEvent> scoreSheetEntryEvents)
    {
      List<ScoreSheetEntryGoalType> results = new List<ScoreSheetEntryGoalType>();

      List<ScoreSheetEntryEvent> homeTeamPenaltyBox = new List<ScoreSheetEntryEvent>();
      List<ScoreSheetEntryEvent> awayTeamPenaltyBox = new List<ScoreSheetEntryEvent>();
      ScoreSheetEntryEvent leavesPenaltyBox;
      ScoreSheetEntryEvent eventToRemove;

      // loop through the events and see if any goals where scored during the penalties
      while (scoreSheetEntryEvents.Count > 0)
      {
        var sseEvent = scoreSheetEntryEvents[0];

        var penaltyBoxToUse = awayTeamPenaltyBox;
        if (sseEvent.HomeTeam)
        {
          penaltyBoxToUse = homeTeamPenaltyBox;
        }

        switch (sseEvent.EventType)
        {
          case ScoreSheetEntryEventType.PenaltyEnd:
            // the penalty ended before any goal was scored...
            // remove person from penalty box

            leavesPenaltyBox = penaltyBoxToUse.Single(x => x.ScoreSheetEntryPenaltyId == sseEvent.ScoreSheetEntryPenaltyId);
            penaltyBoxToUse.Remove(leavesPenaltyBox);

            // remove event since it has been handled
            eventToRemove = scoreSheetEntryEvents.Single(x => x.ScoreSheetEntryPenaltyId == sseEvent.ScoreSheetEntryPenaltyId && x.EventType == sseEvent.EventType);
            scoreSheetEntryEvents.Remove(eventToRemove);
            break;
          case ScoreSheetEntryEventType.PenaltyStart:
            // a penalty occurred, add person to penalty box

            penaltyBoxToUse.Add(sseEvent);

            // remove event since it has been handled
            eventToRemove = scoreSheetEntryEvents.Single(x => x.ScoreSheetEntryPenaltyId == sseEvent.ScoreSheetEntryPenaltyId && x.EventType == sseEvent.EventType);
            scoreSheetEntryEvents.Remove(eventToRemove);
            break;
          case ScoreSheetEntryEventType.Goal:
            // if no one in the penalty box, then its just a regular goal
            if (homeTeamPenaltyBox.Count == 0 && awayTeamPenaltyBox.Count == 0)
            {
              // if no one in the penalty box, then its just a regular goal
              // do nothing
            }
            else if (homeTeamPenaltyBox.Count == awayTeamPenaltyBox.Count)
            {
              // there are the same number of people in the penalty box
              // so the goal was even strength
              // do nothing
            }
            else if (homeTeamPenaltyBox.Count < awayTeamPenaltyBox.Count)
            {
              // the home team is on the power play
              if (sseEvent.HomeTeam)
              {
                // home team scored...on the power play
                results.Add(new ScoreSheetEntryGoalType()
                {
                  ScoreSheetEntryGoalId = Convert.ToInt32(sseEvent.ScoreSheetEntryGoalId),
                  PowerPlayGoal = true,
                  ShortHandedGoal = false,
                  GameWinningGoal = false
                });

                //release person from away team penalty box (but not if match penalty)
                leavesPenaltyBox = awayTeamPenaltyBox.Where(x => x.MatchPenalty == false).OrderBy(x => x.TimeElapsed).FirstOrDefault();
                if (leavesPenaltyBox != null)
                {
                  awayTeamPenaltyBox.Remove(leavesPenaltyBox);

                  // also remove the penalty end event
                  eventToRemove = scoreSheetEntryEvents.Single(x => x.ScoreSheetEntryPenaltyId == leavesPenaltyBox.ScoreSheetEntryPenaltyId && x.EventType == ScoreSheetEntryEventType.PenaltyEnd);
                  scoreSheetEntryEvents.Remove(eventToRemove);
                }
              }
              else
              {
                // away team scored...shorthanded
                results.Add(new ScoreSheetEntryGoalType()
                {
                  ScoreSheetEntryGoalId = Convert.ToInt32(sseEvent.ScoreSheetEntryGoalId),
                  PowerPlayGoal = false,
                  ShortHandedGoal = true,
                  GameWinningGoal = false
                });
              }
            }
            else
            {
              // the away team is on the power play
              if (sseEvent.HomeTeam)
              {
                // home team scored...shorthanded
                results.Add(new ScoreSheetEntryGoalType()
                {
                  ScoreSheetEntryGoalId = Convert.ToInt32(sseEvent.ScoreSheetEntryGoalId),
                  PowerPlayGoal = false,
                  ShortHandedGoal = true,
                  GameWinningGoal = false
                });
              }
              else
              {
                // away team scored...on the power play
                results.Add(new ScoreSheetEntryGoalType()
                {
                  ScoreSheetEntryGoalId = Convert.ToInt32(sseEvent.ScoreSheetEntryGoalId),
                  PowerPlayGoal = true,
                  ShortHandedGoal = false,
                  GameWinningGoal = false
                });

                //release person from home team penalty box (but not if match penalty)
                leavesPenaltyBox = homeTeamPenaltyBox.Where(x => x.MatchPenalty == false).OrderBy(x => x.TimeElapsed).FirstOrDefault();
                if (leavesPenaltyBox != null)
                {
                  homeTeamPenaltyBox.Remove(leavesPenaltyBox);

                  // also remove the penalty end event
                  eventToRemove = scoreSheetEntryEvents.Single(x => x.ScoreSheetEntryPenaltyId == leavesPenaltyBox.ScoreSheetEntryPenaltyId && x.EventType == ScoreSheetEntryEventType.PenaltyEnd);
                  scoreSheetEntryEvents.Remove(eventToRemove);
                }
              }
            }

            // remove event since it has been handled
            eventToRemove = scoreSheetEntryEvents.Single(x => x.ScoreSheetEntryGoalId == sseEvent.ScoreSheetEntryGoalId && x.EventType == sseEvent.EventType);
            scoreSheetEntryEvents.Remove(eventToRemove);
            break;
          default:
            throw new NotImplementedException("The EventType (" + sseEvent.EventType.ToString() + ") is not implemented");
        }
      }

      return results;
    }

    public int? ConvertPlayerNumberIntoPlayer(ICollection<GameRoster> gameRoster, string playerNumber)
    {
      int? playerId = null;

      if (playerNumber != null)
      {
        var gameRosterMatch = gameRoster.Where(x => x.PlayerNumber == playerNumber).FirstOrDefault();
        if (gameRosterMatch == null)
        {
          playerId = 0; // the unknown player
        }
        else
        {
          playerId = gameRosterMatch.PlayerId;
        }
      }

      return playerId;
    }

    public List<ScoreSheetEntryProcessedGoal> UpdateScoreSheetEntryProcessedGoalsWithPPAndSH(List<ScoreSheetEntryProcessedGoal> scoreSheetEntryGoals, List<ScoreSheetEntryProcessedPenalty> scoreSheetEntryPenalties)
    {
      List<ScoreSheetEntryGoalType> scoreSheetEntryGoalTypes = new List<ScoreSheetEntryGoalType>();

      // put all the goals and penalties in order
      List<ScoreSheetEntryEvent> scoreSheetEntryEvents = DeriveScoreSheetEntryEvents(scoreSheetEntryGoals, scoreSheetEntryPenalties);

      int currentGameId = -1;
      int previosGameId = -1;
      List<int> penaltiesProcessing = new List<int>();

      // loop through the events and see if any goals where scored during the penalties
      foreach (var sseEvent in scoreSheetEntryEvents)
      {
        currentGameId = sseEvent.GameId;

        if (currentGameId == previosGameId)
        {
          // this gameid has already been processed; do nothing
        }
        else
        {
          previosGameId = currentGameId;

          var allCurrentGameEvents = scoreSheetEntryEvents.Where(x => x.GameId == currentGameId).OrderBy(x => x.TimeElapsed).ToList();
          var results = ProcessOneGameScoreSheetEntryEvents(allCurrentGameEvents);
          scoreSheetEntryGoalTypes.AddRange(results);
        }
      }


      // loop through goal type results and update score sheet entries
      foreach (var goalTypeResult in scoreSheetEntryGoalTypes)
      {
        var scoreSheetEntry = scoreSheetEntryGoals.Single(x => x.ScoreSheetEntryGoalId == goalTypeResult.ScoreSheetEntryGoalId);
        scoreSheetEntry.PowerPlayGoal = goalTypeResult.PowerPlayGoal;
        scoreSheetEntry.ShortHandedGoal = goalTypeResult.ShortHandedGoal;
      }

      // loop through the penalties and see if any goals where scored during them


      // NOTE THIS WAS COMMENTED OUT
      foreach (var scoreSheetEntryPenalty in scoreSheetEntryPenalties)
      {
        var gameId = scoreSheetEntryPenalty.GameId;
        var penaltyHomeTeam = scoreSheetEntryPenalty.HomeTeam;

        //get start/end time of the penalty
        var start = scoreSheetEntryPenalty.TimeElapsed;
        var end = start.Add(new TimeSpan(0, scoreSheetEntryPenalty.PenaltyMinutes, 0));
        var major = scoreSheetEntryPenalty.PenaltyMinutes == 5 ? true : false;

        //check if there were any goals during the penalty
        var goalsDuringPenalty = scoreSheetEntryGoals.Where(x => x.GameId == gameId && x.TimeElapsed >= start && x.TimeElapsed <= end).OrderBy(x => x.TimeElapsed).ToList();

        //now see if they were power play or shorthanded
        foreach (var goalDuringPenalty in goalsDuringPenalty)
        {
          // if it was a power play goal (hometeam != penalty hometeam)
          // and it was not a major penalty
          // then the power play is over, no need to continue processing
          if (goalDuringPenalty.HomeTeam != penaltyHomeTeam)
          {
            goalDuringPenalty.PowerPlayGoal = true;

            if (!major)
            {
              // then the power play is over, no need to continue processing
              break;
            }
          }
          else
          {
            // it was a shorthanded goal
            goalDuringPenalty.ShortHandedGoal = true;
          }
        }
      }

      return scoreSheetEntryGoals;
    }

    public ProcessingResult ProcessScoreSheetEntryPenalties(int startingGameId, int endingGameId)
    {
      var result = new ProcessingResult();

      DateTime last = DateTime.Now;
      TimeSpan diffFromLast = new TimeSpan();

      try
      {
        var scoreSheetEntryPenalties = _context.ScoreSheetEntryPenalties.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();
        var gameRosters = _context.GameRosters
                                .Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId)
                                .ToList();
        var gameTeams = _context.GameTeams
                        .Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId)
                        .ToList();
        var penalties = _context.Penalties.ToList();

        _logger.Write("ProcessScoreSheetEntryPenalties: scoreSheetEntryPenalties.Count: " + scoreSheetEntryPenalties.Count);

        var scoreSheetEntryPenaltiesProcessed = DeriveScoreSheetEntryProcessedPenalties(scoreSheetEntryPenalties, gameTeams, gameRosters, penalties);

        _context.ScoreSheetEntryProcessedPenalties.AddRange(scoreSheetEntryPenaltiesProcessed);
        var modified = _context.SaveChanges();

        _logger.Write("ProcessScoreSheetEntryPenalties: SaveOrUpdateScoreSheetEntryPenaltyProcessed: " + modified);

        // AUDIT ScoreSheetEntries and ScoreSheetEntriesProcessed should have same ids 
        var inputs = _context.ScoreSheetEntryPenalties.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();
        var outputs = _context.ScoreSheetEntryProcessedPenalties.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();

        if (inputs.Count != outputs.Count)
        {
          result.error = "Error processing ScoreSheetEntryPenalties. The ScoreSheetEntryPenalties count (" + inputs.Count + ") does not match ScoreSheetEntryPenaltiesProcessed count (" + outputs.Count + ")";
        }
        else
        {
          foreach (var input in inputs)
          {
            var output = outputs.Where(x => x.ScoreSheetEntryPenaltyId == input.ScoreSheetEntryPenaltyId).FirstOrDefault();

            if (output == null)
            {
              result.error = "Error processing ScoreSheetEntryPenalties. The ScoreSheetEntryPenaltyId (" + input.ScoreSheetEntryPenaltyId + ") is missing from ScoreSheetEntryPenaltiesProcessed";
            }
          }
        }
      }
      catch (Exception ex)
      {
        result.modified = -2;
        result.error = ex.Message;

        _logger.Write(ex);
        //ErrorHandlingService.PrintFullErrorMessage(ex);
      }

      diffFromLast = DateTime.Now - last;
      result.time = diffFromLast.ToString();
      return result;
    }

    public ProcessingResult ProcessScoreSheetEntryGoals(int startingGameId, int endingGameId)
    {
      var result = new ProcessingResult();

      DateTime last = DateTime.Now;
      TimeSpan diffFromLast = new TimeSpan();

      try
      {
        var scoreSheetEntryGoals = _context.ScoreSheetEntryGoals.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();
        var gameTeams = _context.GameTeams.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();
        var gameRosters = _context.GameRosters
                                .Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId)
                                .ToList();

        result.toProcess = scoreSheetEntryGoals.Count;

        var scoreSheetEntryProcessedGoals = DeriveScoreSheetEntryProcessedGoals(scoreSheetEntryGoals, gameTeams, gameRosters);

        _context.ScoreSheetEntryProcessedGoals.AddRange(scoreSheetEntryProcessedGoals);
        result.modified = _context.SaveChanges();

        // AUDIT ScoreSheetEntries and ScoreSheetEntriesProcessed should have same ids 
        var inputs = _context.ScoreSheetEntryGoals.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();
        var outputs = _context.ScoreSheetEntryProcessedGoals.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();

        if (inputs.Count != outputs.Count)
        {
          result.error = "Error processing ScoreSheetEntryGoals. The ScoreSheetEntryGoals count (" + inputs.Count + ") does not match ScoreSheetEntryProcessedGoals count (" + outputs.Count + ")";
        }
        else
        {
          foreach (var input in inputs)
          {
            var output = outputs.Where(x => x.ScoreSheetEntryGoalId == input.ScoreSheetEntryGoalId).FirstOrDefault();

            if (output == null)
            {
              result.error = "Error processing ScoreSheetEntryGoals. The ScoreSheetEntryGoalId (" + input.ScoreSheetEntryGoalId + ") is missing from ScoreSheetEntryProcessedGoals";
            }
          }
        }
      }
      catch (Exception ex)
      {
        result.modified = -2;
        result.error = ex.Message;

        _logger.Write(ex);
        //ErrorHandlingService.PrintFullErrorMessage(ex);
      }

      diffFromLast = DateTime.Now - last;
      result.time = diffFromLast.ToString();
      return result;
    }

    public ProcessingResult UpdateScoreSheetEntriesWithPPAndSH(int startingGameId, int endingGameId)
    {
      var result = new ProcessingResult();

      DateTime last = DateTime.Now;
      TimeSpan diffFromLast = new TimeSpan();

      try
      {
        var scoreSheetEntryGoals = _context.ScoreSheetEntryProcessedGoals.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();
        var scoreSheetEntryPenalties = _context.ScoreSheetEntryProcessedPenalties.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();

        result.toProcess = scoreSheetEntryGoals.Count;

        var scoreSheetEntryProcessedGoals = UpdateScoreSheetEntryProcessedGoalsWithPPAndSH(scoreSheetEntryGoals, scoreSheetEntryPenalties);

        _context.ScoreSheetEntryProcessedGoals.AddRange(scoreSheetEntryProcessedGoals);
        result.modified = _context.SaveChanges();
      }
      catch (Exception ex)
      {
        result.modified = -2;
        result.error = ex.Message;

        _logger.Write(ex);
        //ErrorHandlingService.PrintFullErrorMessage(ex);
      }

      diffFromLast = DateTime.Now - last;
      result.time = diffFromLast.ToString();
      return result;
    }

    public ProcessingResult ProcessScoreSheetEntrySubs(int startingGameId, int endingGameId)
    {
      var result = new ProcessingResult();

      DateTime last = DateTime.Now;
      TimeSpan diffFromLast = new TimeSpan();

      try
      {
        var scoreSheetEntrySubs = _context.ScoreSheetEntrySubs.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();
        var players = _context.Players.ToList();
        var games = _context.Games.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();
        var gameTeams = _context.GameTeams.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();
        var teamRosters = _context.TeamRosters.ToList();

        result.toProcess = scoreSheetEntrySubs.Count;

        var scoreSheetEntrySubsProcessed = DeriveScoreSheetEntryProcessedSubs(scoreSheetEntrySubs, games, gameTeams, teamRosters, players);

        _context.ScoreSheetEntryProcessedSubs.AddRange(scoreSheetEntrySubsProcessed);
        result.modified = _context.SaveChanges();

        // AUDIT ScoreSheetEntrySubs and ScoreSheetEntrySubsProcessed should have same ids 
        var inputs = _context.ScoreSheetEntrySubs.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();
        var outputs = _context.ScoreSheetEntryProcessedSubs.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();

        if (inputs.Count != outputs.Count)
        {
          result.error = "Error processing ScoreSheetEntrySubs. The ScoreSheetEntrySubs count (" + inputs.Count + ") does not match ScoreSheetEntryProcessedSubs count (" + outputs.Count + ")";
        }
        else
        {
          foreach (var input in inputs)
          {
            var output = outputs.Where(x => x.ScoreSheetEntrySubId == input.ScoreSheetEntrySubId).FirstOrDefault();

            if (output == null)
            {
              result.error = "Error processing ScoreSheetEntrySubs. The ScoreSheetEntrySubId (" + input.ScoreSheetEntrySubId + ") is missing from ScoreSheetEntryProcessedSubs";
            }
          }
        }
      }
      catch (Exception ex)
      {
        result.modified = -2;
        result.error = ex.Message;

        _logger.Write(ex);
        //ErrorHandlingService.PrintFullErrorMessage(ex);
      }

      diffFromLast = DateTime.Now - last;
      result.time = diffFromLast.ToString();
      return result;
    }

    public ProcessingResult ProcessScoreSheetEntriesIntoGameResults(int startingGameId, int endingGameId)
    {
      var result = new ProcessingResult();

      DateTime last = DateTime.Now;
      TimeSpan diffFromLast = new TimeSpan();

      try
      {
        // get list of game entries for these games (use game just in case there was no score sheet entries...0-0 game with no penalty minutes)
        var games = _context.Games.Where(s => s.GameId >= startingGameId && s.GameId <= endingGameId).ToList();

        result.toProcess = games.Count;

        // get a list of periods
        var periods = new int[] { 1, 2, 3, 4 };

        var modifiedCount = 0;

        // loop through each game
        for (var g = 0; g < games.Count; g++)
        {
          var gameId = games[g].GameId;
          int scoreHomeTeamTotal = 0;
          int scoreAwayTeamTotal = 0;
          int penaltyHomeTeamTotal = 0;
          int penaltyAwayTeamTotal = 0;

          // look up the home and away team id
          var homeGameTeam = _context.GameTeams.Where(x => x.GameId == gameId && x.HomeTeam == true).FirstOrDefault();
          var awayGameTeam = _context.GameTeams.Where(x => x.GameId == gameId && x.HomeTeam == false).FirstOrDefault();
          
          if (homeGameTeam == null || awayGameTeam == null)
          {
            throw new ArgumentNullException("homeGameTeam and/or awayGameTeam cannot be null");
          }

          #region loop through each period
          for (var p = 0; p < periods.Length; p++)
          {
            var period = periods[p];
            var scoreHomeTeamPeriod = 0;
            var scoreAwayTeamPeriod = 0;

            #region process all score sheet entries for this specific game/period
            var scoreSheetEntries = _context.ScoreSheetEntryProcessedGoals.Where(s => s.GameId == gameId && s.Period == period).ToList();

            for (var s = 0; s < scoreSheetEntries.Count; s++)
            {
              var scoreSheetEntry = scoreSheetEntries[s];

              if (scoreSheetEntry.HomeTeam)
              {
                scoreHomeTeamPeriod++;
                scoreHomeTeamTotal++;
              }
              else
              {
                scoreAwayTeamPeriod++;
                scoreAwayTeamTotal++;
              }
            }
            #endregion

            #region create and save (or update) the home and away teams GameScore by period
            var homeGameScore = new GameScore(sid: homeGameTeam.SeasonId, tid: homeGameTeam.TeamId, gid: gameId, per: period, score: scoreHomeTeamPeriod);
            _context.GameScores.Add(homeGameScore);

            var awayGameScore = new GameScore(sid: awayGameTeam.SeasonId, tid: awayGameTeam.TeamId, gid: gameId, per: period, score: scoreAwayTeamPeriod);
            _context.GameScores.Add(awayGameScore);
            #endregion

            #region process all score sheet entry penalties for this specific game/period
            var scoreSheetEntryPenalties = _context.ScoreSheetEntryPenalties.Where(s => s.GameId == gameId && s.Period == period).ToList();

            for (var s = 0; s < scoreSheetEntryPenalties.Count; s++)
            {
              var scoreSheetEntryPenalty = scoreSheetEntryPenalties[s];

              if (scoreSheetEntryPenalty.HomeTeam)
              {
                penaltyHomeTeamTotal = penaltyHomeTeamTotal + scoreSheetEntryPenalty.PenaltyMinutes;
              }
              else
              {
                penaltyAwayTeamTotal = penaltyAwayTeamTotal + scoreSheetEntryPenalty.PenaltyMinutes;
              }
            }
            #endregion
          }
          #endregion

          #region create and save (or update) the home and away teams GameScore for game
          var finalPeriod = 0;
          var homeFinalGameScore = new GameScore(sid: homeGameTeam.SeasonId, tid: homeGameTeam.TeamId, gid: gameId, per: finalPeriod, score: scoreHomeTeamTotal);
          _context.GameScores.Add(homeFinalGameScore);

          var awayFinalGameScore = new GameScore(sid: awayGameTeam.SeasonId, tid: awayGameTeam.TeamId, gid: gameId, per: finalPeriod, score: scoreAwayTeamTotal);
          _context.GameScores.Add(awayFinalGameScore);
          #endregion

          #region create and save (or update) the home and away teams GameOutcome for game
          // save game results for the game
          string homeResult = "T";
          string awayResult = "T";
          if (scoreHomeTeamTotal > scoreAwayTeamTotal)
          {
            homeResult = "W";
            awayResult = "L";
          }
          else if (scoreHomeTeamTotal < scoreAwayTeamTotal)
          {
            homeResult = "L";
            awayResult = "W";
          }

          var gameRosters = _context.GameRosters.Where(x=>x.GameId == gameId).ToList();

          if (gameRosters == null)
          {
            throw new ArgumentNullException("gameRosters");
          }

          var gameSubCounts = gameRosters
              .GroupBy(x => new { x.TeamId })
              .Select(grp => new
              {
                TeamId = grp.Key.TeamId,

                Subs = grp.Sum(x => Convert.ToInt32(x.Sub))
              })
              .ToList();

          var homeSubCount = gameSubCounts.Where(x => x.TeamId == homeGameTeam.TeamId).FirstOrDefault().Subs;
          var awaySubCount = gameSubCounts.Where(x => x.TeamId == awayGameTeam.TeamId).FirstOrDefault().Subs;

          var homeGameOutcome = new GameOutcome(
                                        sid: homeGameTeam.SeasonId,
                                        tid: homeGameTeam.TeamId,
                                        gid: gameId,
                                        res: homeResult,
                                        gf: scoreHomeTeamTotal,
                                        ga: scoreAwayTeamTotal,
                                        pim: penaltyHomeTeamTotal,
                                        over: false,
                                        otid: awayGameTeam.TeamId,
                                        subs: homeSubCount
                                        );

          var awayGameOutcome = new GameOutcome(
                                        sid: awayGameTeam.SeasonId,
                                        tid: awayGameTeam.TeamId,
                                        gid: gameId,
                                        res: awayResult,
                                        gf: scoreAwayTeamTotal,
                                        ga: scoreHomeTeamTotal,
                                        pim: penaltyAwayTeamTotal,
                                        over: false,
                                        otid: homeGameTeam.TeamId,
                                        subs: awaySubCount
                                        );

          _context.GameOutcomes.Add(homeGameOutcome);
          _context.GameOutcomes.Add(awayGameOutcome);
          modifiedCount += _context.SaveChanges();
          #endregion
        }

        _logger.Write("ProcessScoreSheetEntriesIntoGameResults: savedGameOutcomes:" + modifiedCount);

        result.modified = modifiedCount;
      }
      catch (Exception ex)
      {
        result.modified = -2;
        result.error = ex.Message;

        _logger.Write(ex);
      }

      diffFromLast = DateTime.Now - last;
      result.time = diffFromLast.ToString();
      return result;
    }
  }
}
