using AutoMapper;
using MediatR;
using SIS.Application.DTOs;
using SIS.Domain.Common.Interfaces;

namespace SIS.Application.Features.Students.Queries
{
    public class GetActiveStudentsQueryHandler : IRequestHandler<GetActiveStudentsQuery, IEnumerable<StudentListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // Inject exactly what this specific handler needs
        public GetActiveStudentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentListDto>> Handle(GetActiveStudentsQuery request, CancellationToken cancellationToken)
        {
            // 1. Get Data
            var students = await _unitOfWork.Students.GetAllAsync();

            // 2. Apply Business Logic
            var activeStudents = students.Where(s => s.IsActive);

            // 3. Map and Return
            return _mapper.Map<IEnumerable<StudentListDto>>(activeStudents);
        }
    }
}
