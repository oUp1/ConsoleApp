using System.Collections.Generic;
using System.Linq;

namespace ConsoleAppYura_1
{
    class UserManagement
    {
        private PermissionManagement PermissionManagement { get; set; } = new PermissionManagement();
        public List<User> Users { get; set; } = new List<User>();

        public UserManagement()
        {
            Users.Add(new User
            {
                Id = Users.Count + 1,
                Name = "admin",
                Password = "admin",
                Permission = PermissionManagement.Permissions.FirstOrDefault()
            });
        }
        public User GetUser(int id)
        {
            return Users.FirstOrDefault(x => x.Id == id);
        }
        public object GetUserByName(string name)
        {
            return Users.First(x => x.Name == name);
        }
        public void AddUser(User user)
        {
            var item = GetUser(user?.Id ?? 0);

            if(item != null)
            {
                Users.Add(user);
            }
        }
        public User AuthorizeUser(string name, string password)
        {
            return Users.FirstOrDefault(x => x.Name == name && x.Password == password);
        }
    }
}
