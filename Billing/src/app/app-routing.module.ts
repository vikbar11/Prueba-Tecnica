import { NgModule } from '@angular/core';
import { Routes, RouterModule} from '@angular/router';
import { AuthGuardService } from './shared/services';
import { HomeComponent } from './pages/home/home.component';
import { DxDataGridModule, DxFormModule } from 'devextreme-angular';
import { CustomersComponent } from './pages/customers/customers.component';
import { BillsComponent } from './pages/bills/bills.component';
import { ProductsComponent } from './pages/products/products.component';

const routes: Routes = [  
  {path: 'customers', component: CustomersComponent},
  {path: 'bills', component: BillsComponent},
  {path: 'products', component: ProductsComponent},
  {path: 'home', component: HomeComponent},
  {path: '**', pathMatch: 'full', redirectTo: 'home'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes), DxFormModule],
  exports: [RouterModule],
})
export class AppRoutingModule { }
