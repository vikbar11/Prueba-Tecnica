import { Component, OnInit } from '@angular/core';
import { Customer } from 'src/app/Models/Customer';
import { CustomerService } from 'src/app/Services/customer.service';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
export class CustomersComponent implements OnInit {

  customer!: Customer;  
  listCustomer: Customer[] = [];
  edit: boolean = false;

  constructor(private customerService: CustomerService) {}

  ngOnInit(): void {
    this.customer = {
      firstName: '',
      lastName: '',
      email: '',
      bornDay: new Date()
    }

    this.getListCustomers()
  }

  getListCustomers() {
    this.customerService.getCustomers().subscribe(res => {
      this.listCustomer = res;
    })
  }

  addCustomer() {
    this.customerService.addCustomer(this.customer).subscribe(res => {      
      console.log(res, '¡Customer successfully added!');
    })
  }

  updateCostumer() {
    this.customerService.updateCustomer(this.customer).subscribe(res => {
      this.edit = false;
      this.customer = new Customer();
      console.log(res, 'Customer upgraded successfully')
    })
  }

  removeCustomer(data: any) {
    console.log(data.key)
    this.customerService.delCustomer(data.key).subscribe(res => {
      this.listCustomer = this.listCustomer.filter(x => x.idCustomer != data.key.idCustomer)
      
    })
  }

  editCustomer(e: any) {
    this.customer = e.key
    this.edit = true;
  }

}
