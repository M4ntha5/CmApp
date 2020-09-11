﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmApp.Contracts.Entities
{
    public class SummaryEntity
    {
        [ForeignKey("Car")]
        public int ID { get; set; }
        public double BoughtPrice { get; set; } = 0;
        public double SoldPrice { get; set; } = 0;
        public DateTime SoldDate { get; set; } = new DateTime(2000, 01, 01, 0, 0, 0, DateTimeKind.Utc);
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string SoldWithin { get; set; } = "";
        public bool Sold { get; set; } = false;
        public double Total { get; set; } = 0;
        public string BaseCurrency { get; set; } = "";
        public string SelectedCurrency { get; set; } = "";
        public double Profit { get; set; }


        public virtual CarEntity Car { get; set; }
    }
}
