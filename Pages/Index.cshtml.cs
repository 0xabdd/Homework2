using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AgeCalculator.Models;
using System.Collections.Generic;

namespace AgeCalculator.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Person> People { get; set; }

        public void OnGet()
        {
            People = new List<Person>();
            AddNewPerson();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var validPeople = GetValidPeople();

            if (validPeople.Count < 3 || validPeople.Count > 5)
            {
                return Page();
            }

            var peopleWithAges = CalculateAges(validPeople);

            ViewData["People"] = peopleWithAges;

            return Page();
        }

        public void AddNewPerson()
        {
            if (People.Count < 5)
            {
                People.Add(new Person());
            }
        }

        private List<Person> GetValidPeople()
        {
            var validPeople = new List<Person>();

            foreach (var person in People)
            {
                if (!string.IsNullOrEmpty(person.Name) && person.BirthDate != null)
                {
                    validPeople.Add(person);
                }
            }

            return validPeople;
        }

        private List<PersonInfo> CalculateAges(List<Person> people)
        {
            var peopleWithAges = new List<PersonInfo>();

            foreach (var person in people)
            {
                var age = CalculateAge(person.BirthDate);
                peopleWithAges.Add(new PersonInfo { Person = person, Age = age });
            }

            return peopleWithAges;
        }

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age))
                age--;

            return age;
        }

        public class PersonInfo
        {
            public Person Person { get; set; }
            public int Age { get; set; }
        }
    }
}
