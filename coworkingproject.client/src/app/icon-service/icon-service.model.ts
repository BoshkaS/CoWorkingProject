import { NgModule } from '@angular/core';
import { MatIconModule, MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';

@NgModule({
  imports: [MatIconModule],
  exports: [MatIconModule],
})
export class IconService {
  constructor(
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer
  ) {
    this.registerIcons();
  }

  private registerIcons() {
    this.iconRegistry.addSvgIcon(
      'air-conditioning',
      this.sanitizer.bypassSecurityTrustResourceUrl(
        'assets/icons/Air Conditioning.svg'
      )
    );
    this.iconRegistry.addSvgIcon(
      'gamepad',
      this.sanitizer.bypassSecurityTrustResourceUrl(
        'assets/icons/Device gamepad.svg'
      )
    );
    this.iconRegistry.addSvgIcon(
      'wifi',
      this.sanitizer.bypassSecurityTrustResourceUrl('assets/icons/WiFi.svg')
    );
    this.iconRegistry.addSvgIcon(
      'coffee-bar',
      this.sanitizer.bypassSecurityTrustResourceUrl(
        'assets/icons/Coffee Bar.svg'
      )
    );
    this.iconRegistry.addSvgIcon(
      'headphones',
      this.sanitizer.bypassSecurityTrustResourceUrl(
        'assets/icons/Headphones.svg'
      )
    );
    this.iconRegistry.addSvgIcon(
      'microphones',
      this.sanitizer.bypassSecurityTrustResourceUrl(
        'assets/icons/Microphones.svg'
      )
    );
    this.iconRegistry.addSvgIcon(
      'default-icon',
      this.sanitizer.bypassSecurityTrustResourceUrl('assets/icons/default.svg')
    );
    this.iconRegistry.addSvgIcon(
      'user',
      this.sanitizer.bypassSecurityTrustResourceUrl('assets/icons/user.svg')
    );
    this.iconRegistry.addSvgIcon(
      'edit',
      this.sanitizer.bypassSecurityTrustResourceUrl('assets/icons/edit.svg')
    );
    this.iconRegistry.addSvgIcon(
      'delete',
      this.sanitizer.bypassSecurityTrustResourceUrl('assets/icons/trash.svg')
    );
    this.iconRegistry.addSvgIcon(
      'date',
      this.sanitizer.bypassSecurityTrustResourceUrl('assets/icons/calendar.svg')
    );
    this.iconRegistry.addSvgIcon(
      'time',
      this.sanitizer.bypassSecurityTrustResourceUrl(
        'assets/icons/clock-hour-3.svg'
      )
    );
    this.iconRegistry.addSvgIcon(
      'armchair',
      this.sanitizer.bypassSecurityTrustResourceUrl('assets/icons/armchair.svg')
    );
    this.iconRegistry.addSvgIcon(
      'icon-delete',
      this.sanitizer.bypassSecurityTrustResourceUrl(
        'assets/icons/Icon-delete.svg'
      )
    );
    this.iconRegistry.addSvgIcon(
      'x',
      this.sanitizer.bypassSecurityTrustResourceUrl('assets/icons/x.svg')
    );

    this.iconRegistry.addSvgIcon(
      'confirm',
      this.sanitizer.bypassSecurityTrustResourceUrl(
        'assets/icons/confirm-booking.svg'
      )
    );
    this.iconRegistry.addSvgIcon(
      'deny',
      this.sanitizer.bypassSecurityTrustResourceUrl(
        'assets/icons/deny-booking.svg'
      )
    );
  }
}
