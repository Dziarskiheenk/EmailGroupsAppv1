using System.ComponentModel.DataAnnotations;

namespace EmailGroupsAppv1.Models
{
  public class MailAddress
  {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    public string Address { get; set; }

    public string GroupName { get; set; }
    public virtual MailGroup MailGroup { get; set; }
  }
}
