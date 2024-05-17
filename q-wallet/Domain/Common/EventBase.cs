using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using q_wallet.Domain.Enums;

namespace q_wallet.Domain.Common
{
	public class EventBase
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid EventId { get; set; }
		public string EventName { get; set; } = string.Empty;	
		public EventType EventType { get; set; }	
		public bool IsDeleted { get; set; }
		public DateTime CreatedOn { get; set; }
	}
}
