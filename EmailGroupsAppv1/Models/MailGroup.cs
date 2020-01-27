using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmailGroupsAppv1.Models
{
  public class MailGroup
  {
    public int Id { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; }
    public string Description { get; set; }
    public List<MailAddress> Addresses { get; set; } = new List<MailAddress>();
  }
}
