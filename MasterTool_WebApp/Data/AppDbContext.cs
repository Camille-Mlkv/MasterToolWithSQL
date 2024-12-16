using MasterTool_WebApp.Models;
using MasterTool_WebApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MasterTool_WebApp.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Service> Services { get; set; }

        //public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<PickupPoint> PickupPoints { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
       // public DbSet<PaymentInfo> PaymentInfos { get; set; }
       public DbSet<OrderDetail> OrderDetailss { get; set; }
       public DbSet<Feedback> Feedbacks { get; set; } 
        public DbSet<ClientWithInfoViewModel> ClientsWithInfo { get; set; }
        public DbSet<MasterWithInfoViewModel> MasterWithInfo { get; set; }
        public DbSet<FeedbackWithClientNameViewModel> FeedbacksWithInfo { get; set; }
        public DbSet<OrderDetailsViewModel> OrderDetails { get; set; }
        public DbSet<PayOrderViewModel> PayOrderModels { get; set; }
        public DbSet<PaymentInfoViewModel> PaymentInfoViewModels { get; set; }
        public DbSet<OrderDetailViewModel> OrderDetailViewModels { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClientWithInfoViewModel>(entity =>
            {
                entity.HasNoKey(); // Указывает, что эта модель не имеет ключа
            });

            modelBuilder.Entity<MasterWithInfoViewModel>(entity =>
            {
                entity.HasNoKey(); 
            });

            modelBuilder.Entity<FeedbackWithClientNameViewModel>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<OrderDetailsViewModel>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<PayOrderViewModel>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<PaymentInfoViewModel>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<OrderDetailViewModel>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasNoKey();
            });

            //modelBuilder.Entity<Role>().ToTable("roles");
        }
    }
}
