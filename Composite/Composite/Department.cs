using System;
using System.Collections.Generic;

namespace Composite
{
    class Department : EmployeeComponent
    {
        private List<EmployeeComponent> _members = new List<EmployeeComponent>();

        public Department(string name) : base(name) { }

        public override void PerformDuties()
        {
            Console.WriteLine($"Department {Name} delegating tasks...");
            foreach (var member in _members)
                member.PerformDuties();
        }

        public override void Display(int level = 0)
        {
            string indent = new string(' ', level * 2);
            Console.WriteLine($"{indent}+ {Name}");

            foreach (var member in _members)
                member.Display(level + 1);
        }

        public void AddMember(EmployeeComponent member)
        {
            _members.Add(member);
        }

        public void RemoveMember(EmployeeComponent member)
        {
            _members.Remove(member);
        }
    }
}
