using System;
using CoreCommandEntities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreCommandEntities.SeedConfiguration
{
    public class CommandConfiguration : IEntityTypeConfiguration<Command>
    {
        public void Configure(EntityTypeBuilder<Command> builder)
        {
            builder.HasData(
                new Command {
                     Id = new Guid("65c3c877-ceac-4d1d-918c-c78281fcd25f"),
                     SnippetDescription = "How to configure Git globally to use username?",
                     Platform = "git",
                     Snippet = "git config --global user.name 'devtestuser'"
                },
                 new Command {
                     Id = new Guid("86555480-a20b-41d0-b4f5-cb23419e120e"),
                     SnippetDescription = "How to configure Git globally to use user email?",
                     Platform = "git",
                     Snippet = "git config --global user.email 'devtestuser@test.com'"
                },
                new Command {
                     Id = new Guid("aeca8962-8f62-46c8-83f8-8ef802b59bb1"),
                     SnippetDescription = "How to update Entity Framework Core tool?",
                     Platform = "efcore",
                     Snippet = "dotnet tool update --global dotnet-ef"
                }

            );
        }
    }
}