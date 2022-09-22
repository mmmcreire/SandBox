using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SandBox.Core.ToDos;

namespace SandBox.Infra.Database;

public class TodoSchema : IEntityTypeConfiguration<ToDo>
{
    public void Configure(EntityTypeBuilder<ToDo> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Description).HasColumnName("todo_description");
        builder.Property(e => e.Id).IsRequired();
        builder.Property(e => e.Description).HasColumnType("varchar(255)").IsRequired();
        builder.Property(e => e.Status).IsRequired();
    }
}
