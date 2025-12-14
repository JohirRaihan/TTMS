using Microsoft.EntityFrameworkCore;
using TTMS.Data.Context;
using TTMS.Data.Models;
using TTMS.Models.User.Enums;
using TTMS.Domains.Task.Handlers;
using TTMS.Models.Task.Dtos;
using TTMS.Domains.Task.Commands;
using TTMS.Domains.Task.Quries;

namespace TTMS.Tests.HandlersTest
{
    [TestClass]
    public class TaskHandlersTests
    {
        private TTMSContext _context = default!;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<TTMSContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new TTMSContext(options);

            // Seed Users and Teams
            var user = new DimUser { Id = Guid.NewGuid(), FullName = "Test User", Email = "user@test.com", Role = UserRole.Manager };
            var team = new DimTeam { Id = Guid.NewGuid(), Name = "Team A", Description = "Desc" };
            _context.Users.Add(user);
            _context.Teams.Add(team);
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task CreateTaskCommand_ShouldCreateTask()
        {
            var handler = new CreateTaskCommandHandler(_context);
            var user = _context.Users.First();
            var team = _context.Teams.First();

            var dto = new CreateTaskDto
            {
                Title = "Task 1",
                Description = "Desc",
                AssignedToUserId = user.Id,
                TeamId = team.Id,
                DueDate = DateTime.UtcNow.AddDays(2)
            };
            var command = new CreateTaskCommand(dto, user.Id);

            var result = await handler.Handle(command, CancellationToken.None);

            var task = await _context.Tasks.FindAsync(result);
            Assert.IsNotNull(task);
            Assert.AreEqual("Task 1", task!.Title);
        }

        [TestMethod]
        public async Task UpdateTaskCommand_EmployeeCanOnlyUpdateStatus()
        {
            var user = _context.Users.First();
            var team = _context.Teams.First();

            var task = new FactTask
            {
                Id = Guid.NewGuid(),
                Title = "Old Task",
                Description = "Old Desc",
                AssignedToUserId = user.Id,
                CreatedByUserId = user.Id,
                TeamId = team.Id,
                Status = TTMS.Models.Task.Enums.TaskStatus.Todo,
                DueDate = DateTime.UtcNow.AddDays(2)
            };
            _context.Tasks.Add(task);
            _context.SaveChanges();

            var handler = new UpdateTaskCommandHandler(_context);

            var updateDto = new UpdateTaskDto
            {
                Title = "New Task",
                Description = "New Desc",
                Status = Models.Task.Enums.TaskStatus.InProgress,
                AssignedToUserId = user.Id,
                DueDate = DateTime.UtcNow.AddDays(3),
                TeamId = team.Id
            };
            var command = new UpdateTaskCommand(task.Id, updateDto, user.Id, UserRole.Employee);

            var result = await handler.Handle(command, CancellationToken.None);

            var updatedTask = await _context.Tasks.FindAsync(task.Id);
            Assert.IsTrue(result);
            Assert.AreEqual(TTMS.Models.Task.Enums.TaskStatus.InProgress, updatedTask!.Status);
            Assert.AreEqual("Old Task", updatedTask.Title); // Employee cannot update title
        }

        [TestMethod]
        public async Task GetTasksQuery_ShouldFilterTasks()
        {
            var user = _context.Users.First();
            var team = _context.Teams.First();

            _context.Tasks.Add(new FactTask { Id = Guid.NewGuid(), Title = "Task1", Description = "Desc1", AssignedToUserId = user.Id, CreatedByUserId = user.Id, TeamId = team.Id, Status = TTMS.Models.Task.Enums.TaskStatus.Todo, DueDate = DateTime.UtcNow });
            _context.Tasks.Add(new FactTask { Id = Guid.NewGuid(), Title = "Task2", Description = "Desc2", AssignedToUserId = user.Id, CreatedByUserId = user.Id, TeamId = team.Id, Status = TTMS.Models.Task.Enums.TaskStatus.Todo, DueDate = DateTime.UtcNow });
            _context.SaveChanges();

            var handler = new GetTasksQueryHandler(_context);
            var query = new GetTasksQuery(Status: TTMS.Models.Task.Enums.TaskStatus.Todo, AssignedToUserId: null, TeamId: null, DueDate: null);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.HasCount(1, result);
            Assert.AreEqual("Task1", result[0].Title);
        }
    }
}