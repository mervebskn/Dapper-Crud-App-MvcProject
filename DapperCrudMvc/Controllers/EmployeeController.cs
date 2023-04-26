using Dapper;
using DapperCrudMvc.Dapper;
using DapperCrudMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DapperCrudMvc.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View(DapperORM.ReturnList<EmployeeModel>("EmployeeViewAll"));
        }
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@EmployeeId", id);
                return View(DapperORM.ReturnList<EmployeeModel>("EmployeeById", param).FirstOrDefault<EmployeeModel>());
            }
        }
        //burda id 0 ise null da döner ya da redirect deriz ya da alert gelsin vs.

        [HttpPost]
        public ActionResult AddOrEdit(EmployeeModel emp)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@EmployeeId", emp.EmployeeId);
            param.Add("@Name", emp.Name);
            param.Add("@Position", emp.Position);
            param.Add("@Age", emp.Age);
            param.Add("@Salary", emp.Salary);
            DapperORM.ExecuteWithoutReturn("EmployeeAddOrEdit", param);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@EmployeeId", id);
            DapperORM.ExecuteWithoutReturn("EmployeeDeleteById",param);
            return RedirectToAction("Index");
        }
    }
}