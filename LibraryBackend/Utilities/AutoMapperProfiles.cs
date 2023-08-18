using AutoMapper;
using LibraryBackend.DTO.Books;
using LibraryBackend.Entities;

namespace LibraryBackend.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<BookCreationDTO, Book>();
            CreateMap<Book, BookDTO>();
        }
    }
}
