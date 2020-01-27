using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmailGroupsAppv1.Models
{
  public class MailGroup
  {
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; }
    public string Description { get; set; }
    public List<MailAddress> Addresses { get; } = new List<MailAddress>();
  }
}
