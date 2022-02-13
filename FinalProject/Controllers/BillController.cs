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
    public class BillController : ApiController
    {
        FinalProject_DBEntities obj = new FinalProject_DBEntities();

        public BillController()
        {
            obj.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Bill
        public ResponseData Get()
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Get() from BillController Called");
            ResponseData ResponseData = new ResponseData();
            var listbills = obj.T_Bills.ToList();
            if (listbills != null)
            {
                //var User = (from u in listuser
                //            where u.UserId > 104
                //            select u);

                ResponseData.Data = listbills;
                ResponseData.Error = null;
                ResponseData.Status = "Success";
                return ResponseData;
            }
            else
            {
                ResponseData.Error = null;
                ResponseData.Status = "No Bills Found..";
                return ResponseData;
            }
        }
        // GET: api/Bill/5
        public ResponseData Get(int id)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Get(id) from BillController Called");
            ResponseData response = new ResponseData();
            IEnumerable<T_Bills> b = obj.T_Bills.ToList();
            if (b != null)
            {
                var bill = from t in b
                           where t.UserId == id
                           select t;

                response.Data = bill;
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

        // POST: api/Bill
        [HttpPost]
        
        public ResponseData Post([FromBody]T_Bills B)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Post() from BillController Called");
            ResponseData ResponseData = new ResponseData();
            if (B != null)
            {
                try
                {
                    B.Total = B.CulturalFund + B.EmergencyFund + B.Maintenance + B.Water + B.Light;

                    obj.T_Bills.Add(B);
                    obj.SaveChanges();
                    ResponseData.Error = null;
                    ResponseData.Status = "Bills Added Succesfully...";
                    return ResponseData;
                }
                catch (Exception e)
                {
                    ResponseData.Error = e;

                    return ResponseData;
                }
            }
            else
            {
                ResponseData.Error = null;
                ResponseData.Status = "Please enter valid data..";
                return ResponseData;
            }

        }


        // PUT: api/Bill/5
        public void Put(int id, [FromBody]T_Bills B)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Put() from BillController Called");
            T_Bills b1 = obj.T_Bills.Find(id);
            b1.CulturalFund = B.CulturalFund;
            b1.EmergencyFund = B.EmergencyFund;
            b1.Maintenance = B.Maintenance;
            b1.Total = b1.CulturalFund + b1.EmergencyFund + b1.Maintenance + B.Water + B.Light ;
            obj.SaveChanges();

        }

        // DELETE: api/Bill/5
        public ResponseData Delete(int id)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Delete() from BillController Called");
            ResponseData ResponseData = new ResponseData();

            obj.T_Bills.Remove(obj.T_Bills.Find(id));
            obj.SaveChanges();
            ResponseData.Error = null;
            ResponseData.Status = "Bills Deleted Successfully...";
            return ResponseData;
        }
    }
}
