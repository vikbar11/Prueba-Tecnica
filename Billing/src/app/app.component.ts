import { Component, HostBinding } from '@angular/core';
import { AuthService, ScreenService, AppInfoService } from './shared/services';
import notify from 'devextreme/ui/notify';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Billing';

  // employee = {
  //   FullName: 'Victor Barrera',
  //   IdEmployee: 730,
  //   hireDate: new Date(2000, 1, 1)
  // }

  // hireDateOptions = {
  //     disabled: true
  // }

  // showMessage = () => {
  //   notify("The button was clicked");
  // }
}
