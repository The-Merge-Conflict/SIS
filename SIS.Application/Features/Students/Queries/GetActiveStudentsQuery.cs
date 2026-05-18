using MediatR;
using SIS.Application.DTOs;

namespace SIS.Application.Features.Students.Queries
{
    // 1. THE REQUEST: What do we want? (It returns an IEnumerable<StudentListDto>)
    public class GetActiveStudentsQuery : IRequest<IEnumerable<StudentListDto>>
    {
        // If you needed to pass parameters (like a SearchTerm or PageNumber), 
        // you would put them here as properties.
    }


}
