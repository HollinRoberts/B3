using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using B3.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace B3.Controllers
{
    public class ActivityController : Controller
    {
        private B3Context _context;
         public ActivityController(B3Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("activity")]
        public IActionResult activity()
        {int? id = HttpContext.Session.GetInt32("userId");
            if(id!=null){
            List<EventPlan> AllEvents = new List<EventPlan>();
            // List<EventPlan> AllEvents = _context.eventplan.Include(attending=>attending.attending).ThenInclude(user=>user.User).ToList();
            AllEvents = _context.eventplan.Include(user=>user.organizer).Include(attending=>attending.attending).ThenInclude(user=>user.User).ToList();
            ViewBag.AllEvents=AllEvents;
            ViewBag.User=(int)id;


            return View("activity");
            }
            return RedirectToAction("index","login");

        }
        [HttpGet]
        [Route("new")]
        public IActionResult New()
        {
            int? id = HttpContext.Session.GetInt32("userId");
            if(id!=null){
                List<Object> error = new List<object>();
                ViewBag.errors= error;
                return View("new");
            }
            return RedirectToAction("index","login");
        }
        [HttpPost]
        [Route("create")]
        public IActionResult create(EventValidate event1)
        {   DateTime now = DateTime.Now;
            int? id = HttpContext.Session.GetInt32("userId");
            if (TryValidateModel(event1)){
                if(event1.date>now){
                EventPlan nw= new EventPlan
                    {
                        title = event1.title,
                        date = event1.date,
                        time = event1.time,
                        duration = event1.duration,
                        units = event1.units,
                        description = event1.description,
                        userid = (int)id,
                        
                    };
                    _context.Add(nw);
                    _context.SaveChanges();
                    
                    TempData["id"]=nw.eventplanid;
                    return RedirectToAction("details",new{id=nw.eventplanid});
                }else{
                    ModelState.AddModelError("DateError", "Date must be in the future.");
                    ViewBag.errors = ModelState.Values;
                    return View("new");
                }
            }
            ViewBag.errors = ModelState.Values;
            return View("new");
        }
        [HttpGet]
        [Route("details/{id}")]
        public IActionResult details(int id)
        {  
        //      if(TempData["id"]!=null){
        //     int wedid=(int)TempData["id"];
        // }
            int? userid = HttpContext.Session.GetInt32("userId");
            if(userid!=null){
                EventPlan Details = _context.eventplan.Include(user=>user.organizer).Include(attending=>attending.attending).ThenInclude(user=>user.User).SingleOrDefault(detail=>detail.eventplanid==id);   
                ViewBag.Details=Details;
                // Other code
                ViewBag.User=(int)userid;
                return View("details");
                
                }
            return RedirectToAction("index","login");
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult logout(int id)
        {
            HttpContext.Session.Clear();
            // Other code
            return RedirectToAction("index","login");
        }
        [HttpGet]
        [Route("join/{id}")]
        public IActionResult join(int id)
        {
            int? uid = HttpContext.Session.GetInt32("userId");
            Attending nw= new Attending
                    {
                        userid = (int)uid,
                        eventplanid = id,
                        
                    };
                    _context.Add(nw);
                    _context.SaveChanges();
            // Other code
            return RedirectToAction("activity","activity");
        }
        [HttpGet]
        [Route("leave/{id}")]
        public IActionResult leave(int id)
        {
             Attending toremove = _context.attending.SingleOrDefault(detail=>detail.eventplanid==id);   
            _context.attending.Remove(toremove);
            _context.SaveChanges();
            // Other code
            return RedirectToAction("activity","activity");
        }
        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult delete(int id)
        {
            EventPlan toremove = _context.eventplan.SingleOrDefault(detail=>detail.eventplanid==id);   
            _context.eventplan.Remove(toremove);
            _context.SaveChanges();
            // Other code
            return RedirectToAction("activity","activity");
        }
    }
}
