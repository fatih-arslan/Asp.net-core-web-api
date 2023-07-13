using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record BookDtoForUpdate
    {
        public BookDtoForUpdate(int id, string title, decimal price)
        {
            Id = id;
            Title = title;
            Price = price;
        }
        public int Id { get; init; }
        public string Title { get; init; }
        public decimal Price { get; init; }
    }
}
