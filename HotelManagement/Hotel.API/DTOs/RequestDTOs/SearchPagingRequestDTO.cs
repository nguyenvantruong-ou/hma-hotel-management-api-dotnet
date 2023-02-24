using Hotel.API.DTOs.RequestDTOs.CustomValidationAttribute;

namespace Hotel.API.DTOs.RequestDTOs
{
    public class SearchPagingRequestDTO
    {
        public string? Kw { get; set; }
        public int Sort { get; set; }
        [IdValidationAttribute]
        public int Page { get; set; }
        [IdValidationAttribute]
        public int PageSize { get; set; }
    }
}
