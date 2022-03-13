using System;

namespace ConsoleAppYura_1
{
    class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Permission Permission { get; set; }
    }
}
