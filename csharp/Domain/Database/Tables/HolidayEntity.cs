using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Database.Tables;

[Table("holidays")]
[Keyless]
public class HolidayEntity
{
    [Column("holiday")]
    public DateTime Holiday { get; set; }

    [Column("description")]
    [MaxLength(255)]
    [Unicode(false)]
    public string Description { get; set; } = default!;
}