using CoWorkingProject.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoWorkingProject.Server.Annotations;

public class WorkspaceImageAnnotation : BaseEntityAnnotation<WorkspaceImage>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="RoomAnnotation"/> class.
	/// </summary>
	/// <param name="modelBuilder"></param>
	public WorkspaceImageAnnotation(ModelBuilder modelBuilder)
		: base(modelBuilder)
	{
	}

	public override void Annotate()
	{
		this.ModelBuilder.HasKey(r => r.Id);
		this.ModelBuilder.Property(r => r.Id).ValueGeneratedOnAdd();
		this.ModelBuilder.HasOne(r => r.Workspace).WithMany(r => r.Images).HasForeignKey(r => r.WorkspaceId).OnDelete(DeleteBehavior.Cascade);
	}
}