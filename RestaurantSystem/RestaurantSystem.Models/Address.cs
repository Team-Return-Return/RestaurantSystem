﻿namespace RestaurantSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Address
    {
        private DateTime createdOn;
        private bool isDeleted;

        public Address()
        {
            this.createdOn = DateTime.Now;
            this.isDeleted = false;
        }

        [Key]
        public long Id { get; set; }

        public DateTime CreatedOn
        {
            get
            {
                return this.createdOn;
            }
            set
            {
                this.createdOn = value;
            }
        }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted
        {
            get
            {
                return this.isDeleted;
            }
            set
            {
                this.isDeleted = value;
            }
        }

        [Required]
        public long CityId { get; set; }

        [Required]
        public City City { get; set; }

        [Required]
        [MaxLength(5)]
        public byte PostCode { get; set; }

        public string Street { get; set; }
    }
}
