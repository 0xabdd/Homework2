using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AgeCalculator.Models;

namespace AgeCalculator.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Person Person1 { get; set; }

        [BindProperty]
        public Person Person2 { get; set; }

        [BindProperty]
        public Person Person3 { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var person1Age = CalculateAge(Person1.BirthDate);
            var person2Age = CalculateAge(Person2.BirthDate);
            var person3Age = CalculateAge(Person3.BirthDate);

            ViewData["Person1Age"] = person1Age;
            ViewData["Person2Age"] = person2Age;
            ViewData["Person3Age"] = person3Age;

            return Page();
        }

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age))
                age--;

            return age;
        }
    }
}
