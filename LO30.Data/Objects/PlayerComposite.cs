using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using LO30.Data.Extensions;

namespace LO30.Data.Objects
{
  public class PlayerComposite
  {
    [Required, Key]
    public int PlayerId { get; set; }

    [Required, MaxLength(35)]
    public string FirstName { get; set; }

    [Required, MaxLength(35)]
    public string LastName { get; set; }

    [MaxLength(5)]
    public string Suffix { get; set; }

    [Required, MaxLength(1)]
    public string PreferredPosition { get; set; }

    [Required, MaxLength(1)]
    public string Shoots { get; set; }

    [Required]
    public int RatingStartYYYYMMDD { get; set; }

    [Required]
    public int RatingEndYYYYMMDD { get; set; }

    [Required, MaxLength(1)]
    public string RatingPosition { get; set; }

    [Required]
    public int RatingPrimary { get; set; }

    [Required]
    public int RatingSecondary { get; set; }

    [MaxLength(5)]
    public string TeamCode { get; set; }

    [MaxLength(15)]
    public string TeamNameShort { get; set; }

    [MaxLength(35)]
    public string TeamNameLong { get; set; }
  }
}