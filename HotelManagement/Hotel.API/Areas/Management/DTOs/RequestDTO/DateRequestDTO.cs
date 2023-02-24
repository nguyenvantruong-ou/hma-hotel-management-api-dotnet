using System.ComponentModel.DataAnnotations;

namespace Hotel.API.Areas.Management.DTOs.RequestDTO
{
    public class DateRequestDTO
    {
        [Range(typeof(DateTime), "1/1/2000", "1/1/2050")]
        public DateTime FromDate { get; set; }

        [Range(typeof(DateTime), "1/1/2000", "1/1/2050")]
        public DateTime ToDate { get; set; }
    }
}
