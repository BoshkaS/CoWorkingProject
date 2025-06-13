// <copyright file="WorkspaceType.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Runtime.Serialization;

namespace CoWorkingProject.Server.Entities.Enums;

public enum WorkspaceType
{
	[EnumMember(Value = "Open space")]
	OpenSpace,
	[EnumMember(Value = "Private rooms")]
	PrivateRoom,
	[EnumMember(Value = "Meeting rooms")]
	MeetingRoom,
}
