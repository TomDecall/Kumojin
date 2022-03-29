using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Api.Controllers;
using ApiModels;
using Database;
using IService;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;

namespace Api.Test
{
    [TestClass]
    public class EventControllerTest
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<EventManagementContext> _contextOptions;
        public EventControllerTest()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            _contextOptions = new DbContextOptionsBuilder<EventManagementContext>()
                .UseSqlite(_connection)
                .Options;

            using var context = new EventManagementContext(_contextOptions);

            context.Database.EnsureCreated();

            context.AddRange(
                new Event { Id = 1, Name = "Event 1", Description = "Ceci est une description", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) },
                new Event { Id = 2, Name = "Event 2", Description = "Ceci est une description 2", StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(2) }
                );

            context.SaveChanges();
        }

        EventManagementContext CreateContext() => new(_contextOptions);


        [TestMethod]
        public async Task GetTest()
        {
            IEventService eventService = new EventService(CreateContext());
            EventController eventController = new(eventService);
            IEnumerable<EventViewModel> datas = await eventController.Get();
            Assert.AreEqual(datas.Count(), 2);
        }

        [TestMethod]
        public async Task PostTest()
        {
            IEventService eventService = new EventService(CreateContext());
            EventController eventController = new(eventService);
            EventViewModel eventViewModel = new()
            {
                Id = 0,
                Name = "Event 3",
                Description = "Description 3",
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(3)
            };

            await eventController.Post(eventViewModel);

            IEnumerable<EventViewModel> datas = await eventController.Get();
            Assert.AreEqual(datas.Count(), 3);
        }
    }
}