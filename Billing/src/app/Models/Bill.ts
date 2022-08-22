import { BillDetail } from "./BillDetail";

export class GetBill{
    idBill?: number;
    billDate!: Date;
    idCustomer!: number;
    billDetails!: BillDetail[];
}

export class GetBillDetails{
    productName!: string;
    price!: number;
    quantity!: number;
}

export class SaveBill {
    billDate!:Date;
    idCustomer!:number;
    
    constructor() {
        this.billDate = new Date        
    }
}

export class SaveBillDetail {
    idProduct!:number;
    quantity!: number;
}

export class ProductDTO {
    idProduct!:number;
    productName!:string;
}