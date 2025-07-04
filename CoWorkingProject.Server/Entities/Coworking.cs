﻿namespace CoWorkingProject.Server.Entities
{
	public class Coworking
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string Address { get; set; }

		public string Photo { get; set; }

		public List<Workspace> Workspaces { get; set; } = new List<Workspace>();
	}
}
