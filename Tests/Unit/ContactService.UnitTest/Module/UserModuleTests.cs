using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ContactService.ContactModule.Data.Data;
using ContactService.ContactModule.Data.Data.Entities;
using ContactService.ContactModule.Engine.User.CommandHandler;
using ContactService.ContactModule.Messages.User.Command;
using ContactService.ContactModule.Messages.User.Dto;
using ContactService.TestBase;
using FluentAssertions;
using Moq;
using Xunit;

namespace ContactService.UnitTest.Module
{
    public class UserModuleTests : IntegrationTestFixture
    {
        private readonly IContactDbContext _context;

        public UserModuleTests()
        {
            Mock<IContactDbContext> dbContextMock = new();

            var userEntityDbSet = new List<UserEntity>
            {
                new() { Id = Guid.NewGuid(), Name = "burak", SurName = "Basaran", Firm = "Odeon" }
            }.AsQueryable().BuildMockDbSet();

            dbContextMock.Setup(c => c.Users).Returns(userEntityDbSet.Object);
            dbContextMock.Setup(c => c.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(userEntityDbSet.Object.Count());

            _context = dbContextMock.Object;
        }

        [Fact]
        public async Task CreateUserCommandHandler_Success()
        {
            CreateUserCommand command = new()
            {
                CreateUserDto = new CreateUserDto()
                {
                    Firm = "Odeon",
                    Name = "Burakk",
                    SurName = "Basaran"
                }

            };

            CreateUserCommandHandler handler = new(_context);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Data.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteUserCommandHandler_Success()
        {
            DeleteUserCommand command = new()
            {
                Name = "burak"

            };

            DeleteUserCommandHandler handler = new(_context);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Data.Should().BeTrue();
        }


        [Fact]
        public async Task GetUsersQueryHandler_Success()
        {
            GetUsersQuery query = new();

            UserListQueryHandler handler = new(_context);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Data.Should().NotBeNullOrEmpty();
        }


    }
}
