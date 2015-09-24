﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Models
{
  public class Setting
  {
    [Required, Key, Column(Order = 1)]
    public int SettingId { get; set; }

    [Required, MaxLength(35), Index("PK2", 1, IsUnique = true)]
    public string SettingName { get; set; }

    [Required]
    public string SettingValue { get; set; }
  }
}