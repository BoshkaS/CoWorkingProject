import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { WorkspaceDto } from '../../Dto/WorkspaceDto.model';
import { WorkspaceService } from './worspaces.service';

@Component({
  selector: 'app-workspaces',
  templateUrl: './workspaces.component.html',
  styleUrl: './workspaces.component.css',
})
export class WorkspacesComponent implements OnInit {
  workspaces$!: Observable<WorkspaceDto[]>;

  constructor(private workspaceService: WorkspaceService) {}

  ngOnInit(): void {
    this.workspaces$ = this.workspaceService.getWorkspaces();
    console.log(this.workspaces$);
  }
}
