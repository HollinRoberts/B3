using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using B3.Models;
using System.ComponentModel.DataAnnotations;

namespace B3.Models
{
    public class EventValidate
    {   
        [Required]
        [MinLength(2)]
        public string title {get;set;}    
        [Required]
        public DateTime date {get;set;}
        [Required]
        public DateTime time {get;set;}
        [Required]
        public int duration {get;set;}
        [Required]
        public string units {get;set;}
        [Required]
        [MinLength(10)]
        public string description {get;set;}

        
    }
}