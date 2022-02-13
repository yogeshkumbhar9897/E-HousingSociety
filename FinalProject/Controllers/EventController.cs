using FinalProject.Models;
using MyLoggerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FinalProject.Controllers
{
    public class EventController : ApiController
    {
        FinalProject_DBEntities obj = new FinalProject_DBEntities();
        public EventController()
        {
            obj.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Event
        public ResponseData Get()
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Get() from EventController Called");
            ResponseData ResponseData = new ResponseData();
            var listevent = obj.T_Events.ToList();
            if (listevent != null)
            {
                //var User = (from u in listuser
                //            where u.UserId > 104
                //            select u);

                var evt = from e in listevent
                          where e.EventDate.Date >= DateTime.Now.Date
                          select e;

                ResponseData.Data = evt;
                ResponseData.Error = null;
                ResponseData.Status = "Success";
                return ResponseData;
            }
            else
            {
                ResponseData.Error = null;
                ResponseData.Status = "No Events Found..";
                return ResponseData;
            }
        }

        // GET: api/Event/5
        //public string Get(int id)
        //{
        //    return "value";
        //}


        // POST: api/Event
        [HttpPost]
        public ResponseData Post([FromBody]T_Events E)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Post() from EventController Called");
            ResponseData ResponseData = new ResponseData();
            try
            {
                if (E.EventName != null && E.EventTime != null)
                {

                    obj.T_Events.Add(E);
                    obj.SaveChanges();
                    ResponseData.Error = null;
                    ResponseData.Status = "Events Added Succesfully...";
                    return ResponseData;
                }
                else
                {
                    ResponseData.Error = null;
                    ResponseData.Status = "Please enter All Fields..";
                    return ResponseData;
                }
            }
            catch (Exception)
            {

                return null;
            }

        }

        // PUT: api/Event/5
        public void Put(int id, [FromBody]T_Events E)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Put() from EventController Called");
            T_Events e1 = obj.T_Events.Find(id);
            e1.EventName = E.EventName;
            e1.EventDate = E.EventDate;
            obj.SaveChanges();

        }

        // DELETE: api/Event/5
        public ResponseData Delete(int id)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Delete() from EventController Called");
            ResponseData ResponseData = new ResponseData();

            obj.T_Events.Remove(obj.T_Events.Find(id));
            obj.SaveChanges();
            ResponseData.Error = null;
            ResponseData.Status = "Events Removed Successfully...";
            return ResponseData;
        }
    }
}
