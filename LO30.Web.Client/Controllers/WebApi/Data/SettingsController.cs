using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace LO30.Controllers.Data
{
  public class SettingsController : ApiController
  {
    public SettingsController()
    {
    }

    //public List<Setting> GetSettings()
    //{
    //  var results = new List<Setting>();

    //  using (var context = new LO30Context())
    //  {
    //    results = context.Settings.ToList();
    //  }
    //  return results;
    //}

  }
}
