using FinalProject.Models;
using MyLoggerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Controllers;
using Role.Controllers;

namespace FinalProject.Controllers
{
    public class ComplaintController : ApiController
    {
        FinalProject_DBEntities obj = new FinalProject_DBEntities();
        public ComplaintController()
        {
            obj.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Complaint
        public ResponseData Get()
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Get() from ComplaintController Called");
            ResponseData ResponseData = new ResponseData();
            IEnumerable<T_Complaints> listcomplaint = obj.T_Complaints.ToList();
            if (listcomplaint != null)
            {
                //var User = (from u in listuser
                //            where u.UserId > 104
                //            select u);

                ResponseData.Data = listcomplaint;
                ResponseData.Error = null;
                ResponseData.Status = "Success";
                return ResponseData;
            }
            else
            {
                ResponseData.Error = null;
                ResponseData.Status = "No Complaint Found..";
                return ResponseData;
            }
        }

        // GET: api/Complaint/5


        // POST: api/Complaint
        public ResponseData Post([FromBody]T_Complaints c)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Post() from ComplaintController Called");
            ResponseData ResponseData = new ResponseData();
            if (c != null)
            {
               

                obj.T_Complaints.Add(c);
                obj.SaveChanges();
                ResponseData.Error = null;
                ResponseData.Status = "Complaint Added Succesfully...";
                return ResponseData;
            }
            else
            {
                ResponseData.Error = null;
                ResponseData.Status = "Please enter valid data..";
                return ResponseData;
            }

        }

        // PUT: api/Complaint/5
        public void Put(int id,[FromBody]T_Complaints a)
          {
            IEnumerable<T_Complaints> t = obj.T_Complaints.ToList();
                
            if(t != null)
            {
                
                T_Complaints ct = (from u in t
                                  where u.UserId == id
                                  select u).SingleOrDefault();
                ct.Status = a.Status;
                obj.SaveChanges();

                Email e = new Email();

                T_Users user = obj.T_Users.Find(id);

                e.to = user.EmailId;
                e.subject = " Related to Complaint in E-Housing Society";
                e.body = "Your complaint regarding " + ct.ComplaintType + " is resolved..." + "Do check ";
                UserController usr = new UserController();
                usr.SendEmail(e);
            }
           
        }

        // DELETE: api/Complaint/5
        public void Delete(int id)
        {
        }
    }
}
