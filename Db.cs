using Microsoft.EntityFrameworkCore;

class Db: DbContext {
    public DbSet<Task> Tasks { get; set; }

    public Db(DbContextOptions<Db> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSeeding((context, _) => {
                context.Set<Task>().AddRange(
                    new Task {
                        Name = "履歴書と職務経歴書を送る",
                        StartAt = DateTime.Now,
                        CreatedAt = DateTime.Now
                    },
                    new Task {
                        Name = "自動車学校の料金を振り込む",
                        StartAt = DateTime.Now,
                        CreatedAt = DateTime.Now,
                        isCompleted = true
                    }
                );
                context.SaveChanges();
            }
        );
    }
}
