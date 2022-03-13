using System;


namespace ConsoleAppYura_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var userManagement = new UserManagement();
            var permissionManagement = new PermissionManagement();

            OperationForUserInConsole(permissionManagement, userManagement);

        }
        private static void OperationForUserInConsole(PermissionManagement pm, UserManagement um)
        {
            Console.WriteLine("Для авторизации введите число 1");
            Console.WriteLine("For exit enter number 2");
            var numberEnter = Console.ReadLine();
            if (!int.TryParse(numberEnter, out int number))
            {
                Console.WriteLine("Incorrect data");
            }
            else
            {
                if (number == 1)
                {
                    var currentUser = AuthUserInConsole(um);
                    if (currentUser == null)
                    {
                        OperationForUserInConsole(pm, um);
                        Console.Read();
                    }
                    else
                    {
                        if (currentUser.Permission.Name == "admin")
                        {
                            AdminOperationWithAddUserOrPermissionInConsole(currentUser, pm, um);
                        }
                        else
                        {
                            WriteDataAuthorizeUser(currentUser);
                            Console.Read();
                        }
                    }
                }else if (number == 2)
                {
                    Console.Clear();
                }
                else
                {
                    OperationForUserInConsole(pm, um);
                    Console.Read();
                }
                                
            }
        }
        private static User AuthUserInConsole(UserManagement um)
        {
            //Console.Clear();
            Console.WriteLine("Для авторизации верно введите имя и пароль");
            Console.Write("Имя: ");
            var nameUser = Console.ReadLine();
            Console.Write("Пароль: ");
            var passwordUser = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(nameUser) || String.IsNullOrWhiteSpace(nameUser))
            {
                AuthUserInConsole(um);
            }
            else
            {
                var currentUser = um.AuthorizeUser(nameUser, passwordUser);
                if (currentUser == null)
                {
                    Console.WriteLine("Нет такого пользователя.");
                    AuthUserInConsole(um);
                }
                return currentUser;
            }
            return null;
        }
        private static void AdminOperationWithAddUserOrPermissionInConsole(User u, PermissionManagement pm, UserManagement um)
        {
            //Console.Clear();
            Console.WriteLine("For add permission enter number 1");
            Console.WriteLine("For add user enter number 2");
            Console.WriteLine("For write your parametrs enter number 3");
            Console.WriteLine("For return on last level enter number 4");
            if (!int.TryParse(Console.ReadLine(), out int number_1))
            {
                Console.WriteLine("Incorrect data");
                AdminOperationWithAddUserOrPermissionInConsole(u, pm, um);
            }
            else
            {
                if (number_1 == 1)
                {
                    AddPermissionInConsole(u, pm, um);
                }
                else if (number_1 == 2)
                {
                    AddUserInConsole(u, pm, um);
                }
                else if (number_1 == 3)
                {
                    WriteDataAuthorizeUser(u);
                }
                else if (number_1 == 4)
                {
                    OperationForUserInConsole(pm, um);
                }
                AdminOperationWithAddUserOrPermissionInConsole(u, pm, um);
            }
        }
        private static void WriteDataAuthorizeUser(User u)
        {
            Console.ForegroundColor = (ConsoleColor) u.Permission.ColorIndex;
            Console.WriteLine("Вы успешно авторизовались. Ваши личные данные:");
            Console.WriteLine($"Id is {u.Id}. Name is {u.Name}. Password is {u.Password}. Your permision is {u.Permission.Name}.");
        }
        private static void AddPermissionInConsole(User u, PermissionManagement pm, UserManagement um)
        {
            Console.WriteLine("Для добавления прав пользователя в базу, введите его название");
            Console.Write("Название: ");
            var namePermission = Console.ReadLine();
            
            if (String.IsNullOrWhiteSpace(namePermission))
            {
                AddPermissionInConsole(u, pm, um);
            }
            else
            {
                var findPermission = pm.GetPermissionByName(namePermission);
                if (findPermission == null)
                {
                    pm.AddPermission(namePermission);
                }
                else
                {
                    Console.WriteLine("Введенный доступ пользователя уже есть в базе.");
                    AdminOperationWithAddUserOrPermissionInConsole(u, pm, um);
                }
            }
        }

        private static void AddUserInConsole(User u, PermissionManagement pm, UserManagement um)
        {
            var pmList = pm.Permissions;
            var countPmList = pmList.Count;
            foreach (var elP in pmList)
            {
                Console.WriteLine($"Id is {elP.Id}. Name is {elP.Name}.");
            }
            Console.WriteLine("Для добавления пользователя в базу, введите его имя, пароль и ID его прав из списка:");
            Console.Write("Имя: ");
            var nameUser = Console.ReadLine();
            Console.Write("Пароль: ");
            var passwordUser = Console.ReadLine();
            Console.Write("Id прав пользователя: ");
            var permissionUser = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(nameUser) || String.IsNullOrWhiteSpace(nameUser) || !int.TryParse(permissionUser, out int numberPM))
            {
                AddUserInConsole(u, pm, um);
            }
            else
            {
                um.AddUser(nameUser, passwordUser, numberPM, pm);
            }
        }



        #region Task1
        private static void TaskFindNumber()
        {
            var randomNumber = new Random().Next(1, 100);
            for (var i = 0; ; i++)
            {
                Console.ForegroundColor = (ConsoleColor)(i % 16);
                Console.BackgroundColor = (ConsoleColor)(15 - i % 16);
                Console.WriteLine("Введите число от 0 до 100.");
                if (!int.TryParse(Console.ReadLine(), out int number))
                {
                    Console.WriteLine("Incorrect data");
                }
                else
                {
                    if (randomNumber == number)
                    {
                        Console.WriteLine("Вы угадали!");
                        Console.WriteLine("Количество итераций равно " + i);
                        break;
                    }
                    else if (randomNumber < number)
                    {
                        Console.WriteLine("Введенное число больше необходимого.");
                    }
                    else if (randomNumber > number)
                    {
                        Console.WriteLine("Введенное число меньше необходимого.");
                    }
                }
            }
        }
        private static void TaskPrintNumberDouble()
        {
            double a;
            var rn = new Random().Next(1,100);
            var rnd = new Random().NextDouble();
            a = rn + rnd;
            Console.WriteLine("Число с точностью до сотых " + a);
        }
        #endregion
    }
}
