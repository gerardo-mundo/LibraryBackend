using AutoMapper;
using LibraryBackend.DTO.Books;
using LibraryBackend.DTO.Employees;
using LibraryBackend.DTO.Publications;
using LibraryBackend.DTO.Users;
using LibraryBackend.DTO.Thesis;
using LibraryBackend.Entities;

namespace LibraryBackend.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<BookCreationDTO, Book>();
            CreateMap<Book, BookDTO>();

            CreateMap<PublicationCreationDTO, Publication>();
            CreateMap<Publication, PublicationDTO>();
            CreateMap<PublicationPatchDTO, Publication>().ReverseMap();

            CreateMap<UserCreationDTO, User>();
            CreateMap<UserPatchDTO, User>().ReverseMap();
            CreateMap<User, StudentDTO>();
            CreateMap<User, ProfessorDTO>();
            CreateMap<User, AdministrativeDTO>();

            CreateMap<ThesisCreationDTO, Thesis>();
            CreateMap<Thesis, ThesisDTO>();
            CreateMap<ThesisPatchDTO, Thesis>().ReverseMap();

            CreateMap<EmployeeCreationDTO, Employee>();
            CreateMap<Employee, EmployeeDTO>();
        }
    }
}
