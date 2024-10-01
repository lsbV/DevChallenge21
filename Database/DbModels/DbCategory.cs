using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DbModels;

public class DbCategory
{
    public required int Id { get; set; }
    [Required]
    [MaxLength(500)]
    public required string Title { get; set; }
    [Required]
    public required HashSet<string> Points { get; set; }
}