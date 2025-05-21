using System;

namespace Composite
{
    class Employee : EmployeeComponent
    {
        public Employee(string name) : base(name) { }

        public override void PerformDuties()
        {
            Console.WriteLine($"Employee {Name} is performing duties.");
        }

        public override void Display(int level = 0)
        {
            string indent = new string(' ', level * 2);
            Console.WriteLine($"{indent}- {Name}");
        }
    }
}
