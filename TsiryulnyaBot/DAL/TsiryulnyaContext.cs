using Microsoft.EntityFrameworkCore;
using TsiryulnyaBot.DAL.Model;

namespace TsiryulnyaBot.DAL;

public partial class TsiryulnyaContext : DbContext
{
    private readonly string _connectionString;
    public TsiryulnyaContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public TsiryulnyaContext(DbContextOptions<TsiryulnyaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<RecordClient> RecordClients { get; set; }

    public virtual DbSet<RecordFillingStep> RecordFillingSteps { get; set; }

    public virtual DbSet<RecordFillingStepItem> RecordFillingStepItems { get; set; }

    public virtual DbSet<RecordParameter> RecordParameters { get; set; }

    public virtual DbSet<RecordParameterClient> RecordParameterClients { get; set; }

    public virtual DbSet<RecordStatus> RecordStatuses { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Specialist> Specialists { get; set; }

    public virtual DbSet<SpecialistAndService> SpecialistAndServices { get; set; }

    public virtual DbSet<WorkerShift> WorkerShifts { get; set; }

    public virtual DbSet<WorkerShiftStatus> WorkerShiftStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("category_pkey");

            entity.ToTable("category");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("name");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_pkey");

            entity.ToTable("client");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("name");
            entity.Property(e => e.TlgId).HasColumnName("tlg_id");
            entity.Property(e => e.TlgUserName)
                .HasMaxLength(250)
                .HasColumnName("tlg_user_name");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<RecordClient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("record_client_pkey");

            entity.ToTable("record_client");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Client).WithMany(p => p.RecordClients)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("record_client_client_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.RecordClients)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("record_client_status_id_fkey");
        });

        modelBuilder.Entity<RecordFillingStep>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("record_filling_step_pkey");

            entity.ToTable("record_filling_step");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Passed).HasColumnName("passed");
            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.StepItemId).HasColumnName("step_item_id");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Record).WithMany(p => p.RecordFillingSteps)
                .HasForeignKey(d => d.RecordId)
                .HasConstraintName("record_filling_step_record_id_fkey");

            entity.HasOne(d => d.StepItem).WithMany(p => p.RecordFillingSteps)
                .HasForeignKey(d => d.StepItemId)
                .HasConstraintName("record_filling_step_step_item_id_fkey");
        });

        modelBuilder.Entity<RecordFillingStepItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("record_filling_step_item_pkey");

            entity.ToTable("record_filling_step_item");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("name");
            entity.Property(e => e.StepPosition).HasColumnName("step_position");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<RecordParameter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("record_parameter_pkey");

            entity.ToTable("record_parameter");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(250)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<RecordParameterClient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("record_parameter_client_pkey");

            entity.ToTable("record_parameter_client");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DatetimeValue).HasColumnName("datetime_value");
            entity.Property(e => e.IntegerValue).HasColumnName("integer_value");
            entity.Property(e => e.ParameterId).HasColumnName("parameter_id");
            entity.Property(e => e.RecordClientId).HasColumnName("record_client_id");
            entity.Property(e => e.StringValue)
                .HasMaxLength(250)
                .HasColumnName("string_value");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UuidValue).HasColumnName("uuid_value");

            entity.HasOne(d => d.Parameter).WithMany(p => p.RecordParameterClients)
                .HasForeignKey(d => d.ParameterId)
                .HasConstraintName("record_parameter_client_parameter_id_fkey");

            entity.HasOne(d => d.RecordClient).WithMany(p => p.RecordParameterClients)
                .HasForeignKey(d => d.RecordClientId)
                .HasConstraintName("record_parameter_client_record_client_id_fkey");
        });

        modelBuilder.Entity<RecordStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("record_status_pkey");

            entity.ToTable("record_status");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("service_pkey");

            entity.ToTable("service");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Specialist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("specialist_pkey");

            entity.ToTable("specialist");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Client).WithMany(p => p.Specialists)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("specialist_client_id_fkey");
        });

        modelBuilder.Entity<SpecialistAndService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("specialist_and_service_pkey");

            entity.ToTable("specialist_and_service");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.SpecialistId).HasColumnName("specialist_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Category).WithMany(p => p.SpecialistAndServices)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("specialist_and_service_category_id_fkey");

            entity.HasOne(d => d.Service).WithMany(p => p.SpecialistAndServices)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("specialist_and_service_service_id_fkey");

            entity.HasOne(d => d.Specialist).WithMany(p => p.SpecialistAndServices)
                .HasForeignKey(d => d.SpecialistId)
                .HasConstraintName("specialist_and_service_specialist_id_fkey");
        });

        modelBuilder.Entity<WorkerShift>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("worker_shift_pkey");

            entity.ToTable("worker_shift");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.SpecialistId).HasColumnName("specialist_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Specialist).WithMany(p => p.WorkerShifts)
                .HasForeignKey(d => d.SpecialistId)
                .HasConstraintName("worker_shift_specialist_id_fkey");
        });

        modelBuilder.Entity<WorkerShiftStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workers_shift_status_pkey");

            entity.ToTable("worker_shift_status");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
