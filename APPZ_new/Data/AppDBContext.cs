using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using APPZ_new.Models;
using APPZ_new.SqlTaskModels;

namespace APPZ_new.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<SqlUserTask> SqlUserTasks { get; set; }

        #region sql tasks
        public DbSet<SqlTask> SqlTasks { get; set; }
        public DbSet<SqlAnswer> SqlAnswers { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Answer>()
                .HasOne(e => e.Question)
                .WithMany(i => i.Answers)
                .HasForeignKey(e => e.QuestionId);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Tasks)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<Task>()
                .HasOne(e => e.Category)
                .WithMany(e => e.Tasks)
                .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.Questions)
                .WithOne(e => e.Task)
                .HasForeignKey(e => e.TaskId);

            modelBuilder.Entity<UserTask>()
                .HasOne(e => e.User)
                .WithMany(e => e.Tasks)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<UserTask>()
                .HasOne(e => e.Task)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.TaskId);

        }

    }
}
