import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { WorkspaceDto } from '../../Dto/WorkspaceDto.model';
import { WorkspaceService } from './workspaces.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-workspaces',
  templateUrl: './workspaces.component.html',
  styleUrl: './workspaces.component.css',
})
export class WorkspacesComponent implements OnInit {
  workspaces$!: Observable<WorkspaceDto[]>;
  coworkingId!: string;

  constructor(
    private workspaceService: WorkspaceService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('coworkingId');
      if (id) {
        this.coworkingId = id;
        this.workspaces$ = this.workspaceService.getWorkspaces(
          this.coworkingId
        );
        console.log(this.workspaces$);
      }
    });
  }
}
