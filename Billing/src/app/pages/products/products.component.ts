import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/Models/Product';
import { ProductService } from 'src/app/Services/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {
  product!: Product;
  listProduct: Product[] = [];
  edit: boolean = false;

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.product ={
    productName:'',
    price:0      
    }

    this.getListProducts()
  }

  getListProducts() {
    this.productService.getProducts().subscribe(res => {
      this.listProduct = res;
    })
  }

  addProduct() {
    this.productService.addProduct(this.product).subscribe(res => {
      console.log(res, 'Product added successfully')
    })
  }

  updateProduct() {
    this.productService.updateProduct(this.product).subscribe(res => {
      this.edit = false;
      this.product = new Product();
      console.log(res, 'Product upgraded successfully')
    })
  }

  removeProduct(data: any) {
    console.log(data.key)
    this.productService.delProduct(data.key).subscribe(res => {
      this.listProduct = this.listProduct.filter(x => x.idProduct != data.key.idProduct)
    })
  }

  editProduct(e: any) {
    this.product = e.key
    this.edit = true;
  }

}
