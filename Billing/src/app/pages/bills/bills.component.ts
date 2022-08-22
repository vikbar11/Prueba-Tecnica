import { Component, OnInit } from '@angular/core';
import notify from 'devextreme/ui/notify';
import { GetBill, GetBillDetails, SaveBill, SaveBillDetail } from 'src/app/Models/Bill';
import { BillDetail } from 'src/app/Models/BillDetail';
import { Customer } from 'src/app/Models/Customer';
import { Product } from 'src/app/Models/Product';
import { BillService } from 'src/app/Services/bill.service';
import { CustomerService } from 'src/app/Services/customer.service';
import { ProductService } from 'src/app/Services/product.service';

@Component({
  selector: 'app-bills',
  templateUrl: './bills.component.html',
  styleUrls: ['./bills.component.scss']
})
export class BillsComponent implements OnInit {
  edit: boolean = false;
  listBills: GetBillDetails[] = [];
  listCustomers: Customer[] = [];
  listProducts: Product[] = [];
  listBillDetails:[]=[];

  bills!: GetBill;
  formBills: SaveBill = new SaveBill();
  formBillDetail: SaveBillDetail = new SaveBillDetail();

  idBill: number = 0;

  constructor(private billService: BillService,
    private customerService: CustomerService,
    private productService: ProductService) {
  }

  ngOnInit(): void {
    this.bills = {
      idBill: 0,
      billDetails: [],
      billDate: new Date,
      idCustomer: 0,
    }
    
    this.getCustomers()
    this.getProducts()
  }

  getCustomers() {
    this.customerService.getCustomers().subscribe(res => {
      console.log(res)
      this.listCustomers = res;
    })

  }

  getProducts() {
    this.productService.getProducts().subscribe(res => {
      console.log(res)
      this.listProducts = res;
    })

  }

  getBills() {
    this.billService.getBills().subscribe(res => {
      console.log(res)
      this.listBills = res;
    })
  }
  
  

  addBill(){
    this.billService.addBill(this.formBills).subscribe(res =>{
      console.log("Bill added successfully", res);
      notify({ message: "Bill added successfully", width: 800 }, "success", 1500);
      this.idBill = res.idBill ? res.idBill : 0;
    })
  }

  addBillDetail() {
    const bill: BillDetail = {
      quantity: this.formBillDetail.quantity,
      description: "",
      idBill: this.idBill,
      idProduct: this.formBillDetail.idProduct,
      idBillDetail: 0,
    }

    this.billService.addBillDetail(bill).subscribe(res => {      
      console.log(res, 'Bill detail added successfully');
      this.billService.getBilldetail(this.idBill).subscribe(res => {
        this.listBills = res;
      })
    })
  }

  
  getSumAllProductsPrices = (): number => {
    let total = 0;
    this.listBills.forEach(detail => {
      total += detail.quantity * detail.price;
    })
    return total;
  }


  editBill(e: any) {
    this.bills = e.key
    this.edit = true;
  }
  }
