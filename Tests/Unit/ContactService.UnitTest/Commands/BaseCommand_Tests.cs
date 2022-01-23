using System;
using ContactService.Application.Commmand;
using FluentAssertions;
using Xunit;

namespace ContactService.UnitTest.Commands
{
    // ReSharper disable once InconsistentNaming
    public class BaseCommand_Tests
    {
        [Fact]
        public void GenericCommand_Constructor_OK()
        {
            Func<ICommand<TestGenericType>> ctorAction = () => new TestBaseCommandGeneric();
            ctorAction.Should().NotThrow();
        }
    }
}
