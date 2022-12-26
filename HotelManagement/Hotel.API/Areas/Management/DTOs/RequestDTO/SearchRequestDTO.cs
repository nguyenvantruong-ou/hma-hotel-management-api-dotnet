﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Hotel.API.Areas.Management.DTOs.RequestDTO
{
    public class SearchRequestDTO
    {
        [ValidateNever]
        public string Kw { get; set; } = null!;
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
