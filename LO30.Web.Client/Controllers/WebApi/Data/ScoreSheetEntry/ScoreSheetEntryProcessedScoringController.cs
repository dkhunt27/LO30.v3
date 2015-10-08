using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Extensions;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.ScoreSheetEntry
{
  public class ScoreSheetEntryProcessedScoringController : ApiController
  {
    
    public ScoreSheetEntryProcessedScoringController()
    {
      
    }

    public List<ScoreSheetEntryProcessedGoal> GetScoreSheetEntriesProcessed()
    {
      var results = new List<ScoreSheetEntryProcessedGoal>();

      using (var context = new LO30Context())
      {
        results = context.ScoreSheetEntryProcessedGoals.IncludeAll().ToList();
      }
      return results.OrderBy(x => x.GameId)
                    .ThenBy(x => x.Period)
                    .ThenByDescending(x => x.TimeRemaining)
                    .ToList();
    }

    public List<ScoreSheetEntryProcessedGoal> GetScoreSheetEntriesProcessedByGameId(int gameId)
    {
      var results = new List<ScoreSheetEntryProcessedGoal>();

      using (var context = new LO30Context())
      {
        results = context.ScoreSheetEntryProcessedGoals.Where(x => x.GameId == gameId).IncludeAll().ToList();
      }
      return results.OrderBy(x => x.GameId)
                    .ThenBy(x => x.Period)
                    .ThenByDescending(x => x.TimeRemaining)
                    .ToList();
    }
  }
}
