using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Obras.Data.Entities;
using System;
namespace Obras.Data.EntitiesConfiguration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.TypePeople).IsRequired();
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Cpf).HasMaxLength(14);
            builder.Property(p => p.Cnpj).HasMaxLength(18);
            builder.Property(p => p.Outsourced);
            builder.Property(p => p.Employed);
            builder.Property(p => p.ZipCode).HasMaxLength(10);
            builder.Property(p => p.Address).HasMaxLength(100);
            builder.Property(p => p.Number).HasMaxLength(15);
            builder.Property(p => p.Neighbourhood).HasMaxLength(100);
            builder.Property(p => p.City).HasMaxLength(50);
            builder.Property(p => p.State).HasMaxLength(2);
            builder.Property(p => p.Complement).HasMaxLength(100);
            builder.Property(p => p.Telephone).HasMaxLength(18);
            builder.Property(p => p.CellPhone).HasMaxLength(18);
            builder.Property(p => p.EMail).HasMaxLength(100);
            builder.Property(p => p.CompanyId).IsRequired();
            builder.Property(p => p.ResponsibilityId).IsRequired();
            builder.Property(p => p.Active).IsRequired();
            builder.Property(p => p.ChangeDate).IsRequired();
            builder.Property(p => p.CreationDate).IsRequired();

            builder.HasOne(e => e.Responsibility).WithMany(e => e.Employees).HasForeignKey(e => e.ResponsibilityId);
            builder.HasOne(e => e.Company).WithMany(e => e.Employees).HasForeignKey(e => e.CompanyId);
            builder.HasOne(e => e.RegistrationUser).WithMany(e => e.RegistrationEmployees).HasForeignKey(e => e.RegistrationUserId);
            builder.HasOne(e => e.ChangeUser).WithMany(e => e.ChangeEmployees).HasForeignKey(e => e.ChangeUserId);
        }
    }
}