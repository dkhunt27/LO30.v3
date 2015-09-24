using DDay.iCal;
using DDay.iCal.Serialization;
using DDay.iCal.Serialization.iCalendar;
using LO30.Data.Contexts;
using LO30.Data.Extensions;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LO30.Controllers
{
    public class ScheduleController : Controller
    {
        LO30Context _context;
        public ScheduleController(LO30Context context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TeamFeed(int seasonId, int teamId, bool playoffs)
        {
            var gameTeams = _context.GameTeams
                                    .Where(x => x.SeasonId == seasonId && x.TeamId == teamId && x.Game.Playoffs == playoffs)
                                    .IncludeAll()
                                    .OrderBy(x => x.Team.TeamNameShort)
                                    .ToList();

            iCalendar ical = new iCalendar();
            foreach (var gameTeam in gameTeams)
            {
                Event icalEvent = ical.Create<Event>();

                var summary = gameTeam.OpponentTeam.TeamNameShort + " vs " + gameTeam.Team.TeamNameShort;
                if (gameTeam.HomeTeam)
                {
                  summary = gameTeam.Team.TeamNameShort + " vs " + gameTeam.OpponentTeam.TeamNameShort;
                }

                icalEvent.Summary = summary;
                icalEvent.Description = summary + " " + gameTeam.Game.Location;

                var year = gameTeam.Game.GameDateTime.Year;
                var mon = gameTeam.Game.GameDateTime.Month;
                var day = gameTeam.Game.GameDateTime.Day;
                var hr = gameTeam.Game.GameDateTime.Hour;
                var min = gameTeam.Game.GameDateTime.Minute;
                var sec = gameTeam.Game.GameDateTime.Second;
                icalEvent.Start = new iCalDateTime(gameTeam.Game.GameDateTime);
                icalEvent.Duration = TimeSpan.FromHours(1.25);
                icalEvent.Location = "Eddie Edgar " + gameTeam.Game.Location;
            }

            ISerializationContext ctx = new SerializationContext();
            ISerializerFactory factory = new SerializerFactory();
            IStringSerializer serializer = factory.Build(ical.GetType(), ctx) as IStringSerializer;

            string output = serializer.SerializeToString(ical);
            var contentType = "text/calendar";

            return Content(output, contentType);

            //var bytes = Encoding.UTF8.GetBytes(output);
            //return File(bytes, contentType, "BillBrown.ics");
        }
    }
}
