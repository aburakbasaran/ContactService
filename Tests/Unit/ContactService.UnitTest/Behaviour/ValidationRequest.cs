using ContactService.Application.Model;
using MediatR;

namespace ContactService.UnitTest.Behaviour
{
    public class ValidationRequest : IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}