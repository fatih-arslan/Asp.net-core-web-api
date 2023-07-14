using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public abstract record BookDtoForManipulation
    {
        [Required(ErrorMessage = "Title is a required field")]
        [MinLength(2, ErrorMessage = "The title must be at least 2 characters long")]
        [MaxLength(50, ErrorMessage = "The title must not exceed 50 characters in length")]
        public string Title { get; init; }

        [Required(ErrorMessage = "Price is a required field")]
        [Range(10, 1000)]
        public decimal  Price { get; init; }
    }
}
