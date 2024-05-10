using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blblan.Data.Entities;

public sealed class Conversation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public User User { get; set; }

    public ICollection<Message> Messages { get; set; }
}
