using CoreCommandEntities.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoreCommandEntities.SeedConfiguration
{
    public class CommandImageConfiguration : IEntityTypeConfiguration<CommandImage>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CommandImage> builder)
        {
            builder.HasData(
                new CommandImage {
                    CommandId = new Guid("aeca8962-8f62-46c8-83f8-8ef802b59bb1"),
                    Id = Guid.NewGuid(),
                    Url = "http://google.com",
                    OrderShown = 1
                }
            );
        }
    }
}