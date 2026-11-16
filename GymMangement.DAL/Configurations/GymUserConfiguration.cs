using GymMangement.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangement.DAL.Configurations
{
    internal class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(X => X.Name)
                   .HasColumnType("varchar")
                      .HasMaxLength(50);

            builder.Property(X => X.Email)
                   .HasColumnType("varchar")
                      .HasMaxLength(50);


            builder.HasIndex(X => X.Email)
                   .IsUnique();
            builder.HasIndex(X => X.Phone)
                   .IsUnique();

            builder.ToTable(Tb =>
            {
                Tb.HasCheckConstraint("EmailCheck", "Email LIKE '_%@_%._%'");
                Tb.HasCheckConstraint("PhoneCheck", "Phone LIKE '010%' or Phone LIKE '011%' or Phone LIKE '012%' or Phone LIKE '015%'");
            });

            builder.OwnsOne(X => X.Address, Address =>
            {
                Address.Property(X => X.City)
                .HasColumnType("varchar")
                .HasColumnName("City")
                .HasMaxLength(30);

                Address.Property(X => X.street)
                .HasColumnType("varchar")
                .HasColumnName("Street")
                .HasMaxLength(50);
            });


        }


    }
}

