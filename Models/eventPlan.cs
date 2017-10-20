using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using B3.Models;

namespace B3.Models
{
    public class EventPlan
    {
        public int eventplanid {get;set;}
        public string title {get;set;}  
      
    
        public DateTime date {get;set;}
      
        public DateTime time {get;set;}

        public int duration {get;set;}

        public string units {get;set;}

        public string description {get;set;}

        public int userid{get;set;}

        public User organizer{get;set;}

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime createdAt {get;set;}

        public int attendingid {get;set;}

        public List<Attending> attending {get;set;}

        public EventPlan()
        {
            attending = new List<Attending>();
        }
    }
}