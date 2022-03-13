using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAppYura_1
{
    class PermissionManagement
    {
        public List<Permission> Permissions { get; set; } = new List<Permission>();

        public PermissionManagement()
        {
            Permissions.Add(new Permission
            {
                Id = 1,
                Name = "admin",
                ColorIndex = GenerateColorIndex()
            });
        }

        public Permission GetPermission(int id)
        {      
            return Permissions.FirstOrDefault(permission => permission.Id == id);
        }

        public Permission GetPermissionByName(string name)
        {
            return Permissions.FirstOrDefault(permission => permission.Name == name);
        }

        public void AddPermission(Permission permission)
        {
            var item = GetPermission(permission?.Id ?? 0);

            if (item == null)
            {
                Permissions.Add(permission);
            }
        }

        public int GenerateColorIndex()
        {
            return new Random().Next(0,15);
        }
    }
}
