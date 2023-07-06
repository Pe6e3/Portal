using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DAL.Entities;
public class MyLogger : BaseEntity
{
    public DateTime Date { get; set; }
    public int UserId { get; set; }
    public string? UserIP { get; set; }
    public string? userLocation { get; set; }
    public string? UserClick { get; set; }
    public virtual User? User { get; set; }

}

