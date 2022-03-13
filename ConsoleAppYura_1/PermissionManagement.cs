using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleAppYura_1
{
    class PermissionManagement
    {
        public List<Permission> Permissions { get; set; }
        public PermissionManagement()
        {
            Permissions = new List<Permission>();

            Permissions.Add(new Permission
            {
                Id = 1,
                Name = "admin",
                ColorIndex = GenerateColorIndex()
            });
        }

        public Permission GetPermission(int id, PermissionManagement pm)
        {      
            var currentPermission = Permissions.FirstOrDefault(permission => permission.Id == id);
            if (currentPermission == null) currentPermission = Permissions.FirstOrDefault(permission => permission.Id == 1);
            return currentPermission;
        }

        public Permission GetPermissionByName(string n)
        {
            return Permissions.FirstOrDefault(permission => permission.Name == n);
        }

        public void AddPermission(string name)
        {
            if (!Permissions.Any(x => x.Name == name))
            {
                var countPermission = Permissions.Count();
                Permissions.Add(new Permission
                {
                    Id = countPermission + 1,
                    Name = name,
                    ColorIndex = GenerateColorIndex()
                });
            }
        }

        public Permission GetDefaultPermission()
        {
            return Permissions.FirstOrDefault();
        }

        private static int GenerateColorIndex()
        {
            return new Random().Next(0,15);
        }
    }
}
