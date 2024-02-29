﻿namespace PrecastFactorySystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Constants.DataConstants;

    public class Precast
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PrecastNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(PrecastType))]
        public int PrecastTypeId { get; set; }

        public PrecastType PrecastType { get; set; } = null!;

        public int Count { get; set; }

        [Required]
        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }

        public Project Project { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(ConcreteClass))]
        public int ConcreteClassId { get; set; }

        public ConcreteClass ConcreteClass { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ConcreteProjectAmount { get; set; }

        [NotMapped]
        public decimal ConcreteActualAmount
            => this.DepartmentPrecasts.Count > 0 ?
            this.DepartmentPrecasts.Average(dp => dp.ConcreteAmount)
            : default;


        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ReinforceProjectWeight { get; set; }

        [NotMapped]
        public decimal ReinforceActualWeight
            => this.PrecastReinforceOrders.Count > 0 ?
            this.PrecastReinforceOrders.Average(pro => pro.ReinforceOrder.PrecastWeight)
            : default;

        public ICollection<PrecastReinforce> PrecastReinforces = new HashSet<PrecastReinforce>();

        public ICollection<PrecastReinforceOrder> PrecastReinforceOrders = new HashSet<PrecastReinforceOrder>();

        public ICollection<PrecastDepartment> DepartmentPrecasts = new HashSet<PrecastDepartment>();
    }
}