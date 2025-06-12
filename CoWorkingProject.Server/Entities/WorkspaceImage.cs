namespace CoWorkingProject.Server.Entities
{
	public class WorkspaceImage
	{
		public Guid Id { get; set; }
		public string ImagePath { get; set; }
		public Guid WorkspaceId { get; set; }
		public Workspace Workspace { get; set; }
	}
}
