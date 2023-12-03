using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Configuration 
{
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();  

            builder
                .HasOne(s => s.Class)
                .WithMany(c => c.Staff)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(s => s.Account)
                .WithMany(a => a.Staff)
                .HasForeignKey(s => s.AccountId)
                .OnDelete(DeleteBehavior.Cascade);


            
        }
    }
}