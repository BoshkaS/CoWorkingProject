import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { WorkspaceDto } from '../../Dto/WorkspaceDto.model';

@Injectable()
export class WorkspaceService {
  constructor(private http: HttpClient) {}

  getWorkspaces() {
    return this.http.get<WorkspaceDto[]>(
      'https://localhost:7065/api/workspace'
    );
  }
}
