using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FinalProject.Controllers
{
    public class RoleController : ApiController
    {
        FinalProject_DBEntities roleObj = new FinalProject_DBEntities();


        public RoleController()
        {
            roleObj.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Role
        public IEnumerable<T_Roles> Get()
        {
            return roleObj.T_Roles.ToList();
        }

        // GET: api/Role/5
        public T_Roles Get(int id)
        {
            T_Roles role = roleObj.T_Roles.Find(id);
            return role;
        }

        // POST: api/Role
        public void Post([FromBody]T_Roles Role)
        {
            //roleObj.T_Roles.Add(Role);
            //roleObj.SaveChanges();
            roleObj.proc_AddRole(Role.RoleName);

        }

        // PUT: api/Role/5
        public void Put(int id, [FromBody]T_Roles r)
        {
            // T_Roles role = roleObj.T_Roles.Find(id);
            //role.RoleName = r.RoleName;
            //roleObj.SaveChanges();
            roleObj.proc_ModifyRole(id, r.RoleName);
        }

        // DELETE: api/Role/5
        public void Delete(int id)
        {
            T_Roles delRole = roleObj.T_Roles.Find(id);
            roleObj.T_Roles.Remove(delRole);
            roleObj.SaveChanges();
            //roleObj.proc_RemoveRole(delRole.RoleId);
        }
    }
}
