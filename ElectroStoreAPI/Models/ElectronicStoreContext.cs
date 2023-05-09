using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ElectroStoreAPI.Models
{
    public partial class ElectronicStoreContext : DbContext
    {
        public ElectronicStoreContext()
        {
        }

        public ElectronicStoreContext(DbContextOptions<ElectronicStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<Client?> Clients { get; set; } = null!;
        public virtual DbSet<ClientOrder> ClientOrders { get; set; } = null!;
        public virtual DbSet<ClientPromocode> ClientPromocodes { get; set; } = null!;
        public virtual DbSet<DefectInformation> DefectInformations { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeeAddress> EmployeeAddresses { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<GeneratePromocode> GeneratePromocodes { get; set; } = null!;
        public virtual DbSet<LoyaltyCard> LoyaltyCards { get; set; } = null!;
        public virtual DbSet<Nomenclature> Nomenclatures { get; set; } = null!;
        public virtual DbSet<NomenclatureOrder> NomenclatureOrders { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderHistory> OrderHistories { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<ProductСategory> ProductСategories { get; set; } = null!;
        public virtual DbSet<Profile> Profiles { get; set; } = null!;
        public virtual DbSet<Promocode> Promocodes { get; set; } = null!;
        public virtual DbSet<Stock> Stocks { get; set; } = null!;
        public virtual DbSet<StoreAddress> StoreAddresses { get; set; } = null!;
        public virtual DbSet<Supply> Supplies { get; set; } = null!;
        public virtual DbSet<VendorType> VendorTypes { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=PC\\SQLEXPRESS;Initial Catalog=Electronic Store;User ID=sa;Password=123");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasKey(e => e.IdBrands);

                entity.Property(e => e.IdBrands).HasColumnName("ID_Brands");

                entity.Property(e => e.NameBrands)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Brands");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient);

                entity.ToTable("Client");

                entity.HasIndex(e => e.LoginClient, "UQ__Client__8D3632005338825E")
                    .IsUnique();

                entity.Property(e => e.IdClient).HasColumnName("ID_Client");

                entity.Property(e => e.EmailClient)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Email_Client");

                entity.Property(e => e.LoginClient)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Login_Client");

                entity.Property(e => e.LoyaltyCardId).HasColumnName("LoyaltyCard_ID");

                entity.Property(e => e.PasswordClient)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Password_Client");

                entity.Property(e => e.PhoneClient)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Phone_Client");

                entity.Property(e => e.SaltClient)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("Salt_Client");

                //entity.HasOne(d => d.LoyaltyCard)
                //    .WithMany(p => p.Clients)
                //    .HasForeignKey(d => d.LoyaltyCardId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Client_LoyaltyCard");
            });

            modelBuilder.Entity<ClientOrder>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ClientOrder");

                entity.Property(e => e.ЛогинКлиента)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Логин клиента");

                entity.Property(e => e.НомерЗаказа)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Номер заказа");

                entity.Property(e => e.Товар)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClientPromocode>(entity =>
            {
                entity.HasKey(e => e.IdClientPromocode);

                entity.ToTable("ClientPromocode");

                entity.Property(e => e.IdClientPromocode).HasColumnName("ID_ClientPromocode");

                entity.Property(e => e.ClientId).HasColumnName("Client_ID");

                entity.Property(e => e.PromocodeId).HasColumnName("Promocode_ID");

                //entity.HasOne(d => d.Client)
                //    .WithMany(p => p.ClientPromocodes)
                //    .HasForeignKey(d => d.ClientId)
                //    .HasConstraintName("FK_ClientPromocode_Client");

                //entity.HasOne(d => d.Promocode)
                //    .WithMany(p => p.ClientPromocodes)
                //    .HasForeignKey(d => d.PromocodeId)
                //    .HasConstraintName("FK_ClientPromocode_Promocode");
            });

            modelBuilder.Entity<DefectInformation>(entity =>
            {
                entity.HasKey(e => e.IdDefectInformation);

                entity.ToTable("DefectInformation");

                entity.Property(e => e.IdDefectInformation).HasColumnName("ID_DefectInformation");

                entity.Property(e => e.DescriptionDefectInformation)
                    .IsUnicode(false)
                    .HasColumnName("Description_DefectInformation");

                entity.Property(e => e.OrderId).HasColumnName("Order_ID");

                //entity.HasOne(d => d.Order)
                //    .WithMany(p => p.DefectInformations)
                //    .HasForeignKey(d => d.OrderId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_DefectInformation_Order");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdEmployee);

                entity.ToTable("Employee");

                entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");

                entity.Property(e => e.LoginEmployee)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("Login_Employee");

                entity.Property(e => e.MiddleNameEmployee)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MiddleName_Employee");

                entity.Property(e => e.NameEmployee)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Employee");

                entity.Property(e => e.PasswordEmployee)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("Password_Employee");

                entity.Property(e => e.PostId).HasColumnName("Post_ID");

                entity.Property(e => e.SaltEmployee)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("Salt_Employee");

                entity.Property(e => e.SurnameEmployee)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Surname_Employee");

                //entity.HasOne(d => d.Post)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.PostId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Employee_Post");
            });

            modelBuilder.Entity<EmployeeAddress>(entity =>
            {
                entity.HasKey(e => e.IdEmployeeAddresses);

                entity.Property(e => e.IdEmployeeAddresses).HasColumnName("ID_EmployeeAddresses");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.Property(e => e.StoreAddressesId).HasColumnName("StoreAddresses_ID");

                //entity.HasOne(d => d.Employee)
                //    .WithMany(p => p.EmployeeAddresses)
                //    .HasForeignKey(d => d.EmployeeId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_EmployeeAddresses_Employee");

                //entity.HasOne(d => d.StoreAddresses)
                //    .WithMany(p => p.EmployeeAddresses)
                //    .HasForeignKey(d => d.StoreAddressesId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_EmployeeAddresses_StoreAddresses");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.IdFeedback);

                entity.ToTable("Feedback");

                entity.Property(e => e.IdFeedback).HasColumnName("ID_Feedback");

                entity.Property(e => e.CommentFeedback)
                    .IsUnicode(false)
                    .HasColumnName("Comment_Feedback");

                entity.Property(e => e.MarkFeedback).HasColumnName("Mark_Feedback");

                entity.Property(e => e.OrderId).HasColumnName("Order_ID");

                entity.Property(e => e.PublicationDateFeedback)
                    .HasColumnType("date")
                    .HasColumnName("PublicationDate_Feedback");

                //entity.HasOne(d => d.Order)
                //    .WithMany(p => p.Feedbacks)
                //    .HasForeignKey(d => d.OrderId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Feedback_Order");
            });

            modelBuilder.Entity<GeneratePromocode>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GeneratePromocode");

                entity.Property(e => e.DateOfCompletion).HasColumnType("date");

                entity.Property(e => e.DateOfCreation).HasColumnType("date");

                entity.Property(e => e.PromocodeValue)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LoyaltyCard>(entity =>
            {
                entity.HasKey(e => e.IdLoyaltyCard);

                entity.ToTable("LoyaltyCard");

                entity.Property(e => e.IdLoyaltyCard).HasColumnName("ID_LoyaltyCard");

                entity.Property(e => e.DiscountValueLoyaltyCard).HasColumnName("DiscountValue_LoyaltyCard");

                entity.Property(e => e.NameLoyaltyCard)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_LoyaltyCard");
            });

            modelBuilder.Entity<Nomenclature>(entity =>
            {
                entity.HasKey(e => e.IdNomenclature);

                entity.ToTable("Nomenclature");

                entity.Property(e => e.IdNomenclature).HasColumnName("ID_Nomenclature");

                entity.Property(e => e.BrandsId).HasColumnName("Brands_ID");

                entity.Property(e => e.DescriptionNomenclature)
                    .IsUnicode(false)
                    .HasColumnName("Description_Nomenclature");

                entity.Property(e => e.NameNomenclature)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Nomenclature");

                entity.Property(e => e.ProductСategoriesId).HasColumnName("ProductСategories_ID");

                entity.Property(e => e.SuppliesId).HasColumnName("Supplies_ID");

                entity.Property(e => e.UnitСostNomenclature)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("UnitСost_Nomenclature");

                //entity.HasOne(d => d.Brands)
                //    .WithMany(p => p.Nomenclatures)
                //    .HasForeignKey(d => d.BrandsId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Nomenclature_Brands");

                //entity.HasOne(d => d.ProductСategories)
                //    .WithMany(p => p.Nomenclatures)
                //    .HasForeignKey(d => d.ProductСategoriesId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Nomenclature_ProductСategories");

                //entity.HasOne(d => d.Supplies)
                //    .WithMany(p => p.Nomenclatures)
                //    .HasForeignKey(d => d.SuppliesId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Nomenclature_Supplies");
            });

            modelBuilder.Entity<NomenclatureOrder>(entity =>
            {
                entity.HasKey(e => e.IdNomenclatureOrder);

                entity.ToTable("NomenclatureOrder");

                entity.Property(e => e.IdNomenclatureOrder).HasColumnName("ID_NomenclatureOrder");

                entity.Property(e => e.NomenclatureId).HasColumnName("Nomenclature_ID");

                entity.Property(e => e.OrderId).HasColumnName("Order_ID");

                //entity.HasOne(d => d.Nomenclature)
                //    .WithMany(p => p.NomenclatureOrders)
                //    .HasForeignKey(d => d.NomenclatureId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_NomenclatureOrder_Nomenclature");

                //entity.HasOne(d => d.Order)
                //    .WithMany(p => p.NomenclatureOrders)
                //    .HasForeignKey(d => d.OrderId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_NomenclatureOrder_Order");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder);

                entity.ToTable("Order");

                entity.Property(e => e.IdOrder).HasColumnName("ID_Order");

                entity.Property(e => e.ClientId).HasColumnName("Client_ID");

                entity.Property(e => e.ClientPromocodeId).HasColumnName("ClientPromocode_ID");

                entity.Property(e => e.EmployeeAddressesId).HasColumnName("EmployeeAddresses_ID");

                entity.Property(e => e.NumOrder)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Num_Order");

                //entity.HasOne(d => d.Client)
                //    .WithMany(p => p.Orders)
                //    .HasForeignKey(d => d.ClientId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Order_Client");

                //entity.HasOne(d => d.ClientPromocode)
                //    .WithMany(p => p.Orders)
                //    .HasForeignKey(d => d.ClientPromocodeId)
                //    .HasConstraintName("FK_Order_ClientPromocode");

                //entity.HasOne(d => d.EmployeeAddresses)
                //    .WithMany(p => p.Orders)
                //    .HasForeignKey(d => d.EmployeeAddressesId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Order_EmployeeAddresses");
            });

            modelBuilder.Entity<OrderHistory>(entity =>
            {
                entity.HasKey(e => e.IdOrderHistory);

                entity.ToTable("OrderHistory");

                entity.Property(e => e.IdOrderHistory).HasColumnName("ID_OrderHistory");

                entity.Property(e => e.OrderId).HasColumnName("Order_ID");

                //entity.HasOne(d => d.Order)
                //    .WithMany(p => p.OrderHistories)
                //    .HasForeignKey(d => d.OrderId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_OrderHistory_Order");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.IdPost);

                entity.ToTable("Post");

                entity.Property(e => e.IdPost).HasColumnName("ID_Post");

                entity.Property(e => e.NamePost)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Post");
            });

            modelBuilder.Entity<ProductСategory>(entity =>
            {
                entity.HasKey(e => e.IdProductСategories);

                entity.Property(e => e.IdProductСategories).HasColumnName("ID_ProductСategories");

                entity.Property(e => e.DescriptionProductСategories)
                    .IsUnicode(false)
                    .HasColumnName("Description_ProductСategories");

                entity.Property(e => e.NameProductСategories)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_ProductСategories");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.HasKey(e => e.IdProfile);

                entity.ToTable("Profile");

                entity.Property(e => e.IdProfile).HasColumnName("ID_Profile");

                entity.Property(e => e.ClientId).HasColumnName("Client_ID");

                entity.Property(e => e.MiddleNameProfile)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MiddleName_Profile");

                entity.Property(e => e.NameProfile)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Profile");

                entity.Property(e => e.SurnameProfile)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Surname_Profile");

                //entity.HasOne(d => d.Client)
                //    .WithMany(p => p.Profiles)
                //    .HasForeignKey(d => d.ClientId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Profile_Client");
            });

            modelBuilder.Entity<Promocode>(entity =>
            {
                entity.HasKey(e => e.IdPromocode);

                entity.ToTable("Promocode");

                entity.Property(e => e.IdPromocode).HasColumnName("ID_Promocode");

                entity.Property(e => e.DateOfCompletionPromocode)
                    .HasColumnType("date")
                    .HasColumnName("DateOfCompletion_Promocode");

                entity.Property(e => e.DateOfCreationPromocode)
                    .HasColumnType("date")
                    .HasColumnName("DateOfCreation_Promocode");

                entity.Property(e => e.DiscountValuePromocode).HasColumnName("DiscountValue_Promocode");

                entity.Property(e => e.RequiredAmountPromocode)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("RequiredAmount_Promocode");

                entity.Property(e => e.ValuePromocode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Value_Promocode");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => e.IdStock);

                entity.ToTable("Stock");

                entity.Property(e => e.IdStock).HasColumnName("ID_Stock");

                entity.Property(e => e.CountStock).HasColumnName("Count_Stock");

                entity.Property(e => e.NomenclatureId).HasColumnName("Nomenclature_ID");

                //entity.HasOne(d => d.Nomenclature)
                //    .WithMany(p => p.Stocks)
                //    .HasForeignKey(d => d.NomenclatureId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Stock_Nomenclature");
            });

            modelBuilder.Entity<StoreAddress>(entity =>
            {
                entity.HasKey(e => e.IdStoreAddresses);

                entity.Property(e => e.IdStoreAddresses).HasColumnName("ID_StoreAddresses");

                entity.Property(e => e.Postcode)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.StoreAddres).IsUnicode(false);
            });

            modelBuilder.Entity<Supply>(entity =>
            {
                entity.HasKey(e => e.IdSupplies);

                entity.Property(e => e.IdSupplies).HasColumnName("ID_Supplies");

                entity.Property(e => e.EmailSupplies)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Email_Supplies");

                entity.Property(e => e.Inn)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("INN");

                entity.Property(e => e.NameSupplies)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Supplies");

                entity.Property(e => e.PhoneSupplies)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("Phone_Supplies");

                entity.Property(e => e.VendorTypeId).HasColumnName("VendorType_ID");

                //entity.HasOne(d => d.VendorType)
                //    .WithMany(p => p.Supplies)
                //    .HasForeignKey(d => d.VendorTypeId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Supplies_VendorType");
            });

            modelBuilder.Entity<VendorType>(entity =>
            {
                entity.HasKey(e => e.IdPost);

                entity.ToTable("VendorType");

                entity.Property(e => e.IdPost).HasColumnName("ID_Post");

                entity.Property(e => e.PostName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Post_Name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
