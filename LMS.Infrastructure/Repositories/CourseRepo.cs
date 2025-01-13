using Domain.Contracts;
using Domain.Models.Entites;

namespace LMS.Infrastructure.Repositories;

public class CourseRepo : RepositoryBase<Course>, ICourseRepo
{
    public CourseRepo(LmsContext context) : base(context)
    {
    }
}