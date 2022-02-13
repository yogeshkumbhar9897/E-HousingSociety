using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Net.Http.Formatting;
using FinalProject.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;
using MyLoggerLib;
using MySecurityLib;

namespace Role.Controllers
{
    public class UserController : ApiController
    {
        
        FinalProject_DBEntities obj = new FinalProject_DBEntities();

        MyLoggerLib.ILogger logger = LoggerFactory.GetLogger(1);
        

        public UserController()
        {
            obj.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/User
        public ResponseData Get()
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Get() from UserController Called");
            ResponseData response = new ResponseData();
            var listuser = obj.T_Users.ToList();
            if(listuser != null)
            {
                var User = (from u in listuser
                            where u.UserId > 102
                            select u);

                response.Data = User;
                response.Error = null;
                response.Status = "Success";
                return response;
            }
            else
            {
                response.Error = null;
                response.Status = "No User Found..";
                return response;
            }
        }

        // GET: api/User/5
        public ResponseData Get(int id)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Get(id) from UserController Called");
            ResponseData response = new ResponseData();
            T_Users u=  obj.T_Users.Find(id);
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

        [HttpGet]
        [Route("api/User/PasswordLog")]
        public ResponseData PasswordLog()
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Passwordlog() from UserController Called");
            ResponseData response = new ResponseData();
            var pl = obj.T_PasswordHistoryLog.ToList();
            if (pl != null)
            {
                var passlist = from p in pl
                               where p.UserId > 102
                               select p;

                response.Data = passlist;
                response.Error = null;
                response.Status = "Success";
                return response;
            }
            else
            {
                response.Error = null;
                response.Status = "No PasswordLog Found..";
                return response;
            }
            
        }

       
        [HttpPost]
        [Route("api/User/Registration")]
        public ResponseData Registration([FromBody]T_Users u)
         {
            ResponseData response = new ResponseData();
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Registration() from UserController Called");
            u.RoleId = 2;

            string encryptPass = null;
            var passwd = u.Password;
            Class1.Encrypt(passwd, out encryptPass);
            u.Password = encryptPass;
           var res =  obj.T_Users.Add(u);
            obj.SaveChanges();


            if (res != null)
            {
                Email mail = new Email();
                mail.to = u.EmailId;
                mail.subject = "E-Housing Society";
                mail.body = "Congratulations... You have been Registered as owner in E-Housing Society...!!!  your password is :"+ passwd;
                SendEmail(mail);


                response.Error = null;
                response.Status = "User Register Successfully...";
                return response;
            }
            else
            {
                response.Error = null;
                response.Status = "Please enter valid data..";
                return response;
            }

        }


        [HttpPost]
        [Route("api/User/Login")]
        public ResponseData Login([FromBody]T_Users u)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Login() from UserController Called");
            ResponseData response = new ResponseData();
            string encrypt = null;
            Class1.Encrypt(u.Password, out encrypt);


            if (u.EmailId != null && u.Password != null)
            {
                var listuser = obj.T_Users.ToList();
                T_Users validuser = (from user in listuser
                                     where user.EmailId == u.EmailId &&
                                     user.Password == encrypt
                                     select user).SingleOrDefault();
                if(validuser != null)
                {
                    response.Data = validuser;
                    response.Error = null;
                    response.Status = "Success";
                    return response;
                }
                else
                {
                    response.Error = null;
                    response.Status = "Incorrect Credintial";
                    return response;
                }
            }
            else
                {
                response.Error = null;
                response.Status = "Field are empty";
                return response;
            }
         
           
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]T_Users u)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("put() from UserController Called");
            T_Users u1 = obj.T_Users.Find(id);
            u.RoleId = 2;
            u1.Name = u.Name;
            u1.EmailId = u.EmailId;
            u1.Password = u.Password;
            u1.MobileNo = u.MobileNo;
            u1.Age = u.Age;
            u1.Gender = u.Gender;
            u1.IsOnline = u.IsOnline;
            u1.IsLocked = u.IsLocked;
            u1.RoleId = u.RoleId;
            
            obj.SaveChanges();
        }

        // DELETE: api/User/5
        public ResponseData Delete(int id)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Delete() from UserController Called");
            ResponseData response = new ResponseData();

           
                obj.T_Users.Remove(obj.T_Users.Find(id));
                obj.SaveChanges();
           
               response.Error = null;
                response.Status = "User Deleted Successfully...";
               return response;

        }

        [HttpPost]
        [Route("api/User/OTP")]
        public ResponseData OTP([FromBody]dynamic otpDetails)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("OTP() from UserController Called");
            string email = otpDetails.EmailId.ToString();


            var userlist = obj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == email
                        select u).SingleOrDefault();
            string o = otpDetails.OTP.ToString();

            var otpd = obj.T_OTP_Details.ToList();
            var vOTP = (from v in otpd
                        where v.UserId == User.UserId && v.OTP == o
                        select v).SingleOrDefault();

            if (vOTP != null)
            {
                ResponseData RC = new ResponseData();
                RC.Status = "success";
                RC.Error = null;
                RC.Data = vOTP;
                return RC;
            }
            else
            {
                ResponseData RC = new ResponseData();
                RC.Status = "fail";
                RC.Error = null;
                RC.Data = false;
                return RC;
            }
        }




        [HttpPost]
        [Route("api/User/IsExist")]
        public ResponseData IsExist([FromBody]T_Users value)
        {

            var userlist = obj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == value.EmailId
                        select u).SingleOrDefault();
            if (User != null)
            {
                ResponseData RC = new ResponseData();
                string mails = GetOTP();

                T_OTP_Details otp = new T_OTP_Details();
                otp.UserId = User.UserId;
                otp.ValidTill = DateTime.Now;
                otp.GeneratedOn = DateTime.Now;
                otp.OTP = mails;
                obj.T_OTP_Details.Add(otp);
                obj.SaveChanges();


                RC.Status = "success";
                RC.Error = null;
                RC.Data = mails;
                return RC;
            }
            else
            {
                ResponseData RC = new ResponseData();
                RC.Status = "fail";
                RC.Error = null;
                RC.Data = false;
                return RC;
            }

        }


        [HttpPut]
        [Route("api/User/UpdatePassword")]
        public ResponseData updatepassword([FromBody]T_Users value)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Update password() from UserController Called");

            string encryptPass = null;
            var passwd = value.Password;
            Class1.Encrypt(passwd, out encryptPass);
            value.Password = encryptPass;

            var userlist = obj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == value.EmailId
                        select u).SingleOrDefault();

            if (User != null)
            {
                User.Password = value.Password;
                obj.SaveChanges();
                ResponseData rc = new ResponseData();
                rc.Status = "success";
                rc.Error = null;
                rc.Data = User;
                return rc;
            }
            else
            {
                ResponseData rc = new ResponseData();
                rc.Status = "fail";
                rc.Error = null;
                rc.Data = null;
                return rc;
            }
        }
        private string GetOTP(string otpType = "1", int len = 5)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Get OTP() from UserController Called");
            //otptype 1 = alpha numeric
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
            if (otpType == "1")
            {
                characters += alphabets + small_alphabets + numbers;
            }
            int length = 5;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }

        [HttpPost]
        [Route("api/User/SendEmail")]
        public string SendEmail([FromBody]Email e)
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Send Mail() from UserController Called");
            string to = e.to;
            string subject = e.subject;
            string body = e.body;
            string result = "Message Sent Successfully..!!";
            string senderID = "vnankar@gmail.com";// use sender’s email id here..
            const string senderPassword = "9420808623"; // sender password here…
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",// smtp server address here…
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                    Timeout = 30000,
                };
                MailMessage message = new MailMessage(senderID, to, subject, body);
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                result = "Error sending email.!!!";
            }
            return result;
        }

        [Route("api/User/UploadFile")]
        public class UploadApiController : ApiController
        {
            [HttpPost]
            public async Task<HttpResponseMessage> PostFormData()
            {

                // Check if the request contains multipart/form-data.
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                string root = HttpContext.Current.Server.MapPath("~/Photos");
                var provider = new CustomMultipartFormDataStreamProvider(root);
                try
                {
                    // Read the form data.
                    await Request.Content.ReadAsMultipartAsync(provider);

                    // This illustrates how to get the file names.
                    foreach (MultipartFileData file in provider.FileData)
                    {
                        Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                        Trace.WriteLine("Server file path: " + file.LocalFileName);
                    }
                    ResponseData responseStatus = new ResponseData() { Status = "Success", Error = null };
                    return Request.CreateResponse(HttpStatusCode.OK, responseStatus);
                }
                catch (System.Exception e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                }
            }
        }
        public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
        {
            public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

            public override string GetLocalFileName(HttpContentHeaders headers)
            {
                return headers.ContentDisposition.FileName.Replace("\"", string.Empty);
            }
        }



    }

}

