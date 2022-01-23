using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ContactService.ContactModule.Data.Data;
using ContactService.ContactModule.Data.Data.Entities;
using ContactService.ContactModule.Engine.User.CommandHandler;
using ContactService.ContactModule.Engine.UserContact.QueryHandler;
using ContactService.ContactModule.Messages.Enum;
using ContactService.ContactModule.Messages.User.Command;
using ContactService.ContactModule.Messages.User.Dto;
using ContactService.ContactModule.Messages.UserContact;
using ContactService.ContactModule.Messages.UserContact.Dto;
using ContactService.TestBase;
using FluentAssertions;
using Moq;
using Xunit;

namespace ContactService.UnitTest.Module
{
    public class ContactModuleTests : IntegrationTestFixture
    {
        private readonly IContactDbContext _context;

        public ContactModuleTests()
        {
            Mock<IContactDbContext> dbContextMock = new();

            var userContactEntityDbSet = new List<UserContactEntity>
            {
                new() { Id = Guid.NewGuid(), Type = (byte)ContactTypeEnum.Location.GetHashCode(), Value = "ankara", UserId = Guid.NewGuid() }
            }.AsQueryable().BuildMockDbSet();

            dbContextMock.Setup(c => c.UserContacts).Returns(userContactEntityDbSet.Object);
            dbContextMock.Setup(c => c.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(userContactEntityDbSet.Object.Count());

            var userEntityDbSet = new List<UserEntity>
            {
                new() { Id = Guid.NewGuid(), Name = "burak", SurName = "Basaran", Firm = "Odeon" }
            }.AsQueryable().BuildMockDbSet();

            dbContextMock.Setup(c => c.Users).Returns(userEntityDbSet.Object);
            dbContextMock.Setup(c => c.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(userEntityDbSet.Object.Count());

            _context = dbContextMock.Object;
        }

        [Fact]
        public async Task CreateUserContactCommandHandler_Success()
        {
            AddUserContactCommand command = new()
            {
                AddUserContactDto = new AddUserContactDto()
                {
                    ContactType = ContactTypeEnum.Location,
                    Value = "ankara",
                    UserName = "Burak",
                }

            };

            AddUserContactCommandHandler handler = new(_context);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Data.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteUserContactCommandHandler_Success()
        {
            DeleteUserContactCommand command = new()
            {
               ContactType = ContactTypeEnum.Location
            };

            DeleteUserContactCommandHandler handler = new(_context);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Data.Should().BeTrue();
        }
        
        [Fact]
        public async Task GetUserContactLocationQueryHandler_Success()
        {
            GetUsersLocationQuery query = new();

            UsersLocationReportQueryHandler handler = new(_context);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Data.Should().NotBeNullOrEmpty();
        }


    }
}
