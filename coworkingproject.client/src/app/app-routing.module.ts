import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WorkspaceComponent } from './main/workspaces/workspace/workspace.component';
import { WorkspacesComponent } from './main/workspaces/workspaces.component';
import { MyBookingsComponent } from './main/my-bookings/my-bookings.component';
import { AddBookingComponent } from './main/my-bookings/my-booking/add-booking/add-booking.component';
import { EditBookingComponent } from './main/my-bookings/my-booking/edit-booking/edit-booking.component';
import { CoworkingsComponent } from './main/coworkings/coworkings.component';

const routes: Routes = [
  { path: '', redirectTo: '/coworkings', pathMatch: 'full' },
  { path: 'coworkings', component: CoworkingsComponent },
  { path: 'workspaces/:coworkingId', component: WorkspacesComponent },
  { path: 'workspace/:id', component: WorkspaceComponent },
  {
    path: 'my-bookings',
    component: MyBookingsComponent,
  },
  { path: 'edit-booking/:id', component: EditBookingComponent },
  { path: 'add-booking', component: AddBookingComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
