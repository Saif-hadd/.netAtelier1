namespace WebApplication1.Models.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {

        List<Employee> lemployees;
        public EmployeeRepository()
        {
            lemployees = new List<Employee>()
{

new Employee {Id=1,Name="Sofien ben ali", Departement= "comptabilité",Salary=1000},
new Employee {Id=2,Name="Mourad triki", Departement= "RH",Salary=1500},
new Employee {Id=3,Name="ali ben mohamed", Departement= "informatique",Salary=1700},
new Employee {Id=4,Name="tarak aribi", Departement= "informatique",Salary=1100}

};
        }

        public void Add(Employee e)
        {
            lemployees.Add(e);
        }
        public Employee FindByID(int id)
        {
            var emp = lemployees.FirstOrDefault(a => a.Id == id);
            return emp;
        }
        public void Delete(int id)
        {
            var emp = FindByID(id);
            lemployees.Remove(emp);
        }
        public IList<Employee> GetAll()
        {
            return lemployees;
        }
        public void Update(int id, Employee newemployee)
        {
            var emp = FindByID(id);
            emp.Name = newemployee.Name;
            emp.Departement = newemployee.Departement;
            emp.Salary = newemployee.Salary;
        }

        public List<Employee> Search(string term)
        {
            if (!string.IsNullOrEmpty(term))
            {
                return lemployees.Where(a => a.Name.Contains(term)).ToList();

            }
            else
                return lemployees;
        }
        public double SalaryAverage()
        {
            return lemployees.Average(x => x.Salary);
        }
        public double MaxSalary()
        {
            return lemployees.Max(x => x.Salary);
        }
        public int HrEmployeesCount()
        {
            return lemployees.Where(x => x.Departement == "HR").Count();
        }
    }
}
