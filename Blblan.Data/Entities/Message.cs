using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blblan.Data.Entities;

public class Message
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int ConversationId { get; set; }

    public string Question { get; set; }

    public string Answer { get; set; }

    public DateTime Timestamp { get; set; }

    public Conversation Conversation { get; set; }
}