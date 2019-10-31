using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VirtualGameStore.Models
{
    public partial class PROG3050Context : DbContext
    {
        public PROG3050Context()
        {
        }

        public PROG3050Context(DbContextOptions<PROG3050Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Creditcard> Creditcard { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Eventgame> Eventgame { get; set; }
        public virtual DbSet<Favorite> Favorite { get; set; }
        public virtual DbSet<Friends> Friends { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Gamerates> Gamerates { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Platform> Platform { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Wishlist> Wishlist { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=PROG3050;Trusted_Connection=True;User ID = sa; Password=Conestoga1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Categoryid)
                    .HasColumnName("categoryid")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Categoriname)
                    .HasColumnName("categoriname")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedUserid)
                    .HasColumnName("created_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUserid)
                    .HasColumnName("updated_userid")
                    .HasColumnType("numeric(18, 0)");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.Property(e => e.Companyid)
                    .HasColumnName("companyid")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(100);

                entity.Property(e => e.Address2)
                    .HasColumnName("address2")
                    .HasMaxLength(100);

                entity.Property(e => e.CeoName)
                    .IsRequired()
                    .HasColumnName("ceo_name")
                    .HasMaxLength(150);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(100);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasColumnName("company_name")
                    .HasMaxLength(150);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedUserid)
                    .HasColumnName("created_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.OfficePhone)
                    .IsRequired()
                    .HasColumnName("office_phone")
                    .HasMaxLength(20);

                entity.Property(e => e.OfficePhone1)
                    .HasColumnName("office_phone1")
                    .HasMaxLength(20);

                entity.Property(e => e.OfficePhone2)
                    .HasColumnName("office_phone2")
                    .HasMaxLength(20);

                entity.Property(e => e.PostCode)
                    .HasColumnName("post_code")
                    .HasMaxLength(10);

                entity.Property(e => e.Province)
                    .HasColumnName("province")
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUserid)
                    .HasColumnName("updated_userid")
                    .HasColumnType("numeric(18, 0)");
            });

            modelBuilder.Entity<Creditcard>(entity =>
            {
                entity.HasKey(e => e.Cardid);

                entity.ToTable("creditcard");

                entity.Property(e => e.Cardid)
                    .HasColumnName("cardid")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Cardholder)
                    .HasColumnName("cardholder")
                    .HasMaxLength(50);

                entity.Property(e => e.Cardnumber)
                    .HasColumnName("cardnumber")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedUserid)
                    .HasColumnName("created_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUserid)
                    .HasColumnName("updated_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Creditcard)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_creditcard_user");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("event");

                entity.Property(e => e.Eventid)
                    .HasColumnName("eventid")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedUserid)
                    .HasColumnName("created_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Enddate)
                    .HasColumnName("enddate")
                    .HasColumnType("date");

                entity.Property(e => e.Eventname)
                    .HasColumnName("eventname")
                    .HasMaxLength(100);

                entity.Property(e => e.RegisterUserid)
                    .HasColumnName("register_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Startdate)
                    .HasColumnName("startdate")
                    .HasColumnType("date");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUserid)
                    .HasColumnName("updated_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.RegisterUser)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.RegisterUserid)
                    .HasConstraintName("FK_event_user");
            });

            modelBuilder.Entity<Eventgame>(entity =>
            {
                entity.ToTable("eventgame");

                entity.Property(e => e.Eventgameid)
                    .HasColumnName("eventgameid")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedUserid)
                    .HasColumnName("created_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.DiscountRate)
                    .HasColumnName("discount_rate")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Eventid)
                    .HasColumnName("eventid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Gameid)
                    .HasColumnName("gameid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUserid)
                    .HasColumnName("updated_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Eventgame)
                    .HasForeignKey(d => d.Eventid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_eventgame_event");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Eventgame)
                    .HasForeignKey(d => d.Gameid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_eventgame_game");
            });

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.HasKey(e => e.Favoritid);

                entity.ToTable("favorite");

                entity.Property(e => e.Favoritid)
                    .HasColumnName("favoritid")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Categoryid)
                    .HasColumnName("categoryid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedUserid)
                    .HasColumnName("created_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Platformid)
                    .HasColumnName("platformid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUserid)
                    .HasColumnName("updated_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Favorite)
                    .HasForeignKey(d => d.Categoryid)
                    .HasConstraintName("FK_favorite_category");

                entity.HasOne(d => d.Favorit)
                    .WithOne(p => p.Favorite)
                    .HasForeignKey<Favorite>(d => d.Favoritid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_favorite_user");

                entity.HasOne(d => d.Platform)
                    .WithMany(p => p.Favorite)
                    .HasForeignKey(d => d.Platformid)
                    .HasConstraintName("FK_favorite_platform");
            });

            modelBuilder.Entity<Friends>(entity =>
            {
                entity.HasKey(e => e.Friendid);

                entity.ToTable("friends");

                entity.Property(e => e.Friendid)
                    .HasColumnName("friendid")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedUserid)
                    .HasColumnName("created_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.FriendUserid)
                    .HasColumnName("friend_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUserid)
                    .HasColumnName("updated_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.FriendUser)
                    .WithMany(p => p.FriendsFriendUser)
                    .HasForeignKey(d => d.FriendUserid)
                    .HasConstraintName("FK_friends_user1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FriendsUser)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_friends_user");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("game");

                entity.Property(e => e.Gameid)
                    .HasColumnName("gameid")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Categoryid)
                    .HasColumnName("categoryid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Companyid)
                    .HasColumnName("companyid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedUserid)
                    .HasColumnName("created_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(1000);

                entity.Property(e => e.LaunchDate)
                    .HasColumnName("launch_date")
                    .HasColumnType("date");

                entity.Property(e => e.Platformid)
                    .HasColumnName("platformid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUserid)
                    .HasColumnName("updated_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Game)
                    .HasForeignKey(d => d.Categoryid)
                    .HasConstraintName("FK_game_category");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Game)
                    .HasForeignKey(d => d.Companyid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_game_company");

                entity.HasOne(d => d.Platform)
                    .WithMany(p => p.Game)
                    .HasForeignKey(d => d.Platformid)
                    .HasConstraintName("FK_game_platform");
            });

            modelBuilder.Entity<Gamerates>(entity =>
            {
                entity.HasKey(e => e.Rateid);

                entity.ToTable("gamerates");

                entity.Property(e => e.Rateid)
                    .HasColumnName("rateid")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedUserid)
                    .HasColumnName("created_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(200);

                entity.Property(e => e.Gameid)
                    .HasColumnName("gameid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Rates)
                    .HasColumnName("rates")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUserid)
                    .HasColumnName("updated_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Gamerates)
                    .HasForeignKey(d => d.Gameid)
                    .HasConstraintName("FK_gamerates_game");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Gamerates)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_gamerates_user");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.Property(e => e.Orderid)
                    .HasColumnName("orderid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Cardid)
                    .HasColumnName("cardid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.DiscountRate)
                    .HasColumnName("discount_rate")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Eventgameid)
                    .HasColumnName("eventgameid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Gameid)
                    .HasColumnName("gameid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.OrderCount)
                    .HasColumnName("order_count")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.OrderDate)
                    .HasColumnName("order_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.OrderPrice)
                    .HasColumnName("order_price")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Eventgame)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.Eventgameid)
                    .HasConstraintName("FK_order_eventgame");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.Gameid)
                    .HasConstraintName("FK_order_game");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_order_user");
            });

            modelBuilder.Entity<Platform>(entity =>
            {
                entity.ToTable("platform");

                entity.Property(e => e.Platformid)
                    .HasColumnName("platformid")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedUserid)
                    .HasColumnName("created_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Platformname)
                    .HasColumnName("platformname")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUserid)
                    .HasColumnName("updated_userid")
                    .HasColumnType("numeric(18, 0)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(150);

                entity.Property(e => e.Address2)
                    .HasColumnName("address2")
                    .HasMaxLength(150);

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birth_date")
                    .HasColumnType("date");

                entity.Property(e => e.CellPhone)
                    .HasColumnName("cell_phone")
                    .HasMaxLength(50);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(50);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedUserid)
                    .HasColumnName("created_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.DisplayName)
                    .HasColumnName("display_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.EndDate)
                    .HasColumnName("end_date")
                    .HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(1);

                entity.Property(e => e.HomePhone)
                    .HasColumnName("home_phone")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(50);

                entity.Property(e => e.LockDatetime)
                    .HasColumnName("lock_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.OfficePhone)
                    .HasColumnName("office_phone")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostCode)
                    .HasColumnName("post_code")
                    .HasMaxLength(10);

                entity.Property(e => e.Province)
                    .HasColumnName("province")
                    .HasMaxLength(50);

                entity.Property(e => e.ReceiveEmail)
                    .HasColumnName("receive_email")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("date");

                entity.Property(e => e.TryCount)
                    .HasColumnName("try_count")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUserid)
                    .HasColumnName("updated_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Usertype)
                    .IsRequired()
                    .HasColumnName("usertype")
                    .HasMaxLength(2);
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.ToTable("wishlist");

                entity.Property(e => e.Wishlistid)
                    .HasColumnName("wishlistid")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Access)
                    .HasColumnName("access")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedUserid)
                    .HasColumnName("created_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Gameid)
                    .HasColumnName("gameid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUserid)
                    .HasColumnName("updated_userid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Wishlist)
                    .HasForeignKey(d => d.Gameid)
                    .HasConstraintName("FK_wishlist_game");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Wishlist)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_wishlist_user");
            });
        }
    }
}
