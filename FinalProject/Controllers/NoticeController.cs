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
    public class NoticeController : ApiController
    {
        FinalProject_DBEntities obj = new FinalProject_DBEntities();
        public NoticeController()
        {
            obj.Configuration.ProxyCreationEnabled = false;
        }
       
        // GET: api/Notice
        public ResponseData Get()
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Get() from NoticeController Called");
            ResponseData ResponseData = new ResponseData();
            var listnotice = obj.T_Notices.ToList();
            if (listnotice != null)
            {

                var not = from l in listnotice
                          where l.MeetingDate >= DateTime.Now.Date
                          select l;

                ResponseData.Data = not;
                ResponseData.Error = null;
                ResponseData.Status = "Success";
                return ResponseData;
            }
            else
            {
                ResponseData.Error = null;
                ResponseData.Status = "No Notices Found..";
                return ResponseData;
            }
        }

        // GET: api/Notice/5
        public ResponseData Get(int id)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Get(id) from NoticeController for familymember Called");
            ResponseData response = new ResponseData();
            T_FamilyMembers u = obj.T_FamilyMembers.Find(id);
            if (u != null)
            {

                response.Data = u;
                response.Error = null;
                response.Status = "Success";
                return response;
            }
            else
            {
                response.Error = null;
                response.Status = "Enter valid Id ...";
                return response;
            }
        }

        // POST: api/Notice
        [HttpPost]
        public ResponseData Post([FromBody]T_Notices N)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Post() from NoticeController Called");
            ResponseData ResponseData = new ResponseData();
            if (N != null)
            {

                obj.T_Notices.Add(N);
                obj.SaveChanges();
                ResponseData.Error = null;
                ResponseData.Status = "Notices added Succesfully...";
                return ResponseData;
            }
            else
            {
                ResponseData.Error = null;
                ResponseData.Status = "Please enter valid data..";
                return ResponseData;
            }

        }

        // PUT: api/Notice/5
        public void Put(int id, [FromBody]T_Notices N)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Put() from NoticeController Called");
            T_Notices n1 = obj.T_Notices.Find(id);
            n1.Announcement = N.Announcement;
            n1.MeetingName = N.MeetingName;
            n1.MeetingDate = N.MeetingDate;
            obj.SaveChanges();

        }

        // DELETE: api/Notice/5
        public ResponseData Delete(int id)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Delete() from NoticeController Called");
            ResponseData ResponseData = new ResponseData();

            obj.T_Notices.Remove(obj.T_Notices.Find(id));
            obj.SaveChanges();
            ResponseData.Error = null;
            ResponseData.Status = "Notices Removed Successfully...";
            return ResponseData;
        }
    }
}
