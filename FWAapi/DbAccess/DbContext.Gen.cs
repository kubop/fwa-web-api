using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpers = Toolkit.Common.Helpers;
using Yamo;
using FWAapi.Model;

namespace FWAapi.DbAccess
{
	public partial class DbContext
	{
		private void GeneratedOnModelCreating(ModelBuilder modelBuilder)
		{
			OnAddressModelCreating(modelBuilder);
			OnUserModelCreating(modelBuilder);
			OnUserViewModelCreating(modelBuilder);
		}

		private void OnAddressModelCreating(ModelBuilder modelBuilder)
		{
			var entity = modelBuilder.Entity<Address>().ToTable("tbl_Address");

			entity.Property(x => x.AddressId).HasColumnName("nAddressId").IsKey().IsIdentity();
			entity.Property(x => x.Street).HasColumnName("sStreet").IsRequired();
			entity.Property(x => x.Number).HasColumnName("nNumber");
			entity.Property(x => x.City).HasColumnName("sCity").IsRequired();
			entity.Property(x => x.ZipCode).HasColumnName("sZipCode").IsRequired();
		}

		private void OnUserModelCreating(ModelBuilder modelBuilder)
		{
			var entity = modelBuilder.Entity<User>().ToTable("tbl_User");

			entity.Property(x => x.UserId).HasColumnName("nUserId").IsKey().IsIdentity();
			entity.Property(x => x.FirstName).HasColumnName("sFirstName").IsRequired();
			entity.Property(x => x.LastName).HasColumnName("sLastName").IsRequired();
			entity.Property(x => x.Login).HasColumnName("sLogin").IsRequired();
			entity.Property(x => x.Password).HasColumnName("sPassword").IsRequired();
			entity.Property(x => x.AddressId).HasColumnName("nAddressId");
		}

		private void OnUserViewModelCreating(ModelBuilder modelBuilder)
		{
			var entity = modelBuilder.Entity<UserView>().ToTable("view_User");

			entity.Property(x => x.UserId).HasColumnName("nUserId");
			entity.Property(x => x.FirstName).HasColumnName("sFirstName").IsRequired();
			entity.Property(x => x.LastName).HasColumnName("sLastName").IsRequired();
			entity.Property(x => x.Login).HasColumnName("sLogin").IsRequired();
			entity.Property(x => x.Password).HasColumnName("sPassword").IsRequired();
			entity.Property(x => x.Street).HasColumnName("sStreet");
			entity.Property(x => x.Number).HasColumnName("nNumber");
			entity.Property(x => x.City).HasColumnName("sCity");
			entity.Property(x => x.ZipCode).HasColumnName("sZipCode");
		}
	}
}
