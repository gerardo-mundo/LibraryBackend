using AutoMapper;
using LibraryBackend.DTO.Books;
using LibraryBackend.DTO.Employees;
using LibraryBackend.DTO.Publications;
using LibraryBackend.DTO.Users;
using LibraryBackend.DTO.Thesis;
using LibraryBackend.Entities;
using LibraryBackend.DTO.Loans;
using LibraryBackend.Migrations;
using LibraryBackend.DTO.BorrowedBooks;

namespace LibraryBackend.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<BookCreationDTO, Book>().ReverseMap();
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

            CreateMap<LoanCreationDTO, Loan>()
            .ForMember(loan => loan.BorrowedBooks,
                options => options.MapFrom(MapBorrowedBooks));
            CreateMap<Loan, LoanDTO>();

            CreateMap<BorrowedBookCreationDTO, BorrowedBooks>();
            CreateMap<BorrowedBooks, BorrowedBookDTO>();
        }

        private List<BorrowedBooks> MapBorrowedBooks(LoanCreationDTO loanCreationDTO, Loan loan)
        {
            var result = new List<BorrowedBooks>();

            if (result == null) { return result; }

            foreach (var adquisition in loanCreationDTO.BorrowedBooks)
            {
                result.Add(new BorrowedBooks() { Adquisition = adquisition });
            }

            return result;
        }
    }
}
