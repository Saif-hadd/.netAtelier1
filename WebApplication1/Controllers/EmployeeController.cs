using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Repositories;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IRepository<Employee> employeeRepository;

        // Injection de dépendance
        public EmployeeController(IRepository<Employee> empRepository)
        {
            employeeRepository = empRepository;
        }

        // Affiche la liste des employés
        public ActionResult Index()
        {
            var employees = employeeRepository.GetAll();
            ViewData["EmployeesCount"] = employees.Count();
            ViewData["SalaryAverage"] = employeeRepository.SalaryAverage();
            ViewData["MaxSalary"] = employeeRepository.MaxSalary();
            ViewData["HREmployeesCount"] = employeeRepository.HrEmployeesCount();
            return View(employees);
        }

        // Affiche les détails d'un employé par ID
        public ActionResult Details(int id)
        {
            var employee = employeeRepository.FindByID(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // Crée un nouvel employé (GET: afficher le formulaire)
        public ActionResult Create()
        {
            return View();
        }

        // Crée un nouvel employé (POST: traiter le formulaire)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee e)
        {
            if (ModelState.IsValid)
            {
                employeeRepository.Add(e);
                return RedirectToAction(nameof(Index));
            }
            return View(e);
        }

        // Mise à jour d'un employé (GET: afficher le formulaire de modification)
        public ActionResult Edit(int id)
        {
            var employee = employeeRepository.FindByID(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // Mise à jour d'un employé (POST: traiter le formulaire de modification)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employee newEmployee)
        {
            if (ModelState.IsValid)
            {
                employeeRepository.Update(id, newEmployee);
                return RedirectToAction(nameof(Index));
            }
            return View(newEmployee);
        }

        // Supprimer un employé
        public ActionResult Delete(int id)
        {
            var employee = employeeRepository.FindByID(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // Confirmer la suppression (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            employeeRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Search (string term)
        {
            var result = employeeRepository.Search(term);
            return View("Index", result);
        }
    }
}