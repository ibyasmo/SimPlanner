using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SM.Metadata
{    
    public class ManequinMetadata
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        [StringLength(512)]
        public string DecommissionReason { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PurchaseDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? LocalCurrencyPurchasePrice { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DecommissionDate { get; set; }
    }
}
