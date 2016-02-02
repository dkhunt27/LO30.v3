using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LO30.Web.Client.Models
{
  public class TeamFeedModel
  {
    public string TeamCode { get; set; }

    public string TeamNameShort { get; set; }

    public string TeamNameLong { get; set; }

    public string TeamFeedUrl { get; set; }
  }
}