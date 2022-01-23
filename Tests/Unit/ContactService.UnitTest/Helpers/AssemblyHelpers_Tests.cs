using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ContactService.Infrastructure.Helpers;
using FluentAssertions;
using Xunit;

namespace ContactService.UnitTest.Helpers
{
    // ReSharper disable once InconsistentNaming
    public class AssemblyHelpers_Tests
    {
        [Fact]
        public void LoadFromSearchPatterns_EmptyParam_ReturnEmpty()
        {
            IEnumerable<Assembly> assemblies = AssemblyHelpers.LoadFromSearchPatterns();
            assemblies.Should().BeEmpty();
        }

        [Theory]
        [InlineData("FluentAssertions.*")]
        [InlineData("Moq.*")]
        public void LoadFromSearchPatterns_RegisteredAssembly_ReturnSelectedAssembly(string assemblyName)
        {
            IEnumerable<Assembly> assemblies = AssemblyHelpers.LoadFromSearchPatterns(assemblyName);
            assemblies.Should().NotBeNull();
            assemblies.Should().HaveCountGreaterThan(0);
            assemblies.First().FullName.Should().MatchRegex(assemblyName);
        }
    }
}
