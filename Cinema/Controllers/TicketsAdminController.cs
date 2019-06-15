using Cinema.Models;
using Cinema.Models.Tickets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Type = Cinema.Models.Tickets.Type;


namespace Cinema.Controllers
{
    public class TicketAdminController : Controller
    {

        private string PathToJson = "~/Files/Tickets.json"; // относительный путь

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult SaveTickets()
        {
            var movies = new Movie[]
            {
                new Movie
                {
                    Id = 1,
                    Name = "Ангел (2018)",
                    Description = "Карлитосу всего лишь семнадцать лет. Но главный герой является самым настоящим воплощением ужаса. Внешне он выглядит как святой, словно является самим ангелом. Но в глубине души у него настоящий мрак, который с каждым днём становится все хуже. Ситуация действительно складывается не самым лучшим образом и остаётся надеяться, что в будущем он как-то поменяет своё отношение ко всему окружающему. Но на самом деле это не так, ведь недавно в школе он встречает Рамона. Он точно такой же злой парень, как и Карлитосу. Вместе они собираются составить самый настоящий дует, который будет нарушать все имеющиеся правила. К чему же это в конечном итоге приведёт и каков будет результат всей этой ситуации? Это скоро предстоит узнать и приходится надеяться лишь на успех. Таким ли он будет?",
                    Duration = 118,
                    Genre = new []{Genre.Drama, Genre.Crime, Genre.Biography},
                    Type = new []{Type.D2 },
                    MinAge = 18,
                    Rating = 6.9f,
                    ImageUrl = "https://kinorad.info/uploads/posts/2019-01/1546589367-2038864493.jpg"
                },
                new Movie
                {
                    Id = 2,
                    Name = "Покемон. Детектив Пикачу (2019)",
                    Description = "История начинается с таинственного исчезновения частного детектива экстра- класса Гарри Гудмана, расследовать которое предстоит его 21-летнему сыну Тиму. Помощь в расследовании ему окажет бывший партнер отца, детектив Пикачу — уморительный, остроумный и обаятельный сыщик, который является загадкой даже для себя самого. Обнаружив, что они каким-то фантастическим образом способны общаться друг с другом, Тим и Пикачу объединяют усилия в захватывающем расследовании этой запутанной истории. В погоне за уликами по неоновым улицам Райм Сити — современного разросшегося мегаполиса, где люди и покемоны живут бок о бок в гиперреалистичном мире игрового экшна, — они встречают самых разнообразных покемонов и раскрывают ужасный заговор, который способен разрушить это мирное сосуществование и стать угрозой для всей вселенной покемонов.",
                    Duration = 138,
                    Genre = new []{Genre.Fantasy, Genre.Cartoons},
                    Type = new []{Type.D2,Type.D3 },
                    MinAge = 6,
                    Rating = 7.6f,
                    ImageUrl = "https://kinorad.info/uploads/posts/2019-05/1557859848_iphone360_994864.jpg"
                }
            };

            var tariffs = new Tariff[]
            {
                new Tariff
                {
                    Id = 1,
                    Name = "Standart",
                    Cost = 100
                },
                new Tariff
                {
                    Id = 2,
                    Name = "Dbox",
                    Cost = 250
                }
            };

            var halls = new Hall[]
            {
                new Hall
                {
                    Id = 1,
                    Name = "Hall 1"
                },
                 new Hall
                {
                    Id = 2,
                    Name = "Hall 2"
                }
            };

            var timeslots = new Timeslot[]
            {
                new Timeslot
                {
                    Id = 1,
                    HallId = 1,
                    MovieId = 1,
                    StartTime = DateTime.Now.AddHours(-1),
                    TariffId = 1
                },
                new Timeslot
                {
                    Id = 2,
                    HallId = 2,
                    MovieId = 2,
                    StartTime = DateTime.Now,
                    TariffId = 2
                },
                 new Timeslot
                {
                    Id = 3,
                    HallId = 1,
                    MovieId = 1,
                    StartTime = DateTime.Now.AddHours(1),
                    TariffId = 1
                },
                 new Timeslot
                {
                    Id = 4,
                    HallId = 2,
                    MovieId = 2,
                    StartTime = DateTime.Now.AddHours(-1),
                    TariffId = 2
                },
                new Timeslot
                {
                    Id = 5,
                    HallId = 1,
                    MovieId = 2,
                    StartTime = DateTime.Now,
                    TariffId = 2
                },
                 new Timeslot
                {
                    Id = 6,
                    HallId = 2,
                    MovieId = 1,
                    StartTime = DateTime.Now.AddHours(1),
                    TariffId = 1
                },
            };

            var jsonModel = new TicketsJsonModel
            {
                Halls = halls,
                Movies = movies,
                Tariffs = tariffs,
                Timeslots = timeslots
            };

            var json = JsonConvert.SerializeObject(jsonModel);

            var jsonFilePath = HttpContext.Server.MapPath(PathToJson);//физический путь к файлу

            System.IO.File.WriteAllText(jsonFilePath, json); 

            return Content(json,"application/json");
        }
        
        public ActionResult GetAllTickets()
        {
            var jsonFilePath = HttpContext.Server.MapPath(PathToJson);

            if(System.IO.File.Exists(jsonFilePath))
            {
                var jsonModel = System.IO.File.ReadAllText(jsonFilePath);
                var deserialize = JsonConvert.DeserializeObject<TicketsJsonModel>(jsonModel);
                return Content(jsonModel, "application/json");
            }
            return Content("File do not exist", "application/json");
        }
    }
}