using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Logic;

namespace WebApplication1.Controllers
{

    public class HomeController : Controller
    {


        public ViewResult Index(string phoneNumber="")
        {
            var model = new Models.PhoneNumberViewModel 
            {
                PhoneNumber = "",
                PageSize = 20,
                PageNumber = 1,
                PhoneNumbers = new List<Models.PhoneNumberVariation>()
            };

            return View(model);
        }

        private List<Models.PhoneNumberVariation> GetVariations(string phoneNumber, int pageNumber, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber)) { return new List<Models.PhoneNumberVariation>(); }

            var nums = PhoneNumberGenerator.GenerateNumbers(phoneNumber)
             .Select((s, i) => new Models.PhoneNumberVariation { Id = i+1, NumberVariation = s })
             .Skip((pageSize) * (pageNumber - 1))
             .Take(pageSize)
             .ToList();

            return nums;

        }

        [HttpPost]
        public ViewResult ViewVariations(string phoneNumber, int pageSize=20)
        {

            var nums = GetVariations(phoneNumber, 1, pageSize);

            var numVariations = PhoneNumberGenerator.NumVariations(phoneNumber); 

            var model = new Models.PhoneNumberViewModel
            {
                PhoneNumber = phoneNumber,
                PageSize = pageSize,
                NumVariations = numVariations,
                PageNumber = 1,          
                PhoneNumbers = nums
            };

            return View("Index",model);
        }


        [HttpPost]
        public ViewResult Next(Models.PhoneNumberViewModel m)
        {
            m.PageNumber++;
            m.PhoneNumbers = GetVariations(m.PhoneNumber, m.PageNumber, m.PageSize);

            return View("Index",m);
        }

        [HttpPost]
        public ViewResult Previous(Models.PhoneNumberViewModel m)
        {
            m.PageNumber--;
            m.PhoneNumbers = GetVariations(m.PhoneNumber, m.PageNumber, m.PageSize);

            return View("Index", m);
        }

        [HttpPost]
        public ViewResult PageSizeChange(Models.PhoneNumberViewModel m)
        {
            m.PageNumber = 1;
            m.PhoneNumbers = GetVariations(m.PhoneNumber, m.PageNumber, m.PageSize);
            return View("Index", m);
        }
    }
}