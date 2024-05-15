using AutoMapper;
using FacultyApp.Model;
using FacultyApp.Dto;
using FacultyApp.Utils;

public class CreateStudentMappingProfile : Profile {
  public CreateStudentMappingProfile(){
    CreateMap<RegisterStudentDto, Student>()
      .ForMember(dest =>
        dest.DateOfBirth,
        opt => opt.MapFrom(src => src.DateOfBirth.Date)
      )
      .ForMember(dest =>
        dest.Password,
        opt => opt.MapFrom(src => PasswordHasher.HashPassword(src.Password)) 
      )
      .ForMember(dest =>
        dest.CurrentYear,
        opt => opt.MapFrom(_ => 1)
      )
      .ForMember(dest =>
        dest.Graduated,
        opt => opt.MapFrom(_ => false)
      )
      .ForMember(dest =>
        dest.EnrolledIn,
        opt => opt.MapFrom(_ => (uint)DateTime.Now.Year)
      )
      .ForMember(dest =>
        dest.GPA,
        opt => opt.MapFrom(_ => 0.0f)
      );
  }
}

public class CreateTeacherMappingProfile : Profile {
  public CreateTeacherMappingProfile(){
    CreateMap<RegisterTeacherDto, Teacher>()
      .ForMember(dest =>
      dest.DateOfBirth,
      opt => opt.MapFrom(src => src.DateOfBirth.Date)
    )
    .ForMember(dest =>
      dest.Password,
      opt => opt.MapFrom(src => PasswordHasher.HashPassword(src.Password)) 
    )
    .ForMember(dest =>
        dest.EmployedIn,
        opt => opt.MapFrom(_ => (uint)DateTime.Now.Year)
    );
  }

}