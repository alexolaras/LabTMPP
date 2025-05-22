
namespace Composite
{
    abstract class EmployeeComponent
    {
        public string Name { get; }

        public EmployeeComponent(string name)
        {
            Name = name;
        }

        public abstract void PerformDuties();
        public abstract void Display(int level = 0);
    }
}
