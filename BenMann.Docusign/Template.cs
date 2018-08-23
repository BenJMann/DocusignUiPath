using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenMann.Docusign
{
    public class Template
    {
        public string templateId;
        public List<Role> templateRoles;
        public string status = "sent";

        public Template(string id)
        {
            templateId = id;
            templateRoles = new List<Role>();
        }
        public void AddRole(Role role)
        {
            templateRoles.Add(role);
        }
    }
    public class Role
    {
        public string roleName;
        public string name;
        public string email;

        public Role(string roleName)
        {
            this.roleName = roleName;
        }
        public Role(string roleName, string name, string email)
        {
            this.roleName = roleName;
            this.name = name;
            this.email = email;
        }
    }
}
