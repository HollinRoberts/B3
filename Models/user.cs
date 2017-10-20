using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using B3.Models;

namespace B3.Models
{
    public class User
    {
        public int userid {get;set;}
        public string first {get;set;}  
      
        public string last {get;set;}
    
        public string email {get;set;}
      
        public string password {get;set;}

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime createdAt {get;set;}

        public int eventplanid {get;set;}
        public List<EventPlan> activities {get;set;}
        public int attendingid {get;set;}

        public List<Attending> attending {get;set;}

       
        public User()
        {
            attending = new List<Attending>();
            activities= new List<EventPlan>();
           
        }
    }
}