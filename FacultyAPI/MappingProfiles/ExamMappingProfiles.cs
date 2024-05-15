using AutoMapper;
using FacultyApp.Model;
using FacultyApp.Dto;
using FacultyApp.Enums;

public class CourseMappingProfile : Profile 
{
    public CourseMappingProfile() 
    {
      CreateMap<Course, CourseDto>()
        .ForMember(dest =>
          dest.Teacher,
          opt => opt.MapFrom(src => src.Teacher.FirstName + " " + src.Teacher.LastName)

        );
    }
}

public class GetExaminationMappingProfile : Profile 
{
  public GetExaminationMappingProfile(){
    CreateMap<Examination, ExaminationDto>()
      .ForMember(dest =>
        dest.TeacherName,
        opt => opt.MapFrom(src => src.Teacher.FirstName + " " + src.Teacher.LastName)
      );
  }
}

public class CreateExaminationMappingProfile : Profile 
{
  public CreateExaminationMappingProfile() {
    CreateMap<NewExaminationDto, Examination>()  
      .ForMember(dest => 
        dest.ScheduledFor,
        opt => opt.MapFrom(src => src.ScheduledFor.ToUniversalTime())
      )
      .ForMember(dest =>
        dest.Status,
        opt => opt.MapFrom(_ => ExaminationStatus.SCHEDULED)
    );
  }
}
