﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MasterTool_WebApp.Models
{
    public class Feedback
    {
        [Key]
        [Column("feedback_id")]
        public long FeedbackId { get; set; }

        [Required]
        [MaxLength(500)]
        [Column("text")]
        public string Text { get; set; }

        [Required]
        [Range(1, 5)]
        [Column("rating")]
        public int Rating { get; set; }

        [Required]
        [ForeignKey("Order")]
        [Column("order_id")]
        public long OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
