using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using OfficesAPI.Domain.Models;

namespace OfficesAPI.Infrastructure.Database.Configuration;

public class OfficeConfiguration : IEntityTypeConfiguration<Office> {
    public void Configure(EntityTypeBuilder<Office> builder) {
        builder.ToCollection("offices");
    }
}
