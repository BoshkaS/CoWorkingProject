import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WorkspaceComponent } from './main/workspaces/workspace/workspace.component';
import { WorkspacesComponent } from './main/workspaces/workspaces.component';
import { MyBookingsComponent } from './main/my-bookings/my-bookings.component';
import { AddBookingComponent } from './main/my-bookings/my-booking/add-booking/add-booking.component';

const routes: Routes = [
  { path: '', redirectTo: '/workspaces', pathMatch: 'full' },
  { path: 'workspaces', component: WorkspacesComponent },
  { path: 'workspace/:id', component: WorkspaceComponent },
  {
    path: 'my-bookings',
    component: MyBookingsComponent,
  },
  { path: 'add-booking', component: AddBookingComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
