using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Models
{
  public class PlayerStatus
  {
    [Key, Column(Order = 1)]
    public int PlayerId { get; set; }

    [Required]
    public int PlayerStatusTypeId { get; set; }

    [Key, Column(Order = 2)]
    public int EventYYYYMMDD { get; set; }

    [Required]
    public bool CurrentStatus { get; set; }

    [ForeignKey("PlayerId")]
    public virtual Player Player { get; set; }

    [ForeignKey("PlayerStatusTypeId")]
    public virtual PlayerStatusType PlayerStatusType { get; set; }
  }
}