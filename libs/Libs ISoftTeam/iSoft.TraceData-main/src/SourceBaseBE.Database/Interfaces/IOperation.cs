﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceBaseBE.Database.Interfaces
{
  public interface IEntityOperation<TStatus, TState>
  {
    [Column(TypeName = "TINYINT")]
    public TStatus Status { get; set; }

    [Column(TypeName = "TINYINT")]
    public TState State { get; set; }

    [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
    public DateTime? StartAt { get; set; }

    [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
    public DateTime? EndAt { get; set; }
  }
}
