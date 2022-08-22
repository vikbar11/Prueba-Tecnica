import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { SideNavOuterToolbarModule, SideNavInnerToolbarModule, SingleCardModule } from './layouts';
import { FooterModule } from './shared/components';
import { AuthService, ScreenService, AppInfoService } from './shared/services';
import { UnauthenticatedContentModule } from './unauthenticated-content';
import { AppRoutingModule } from './app-routing.module';
import { DxButtonModule } from 'devextreme-angular';
import { DxButtonComponent } from 'devextreme-angular';
import { DxDropDownButtonModule } from 'devextreme-angular';
import { DxButtonGroupModule } from 'devextreme-angular';
import { DxDropDownButtonComponent } from 'devextreme-angular';
import { DxFormModule } from 'devextreme-angular';
import { DxDataGridModule } from 'devextreme-angular';
import { HttpClientModule } from '@angular/common/http';
import { CustomersComponent } from './pages/customers/customers.component';
import { ProductsComponent } from './pages/products/products.component';
import { BillsComponent } from './pages/bills/bills.component';


@NgModule({
  declarations: [
    AppComponent,
    CustomersComponent,
    ProductsComponent,
    BillsComponent
  ],
  imports: [
    BrowserModule,
    SideNavOuterToolbarModule,
    SideNavInnerToolbarModule,
    SingleCardModule,
    FooterModule,
    UnauthenticatedContentModule,
    AppRoutingModule,
    HttpClientModule,
    DxButtonModule,
    DxFormModule,
    DxDataGridModule,    
    DxDropDownButtonModule,
    DxButtonGroupModule
  ],
  providers: [
    AuthService,
    ScreenService,
    AppInfoService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
