using System;
using System.Linq;

namespace ConsoleAppYura_1
{
    class Program
    {
        public UserManagement UserManagement { get; } = new UserManagement();
        public PermissionManagement PermissionManagement { get; } = new PermissionManagement();

        static void Main(string[] args)
        {
            var program = new Program();
            program.OperationForUserInConsole();
        }

        private void OperationForUserInConsole()
        {
            Console.Clear();
            Console.WriteLine("Для авторизации введите число 1");
            Console.WriteLine("For exit enter number 2");
            if (!int.TryParse(Console.ReadLine(), out int number))
            {
                Console.WriteLine("Incorrect data");
            }
            else
            {
                if (number == 1)
                {
                    var currentUser = AuthUserInConsole();

                    if (currentUser.Permission.Name == "admin")
                    {
                        AdminOperationWithAddUserOrPermissionInConsole(currentUser);
                    }
                    else
                    {
                        DisplayAuthorizedUser(currentUser);
                        Console.Read();
                    }
                }
            }
        }
        private User AuthUserInConsole()
        {
            Console.Clear();
            Console.WriteLine("Для авторизации верно введите имя и пароль");
            Console.Write("Имя: ");
            var userName = Console.ReadLine();
            Console.Write("Пароль: ");
            var userPassword = Console.ReadLine();

            var currentUser = UserManagement.AuthorizeUser(userName, userPassword);
            if (currentUser == null)
            {
                Console.WriteLine("Нет такого пользователя.");
                AuthUserInConsole();
            }
            return currentUser;
        }

        private void AdminOperationWithAddUserOrPermissionInConsole(User user)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("For add permission enter number 1");
                Console.WriteLine("For add user enter number 2");
                Console.WriteLine("For write your parametrs enter number 3");
                Console.WriteLine("For return on last level enter number 4");
                Console.WriteLine("For exit 5");

                if (!int.TryParse(Console.ReadLine(), out int number_1))
                {
                    AdminOperationWithAddUserOrPermissionInConsole(user);
                }
                else
                {
                    if (number_1 == 1)
                    {
                        AddPermissionInConsole(user);
                    }
                    else if (number_1 == 2)
                    {
                        AddUserInConsole(user);
                    }
                    else if (number_1 == 3)
                    {
                        DisplayAuthorizedUser(user);
                    }
                    else if (number_1 == 4)
                    {
                        OperationForUserInConsole();
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
        private void DisplayAuthorizedUser(User user)
        {
            Console.Clear();
            Console.ForegroundColor = (ConsoleColor)user.Permission.ColorIndex;
            Console.WriteLine("Вы успешно авторизовались. Ваши личные данные:");
            Console.WriteLine($"Id is {user.Id}. Name is {user.Name}. Password is {user.Password}. Your permision is {user.Permission.Name}.");
            Console.WriteLine("Enter something to back");
            Console.Read();
        }

        private void AddPermissionInConsole(User user)
        {
            Console.Clear();
            Console.Write("Enter permission name: ");
            var permissionName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(permissionName))
            {
                AddPermissionInConsole(user);
            }
            else
            {
                var permission = PermissionManagement.GetPermissionByName(permissionName);

                if (permission == null)
                {
                    PermissionManagement.AddPermission(new Permission
                    {
                        Id = PermissionManagement.Permissions.Count + 1,
                        Name = permissionName,
                        ColorIndex = PermissionManagement.GenerateColorIndex()
                    });
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Введенный доступ пользователя уже есть в базе.");
                    AdminOperationWithAddUserOrPermissionInConsole(user);
                }
            }
        }

        private void AddUserInConsole(User user)
        {
            Console.Clear();
            foreach (var permission in PermissionManagement.Permissions)
            {
                Console.WriteLine($"Id - {permission.Id}. Name - {permission.Name}.");
            }

            Console.WriteLine("---------------------------------");
            Console.WriteLine("Для добавления пользователя в базу, введите его имя, пароль и ID его прав из списка:");
            Console.Write("Имя: ");
            var userName = Console.ReadLine();
            Console.Write("Пароль: ");
            var userPassword = Console.ReadLine();
            Console.Write("Id прав пользователя: ");
            var userPermission = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userName) ||
                string.IsNullOrWhiteSpace(userPassword) || 
                !int.TryParse(userPermission, out int permissionId) ||
                !PermissionManagement.Permissions.Select(x => x.Id).Contains(permissionId))
            {
                AddUserInConsole(user);
            }
            else
            {
                UserManagement.AddUser(new User
                {
                    Id = UserManagement.Users.Count + 1,
                    Name = userName,
                    Password = userPassword,
                    Permission = PermissionManagement.GetPermission(permissionId)
                });
            }
        }
    }
}
