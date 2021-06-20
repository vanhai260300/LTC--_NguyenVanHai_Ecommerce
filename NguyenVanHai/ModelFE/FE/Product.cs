namespace ModelFE.FE
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public decimal? UnitCost { get; set; }

        public long? Quantity { get; set; }

        [StringLength(200)]
        public string Image { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public int IDCategory { get; set; }

        public virtual Category Category { get; set; }
    }
}
