using Microsoft.EntityFrameworkCore;

namespace PokemonLikeProject.Model
{
    public partial class ExerciceMonsterContext : DbContext
    {
        // Attribut pour stocker la chaîne de connexion
        private readonly string _databaseLink;

        // Constructeur sans paramètre
        public ExerciceMonsterContext()
        {
        }

        // Constructeur avec chaîne de connexion
        public ExerciceMonsterContext(string databaseLink)
        {
            _databaseLink = databaseLink;
        }

        // Constructeur avec options pour DbContext
        public ExerciceMonsterContext(DbContextOptions<ExerciceMonsterContext> options)
            : base(options)
        {
        }

        // Définition des DbSet pour chaque entité
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<Monster> Monsters { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Spell> Spell { get; set; }

        // Configuration de la chaîne de connexion dans OnConfiguring
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Vérifie si la chaîne de connexion est déjà configurée, sinon l'applique
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_databaseLink);
            }
        }

        // Configuration des entités dans la base de données
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Login__3214EC278FC0D20C");
                entity.ToTable("Login");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.PasswordHash).HasMaxLength(255);
                entity.Property(e => e.Username).HasMaxLength(50);
            });

            modelBuilder.Entity<Monster>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Monster__3214EC273B51FD2A");
                entity.ToTable("Monster");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.ImageUrl).HasMaxLength(255).HasColumnName("ImageURL");
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasMany(d => d.Spells).WithMany(p => p.Monsters)
                    .UsingEntity<Dictionary<string, object>>(
                        "MonsterSpell",
                        r => r.HasOne<Spell>().WithMany()
                            .HasForeignKey("SpellId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__MonsterSp__Spell__44FF419A"),
                        l => l.HasOne<Monster>().WithMany()
                            .HasForeignKey("MonsterId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__MonsterSp__Monst__440B1D61"),
                        j =>
                        {
                            j.HasKey("MonsterId", "SpellId").HasName("PK__MonsterS__293EA4DF302FA808");
                            j.ToTable("MonsterSpell");
                            j.IndexerProperty<int>("MonsterId").HasColumnName("MonsterID");
                            j.IndexerProperty<int>("SpellId").HasColumnName("SpellID");
                        });
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Player__3214EC27BD957673");
                entity.ToTable("Player");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.LoginId).HasColumnName("LoginID");
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Login).WithMany(p => p.Players)
                    .HasForeignKey(d => d.LoginId)
                    .HasConstraintName("FK__Player__LoginID__398D8EEE");

                entity.HasMany(d => d.Monsters).WithMany(p => p.Players)
                    .UsingEntity<Dictionary<string, object>>(
                        "PlayerMonster",
                        r => r.HasOne<Monster>().WithMany()
                            .HasForeignKey("MonsterId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__PlayerMon__Monst__412EB0B6"),
                        l => l.HasOne<Player>().WithMany()
                            .HasForeignKey("PlayerId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__PlayerMon__Playe__403A8C7D"),
                        j =>
                        {
                            j.HasKey("PlayerId", "MonsterId").HasName("PK__PlayerMo__378F20A4DC55C337");
                            j.ToTable("PlayerMonster");
                            j.IndexerProperty<int>("PlayerId").HasColumnName("PlayerID");
                            j.IndexerProperty<int>("MonsterId").HasColumnName("MonsterID");
                        });
            });

            modelBuilder.Entity<Spell>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Spell__3214EC27EC48FC61");
                entity.ToTable("Spell");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

