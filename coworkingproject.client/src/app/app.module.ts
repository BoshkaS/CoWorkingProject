import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { WorkspacesComponent } from './main/workspaces/workspaces.component';
import { MyBookingsComponent } from './main/my-bookings/my-bookings.component';
import { WorkspaceComponent } from './main/workspaces/workspace/workspace.component';
import { WorkspaceService } from './main/workspaces/worspaces.service';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MatIconModule } from '@angular/material/icon';
import { IconService } from './icon-service/icon-service.model';
import { MyBookingComponent } from './main/my-bookings/my-booking/my-booking.component';
import { MyBookingsService } from './main/my-bookings/my-bookings.service';
import { DeleteBookingComponent } from './main/my-bookings/my-booking/delete-booking/delete-booking.component';
import { DeleteBookingService } from './main/my-bookings/my-booking/delete-booking/delete-booking.service';
import { AddBookingComponent } from './main/my-bookings/my-booking/add-booking/add-booking.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    WorkspacesComponent,
    MyBookingsComponent,
    WorkspaceComponent,
    MyBookingComponent,
    DeleteBookingComponent,
    AddBookingComponent,
  ],
  imports: [BrowserModule, HttpClientModule, AppRoutingModule, MatIconModule],
  providers: [
    WorkspaceService,
    IconService,
    MyBookingsService,
    DeleteBookingService,
    provideAnimationsAsync(),
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
