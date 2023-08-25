using AutoMapper;
using LibraryBackend.DTO.Books;
using LibraryBackend.DTO.Publications;
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

        }
    }
}
