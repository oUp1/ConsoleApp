using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAppYura_1
{
    class UserManagement
    {
        private List<User> Users { get; set; }
        private PermissionManagement PermissionManagement { get; set; }

        public UserManagement()
        {
            PermissionManagement = new PermissionManagement();
            Users = new List<User>();
            Users.Add(new User
            {
                Id = new System.Guid(),
                Name = "admin",
                Password = "admin",
                Permission = PermissionManagement.GetDefaultPermission()
            });
        }
        public object GetUserById(Guid gid)
        {
            return Users.First(x => x.Id == gid);
        }
        public object GetUserByName(string n)
        {
            return Users.First(x => x.Name == n);
        }
        public void AddUser(string name, string password, int permissionId, PermissionManagement pm)
        {
            if (!Users.Any(x => (x.Name == name && x.Password == password)))
            {
                Users.Add(new User
                {
                    Id = new System.Guid(),
                    Name = name,
                    Password = password,
                    Permission = pm.GetPermission(permissionId, pm),
                });
            }
        }
        public User AuthorizeUser(string name, string password)
        {
            return Users.FirstOrDefault(x => (x.Name == name && x.Password == password));
        }
    }
}
