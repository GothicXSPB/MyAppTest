using System.Diagnostics;
using MyApp.Dal;
using MyApp.Models;

namespace MyApp.Sevices
{
    public class DBService
    {
        private readonly Random r = new Random();

        public void AddRecordInDB(string command)
        {
            string record = command.Substring(8);
            using (var context = new Context())
            {
                Employees employees = new Employees();
                string[] parts = record.Split(" ");
                employees.LastName = parts[0];
                employees.FirstName = parts[1];
                employees.Surname = parts[2];
                employees.DateOfBirth = DateTime.Parse(parts[3]);
                employees.Age = GetAge(employees.DateOfBirth);
                employees.Gender = parts[4];

                context.Employees.Add(employees);
                context.SaveChanges();
            }
        }

        public void GetUniqueRecordsSortedByNameAndDate()
        {
            var context = new Context();

            var result = context.Employees
                .GroupBy(e => new { e.LastName, e.FirstName, e.Surname, e.DateOfBirth })
                .Select(x => new
                {
                    FullName = x.Key.LastName + " " + x.Key.FirstName + " " + x.Key.Surname,
                    DateBirth = x.Key.DateOfBirth,
                    Sex = x.First().Gender,
                    Age = DateTime.Now.Year - x.Key.DateOfBirth.Year

                })
                .OrderBy(r => r.FullName)
                .ToList(); ;

            foreach (var record in result)
            {
                Console.WriteLine($"ФИО: {record.FullName}");
                Console.WriteLine($"Дата рождения: {record.DateBirth.ToShortDateString()}");
                Console.WriteLine($"Пол: {record.Sex}");
                Console.WriteLine($"Полных лет: {record.Age}");
                Console.WriteLine();
            }
        }

        public void GetRecordsByLastName()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var context = new Context();
            var result = context.Employees.Where(x => x.Gender == "M" && x.LastName.StartsWith("F"));

            stopwatch.Stop();
            TimeSpan elapsed = stopwatch.Elapsed;

            foreach (var record in result)
            {
                Console.WriteLine($"ФИО: {record.LastName} {record.FirstName} {record.Surname}");
                Console.WriteLine($"Пол: {record.Gender}");
                Console.WriteLine();
            }

            Console.WriteLine($"Время выполнения запроса: {elapsed}");
        }       

        public void DoATestTask()
        {
            for (int i = 0; i <= 999900; i++)
            {
                AutoFeelDB();
            }
            for (int i = 0; i <= 100; i++)
            {
                AutoFeelDBWithConditions();
            }
        }

        public void AutoFeelDB()
        {
            var context = new Context();
            context.Employees.Add(GetRandomRecord());
            context.SaveChanges();
        }
        public void AutoFeelDBWithConditions()
        {
            var context = new Context();
            context.Employees.Add(GetRandomRecordWithConditions());
            context.SaveChanges();
        }

        private int GetAge(DateTime dateOfBirth)
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - dateOfBirth.Year;
            if (currentDate.Month < dateOfBirth.Month ||
                (currentDate.Month == dateOfBirth.Month &&
                    currentDate.Day < dateOfBirth.Day))
            {
                age--;
            }
            return age;
        }

        private Employees GetRandomRecord()
        {
            Random r = new Random();
            Employees employees = new Employees();

            employees.FirstName = GenerateName();
            employees.LastName = GenerateName();
            employees.Surname = GenerateName();
            var gender = GenerateGender();
            if (gender == true)
            {
                employees.Gender = "M";
            }
            else
            {
                employees.Gender = "F";
            }
            employees.DateOfBirth = GenerateDateOfBirth();
            employees.Age = GetAge(employees.DateOfBirth);
            return employees;
        }

        private Employees GetRandomRecordWithConditions()
        {
            Random r = new Random();
            Employees employees = new Employees();

            employees.FirstName = GenerateName();
            employees.LastName = "F" + GenerateName();
            employees.Surname = GenerateName();
            employees.Gender = "M";
            employees.DateOfBirth = GenerateDateOfBirth();
            employees.Age = GetAge(employees.DateOfBirth);
            return employees;

        }

        private string GenerateName()
        {
            var range = Enumerable.Range(0, r.Next(3, 15));
            var chars = range.Select(x => (char)r.Next('A', 'Z')).ToArray();
            var str = new string(chars);
            return str;
        }

        private bool GenerateGender()
        {
            return Convert.ToBoolean(r.Next(0, 2));
        }

        private DateTime GenerateDateOfBirth()
        {
            DateTime start = new DateTime(1985, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(r.Next(range));
        }

    }
}
