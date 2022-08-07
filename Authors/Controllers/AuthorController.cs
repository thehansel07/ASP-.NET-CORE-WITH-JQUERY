using Authors.Core.Interface;
using Authors.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authors.Controllers
{
    public class AuthorController : Controller
    {
        IBaseRepository<Author> _authorRepository;
        public AuthorController(IBaseRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public IActionResult Index()
        {
            return View();
        }     
        public IActionResult Index1()
        {
            return View();
        }

        public async Task<IActionResult> LoadData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
               
                // Skiping number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();
               
                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();

                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
               
                // Sort Column Direction ( asc ,desc)      
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                
                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data  
                var authorsData = await _authorRepository.GetAllAsync();

                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    authorsData = authorsData.OrderBy(c => c.FirstName);
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    authorsData = authorsData.Where(m => m.LastName.ToLower() == searchValue.ToLower()
                    || Convert.ToString(m.IdBook).ToLower() == searchValue.ToLower() 
                    || m.FirstName.ToString() == searchValue.ToLower());
                }

                //total number of rows count   
                recordsTotal = authorsData.Count();

                //Paging   
                var data = authorsData.Skip(skip).Take(pageSize).ToList();

                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrio un error", ex);
            }

        }


        public async Task<JsonResult> ListAuthors() 
        {
            var authorsData = await _authorRepository.GetAllAsync();

            return Json(new { data = authorsData });
        }

    }
}
