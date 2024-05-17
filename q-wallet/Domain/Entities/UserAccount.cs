using q_wallet.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace q_wallet.Domain.Entities
{
	public class UserAccount : EntityBase
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PlaceOfBirth { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } 
        public string Nationality { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
    }
}
