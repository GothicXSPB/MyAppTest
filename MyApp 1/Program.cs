// See https://aka.ms/new-console-template for more information

using MyApp.Dal;
using MyApp.Sevices;

DBService dBService = new DBService();
Context dBContext = new Context();

var menu = Console.ReadLine();
string command = menu.Substring(0, 7);

if (command != null)
    switch (command)
    {
        case "MyApp 1":
            dBContext.Database.EnsureCreated();
            break;

        case "MyApp 2":
            dBService.AddRecordInDB(menu);
            break;

        case "MyApp 3":
            dBService.GetUniqueRecordsSortedByNameAndDate();
            break;

        case "MyApp 4":
            dBService.DoATestTask();
            break;

        case "MyApp 5":
            dBService.GetRecordsByLastName();
            break;
    }
