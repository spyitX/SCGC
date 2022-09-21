using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class EmployeeController : ControllerBase
    {
        //new Employee("1,Supinyo,Chaipanya,SupinyoC@gmail.com")
        //Employee E = new Employee();
        //Employee employee. = "1";
        //Employee.Firstname = "Supinyo";
        //Employee.Lastname = "Chaipanya";
        //E.iD() = 1;
        //E.FirstName() = "Supinyo";
       
        private ICacheProvider _cacheProvider;
        public EmployeeController(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }
        [Route("getAllEmployee")]
        public IActionResult GetAllEmployee()
        {  
            try
            {
                var employees = _cacheProvider.GetCachedResponse();
                //var employees = _cacheProvider.GetCachedResponse().Result;
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return new ContentResult()
                {
                    StatusCode = 500,
                    Content = "{ \n error : " + ex.Message + "}",
                    ContentType = "application/json"
                };
            }
        }
    }
}