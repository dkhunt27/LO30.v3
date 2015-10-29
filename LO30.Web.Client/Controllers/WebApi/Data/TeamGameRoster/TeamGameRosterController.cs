using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;
using LO30.Data.Objects;
using System;

namespace LO30.Controllers.Data.Games
{
  public class TeamGameRosterController : ApiController
  {
    public TeamGameRosterController()
    {
    }

    public List<TeamGameRoster> GetTeamGameRosterByGameIdAndHomeTeam(int gameId, bool homeTeam)
    {
      var results = new List<TeamGameRoster>();
      using (var context = new LO30Context())
      {
        var gameTeam = context.GameTeams
                                    .Where(x => x.GameId == gameId && x.HomeTeam == homeTeam)
                                    .IncludeAll()
                                    .FirstOrDefault();

        var gameRosterItems = context.GameRosters
                                    .Where(x => x.GameId == gameId && x.TeamId == gameTeam.TeamId)
                                    .IncludeAll()
                                    .ToList();

        var teamRosterItems = context.TeamRosters
                                    .Where(x => x.TeamId == gameTeam.TeamId)
                                    .IncludeAll()
                                    .ToList();

        var scoreSheetEntryProcessedGame = context.ScoreSheetEntryProcessedGames
                                                  .Where(x => x.GameId == gameId)
                                                  .SingleOrDefault();


        // loop through each team roster and add them to the teamGameRoster
        // and then match to the game roster (if exists) and determine who is in/out and who subbed for who

        foreach (var teamRosterItem in teamRosterItems) {
          var teamGameRoster = new TeamGameRoster();

          // set the teamRoster
          teamGameRoster.Rostered = teamRosterItem;
          teamGameRoster.RosteredPlayed = true;             //default/assume player played...will play


          //first determine if the game has played 
          teamGameRoster.GameProcessed = true;                // assume it was played

          if (scoreSheetEntryProcessedGame == null)
          {
            teamGameRoster.GameProcessed = false;
          }

          if (teamGameRoster.GameProcessed)
          {
            // only if the game was processed, check to see if they played and/or was subbed for.

            //first determine if the rostered player was in/out
            var rosteredIn = gameRosterItems.Where(x => x.PlayerId == teamRosterItem.PlayerId).SingleOrDefault();

            if (rosteredIn == null)
            {
              teamGameRoster.RosteredPlayed = false;
            }

            //next see if rostered player was also subbed for 
            //  FYI RosteredPlayed and RosteredWasSubbedFor are not mutually exclusive...they can be:
            //  (false/true) rostered player didn't play and there is a sub for him
            //  (false/false) rostered player didn't play and there was no sub for him
            //  (true/true) rostered player played but was late, got hurt, had to leave early, etc and someone subbed for him
            //  (true/false) rostered player played no one subbed for him


            // subbed is an array because it is possible that the sub didn't play the entire game, maybe he was covering for late arriving other sub, maybe he got hurt, had to leave early
            var subbedFor = gameRosterItems.Where(x => x.SubbingForPlayerId == teamRosterItem.PlayerId).ToList();

            if (subbedFor.Count > 0)
            {
              teamGameRoster.RosteredWasSubbedFor = true;
              teamGameRoster.SubbedForRostered = subbedFor;
            }
          }

          results.Add(teamGameRoster);
        }
      }

      return results;

    }
  }
}
