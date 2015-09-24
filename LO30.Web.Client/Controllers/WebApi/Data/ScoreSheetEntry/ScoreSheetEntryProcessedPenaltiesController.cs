using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Extensions;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.ScoreSheetEntry
{
  public class ScoreSheetEntryProcessedPenaltiesController : ApiController
  {
    
    public ScoreSheetEntryProcessedPenaltiesController()
    {
      
    }

    public List<ScoreSheetEntryProcessedPenalty> GetScoreSheetEntryPenaltiesProcessed()
    {
      var results = new List<ScoreSheetEntryProcessedPenalty>();

      using (var context = new LO30Context())
      {
        results = context.ScoreSheetEntryProcessedPenalties.IncludeAll().ToList();
      }
      return results.OrderBy(x => x.GameId)
                    .OrderBy(x => x.Period)
                    .OrderByDescending(x => x.TimeRemaining)
                    .ToList();
    }

    public List<ScoreSheetEntryProcessedPenalty> GetScoreSheetEntryPenaltiesProcessedByGameId(int gameId)
    {
      var results = new List<ScoreSheetEntryProcessedPenalty>();

      using (var context = new LO30Context())
      {
        results = context.ScoreSheetEntryProcessedPenalties.Where(x => x.GameId == gameId).IncludeAll().ToList();
      }
      return results.OrderBy(x => x.GameId)
                    .OrderBy(x => x.Period)
                    .OrderByDescending(x => x.TimeRemaining)
                    .ToList();
    }
  }
}
