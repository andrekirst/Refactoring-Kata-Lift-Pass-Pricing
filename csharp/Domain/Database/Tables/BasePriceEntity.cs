using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Database.Tables;

[Table("base_price")]
public class BasePriceEntity
{
    [Key]
    [Column("pass_id")]
    public int Id { get; set; }

    [Column("type")]
    [MaxLength(255)]
    [Unicode(false)]
    public string Type { get; set; }

    [Column("cost")]
    public int Cost { get; set; }
}