using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            Department headOffice = new Department("Head Office");

            Department itDept = new Department("IT Department");
            Department hrDept = new Department("HR Department");
            Department financeDept = new Department("Finance Department");

            Employee alice = new Employee("Alice");
            Employee bob = new Employee("Bob");
            Employee charlie = new Employee("Charlie");
            Employee dana = new Employee("Dana");
            Employee eve = new Employee("Eve");

            itDept.AddMember(alice);
            itDept.AddMember(bob);

            hrDept.AddMember(charlie);
            financeDept.AddMember(dana);
            financeDept.AddMember(eve);

            headOffice.AddMember(itDept);
            headOffice.AddMember(hrDept);
            headOffice.AddMember(financeDept);

            Console.WriteLine("=== Displaying Organization Structure ===\n");
            headOffice.Display();

            Console.WriteLine("\n=== Performing Duties ===\n");
            headOffice.PerformDuties();
        }
    }
}
