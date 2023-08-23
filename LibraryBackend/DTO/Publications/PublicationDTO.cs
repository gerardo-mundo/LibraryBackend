using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.DTO.Publications
{
    public class PublicationDTO
    {
        public int Id { get; set; }
        public PublicationTypes Type { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string? AuthorTwo { get; set; }
        public string? AuthorThree { get; set; }
        public string? AuthorFour { get; set; }
        public string Publisher { get; set; } = null!;
        public long? ISBN { get; set; }
        public int? ISSN { get; set; }
        public int Year { get; set; }
        public string Vol { get; set; } = null!;
    }
}
