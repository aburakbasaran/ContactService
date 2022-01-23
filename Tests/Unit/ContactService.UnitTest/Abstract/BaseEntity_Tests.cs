using System;
using System.Threading;
using System.Threading.Tasks;
using ContactService.Application.Abstract;
using ContactService.Application.Exception;
using ContactService.Application.Interface;
using FluentAssertions;
using Moq;
using Xunit;

namespace ContactService.UnitTest.Abstract
{
    public class BaseEntity_Tests
    {
        [Fact]
        public async Task CheckRule_IsNotBroken()
        {
            Mock<IBusinessRule> bussinessRule = new();
            bussinessRule.Setup(c => c.IsBroken(CancellationToken.None)).ReturnsAsync(false);

            Func<Task> bussinessRuleF = async () => { await BaseEntity.CheckRule(bussinessRule.Object, CancellationToken.None); };
            await bussinessRuleF.Should().NotThrowAsync();

            Func<Task> checkRuleActionF = async () => { await BaseEntity.CheckRule(bussinessRule.Object, CancellationToken.None); };
            await checkRuleActionF.Should().NotThrowAsync<BusinessRuleValidationException>();

            bussinessRule.Verify(c => c.IsBroken(CancellationToken.None), Times.Exactly(2));
        }

        [Fact]
        public async Task CheckRule_IsBroken_ThrowBusinessRuleValidationException()
        {
            Mock<IBusinessRule> bussinessRule = new();
            bussinessRule.Setup(c => c.IsBroken(CancellationToken.None)).ReturnsAsync(true);

            Func<Task> checkRuleActionF = async () => { await BaseEntity.CheckRule(bussinessRule.Object, CancellationToken.None); };
            await checkRuleActionF.Should().ThrowAsync<BusinessRuleValidationException>();

            bussinessRule.Verify(c => c.IsBroken(CancellationToken.None), Times.Once);
        }
    }
}
