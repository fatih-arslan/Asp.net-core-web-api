using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Config
{
    internal class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book { Id = 1, Title = "Lord of The Rings", Price = 100 },
                new Book { Id = 2, Title = "Das Schloss", Price = 75 },
                new Book { Id = 3, Title = "Grapes of Wrath", Price = 75 }
            );
        }
    }
}
