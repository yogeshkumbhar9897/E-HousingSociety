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
    
    public class FamilyController : ApiController
    {
        FinalProject_DBEntities obj = new FinalProject_DBEntities();
        public FamilyController()
        {
            obj.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Family
        public ResponseData Get()
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Get() from FamilyController Called");
            ResponseData ResponseData = new ResponseData();
            var listfamily = obj.T_FamilyMembers.ToList();

            if (listfamily != null)
            {
               

                ResponseData.Data = listfamily;
                ResponseData.Error = null;
                ResponseData.Status = "Success";
                return ResponseData;
            }
            else
            {
                ResponseData.Error = null;
                ResponseData.Status = "No User Found..";
                return ResponseData;
            }
        }


       // GET: api/Family/5
        public ResponseData Get(int id)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Get(id) from FamilyController Called");
            ResponseData response = new ResponseData();
            IEnumerable<T_FamilyMembers> u = obj.T_FamilyMembers.ToList();
            if (u != null)
            {
                var uniqefamily = (from fam in u
                                   where fam.UserId == id
                                   select fam);
                response.Data = uniqefamily;
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

      
       

        // POST: api/Family
        [HttpPost]
        [Route("api/Family/Registration")]
        public ResponseData Registration([FromBody]T_FamilyMembers T)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Registration() from FamilyController Called");
            ResponseData ResponseData = new ResponseData();
            if (T != null)
            {

                obj.T_FamilyMembers.Add(T);
                obj.SaveChanges();
                ResponseData.Error = null;
                ResponseData.Status = "Member Added Successfully...";
                return ResponseData;
            }
            else
            {
                ResponseData.Error = null;
                ResponseData.Status = "Please enter valid data..";
                return ResponseData;
            }

        }

        // PUT: api/Family/5
        public void Put(int id, [FromBody]T_FamilyMembers T)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Put() from FamilyController Called");
            T_FamilyMembers t1 = obj.T_FamilyMembers.Find(id);
            t1.MemberName = T.MemberName;
            t1.MemberAge = T.MemberAge;
            t1.MemberGender = T.MemberGender;
            t1.MemberRelation = T.MemberRelation;
            obj.SaveChanges();
        }
        // DELETE: api/Family/5
        public ResponseData Delete(int id)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Delete() from FamilyController Called");
            ResponseData ResponseData = new ResponseData();

            obj.T_FamilyMembers.Remove(obj.T_FamilyMembers.Find(id));
            obj.SaveChanges();
            ResponseData.Error = null;
            ResponseData.Status = "Member Deleted Successfully...";
            return ResponseData;

        }
    }
}
