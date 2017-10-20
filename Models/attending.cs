using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using B3.Models;

namespace B3.Models
{
    public class Attending
    {
        public int attendingid {get;set;}
        public int userid {get;set;}

        public User User{get;set;}
      
        public int eventplanid {get;set;}

        public EventPlan eventPlan{get;set;}
     
    }
}