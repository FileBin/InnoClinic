using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using OfficesAPI.Domain.Models;

namespace OfficesAPI.Infrastructure.Database.Configuration;

internal class OfficeConfiguration : IEntityTypeConfiguration<Office> {
    public void Configure(EntityTypeBuilder<Office> builder) {
        builder.ToCollection("offices");
    }
}
