export interface CustomerPrediction {
  custId: number;
  customerName: string;
  lastOrderDate: string;
  nextPredictedOrder: string;
}

export interface CustomerOrders {
  orderid: number,
  custId: number,
  empid: number,
  orderdate: Date,
  requireddate: Date,
  shippeddate: Date,
  shipperid: number,
  freight: number,
  shipname: string,
  shipaddress: string,
  shipcity: string,
  shipregion: null,
  shippostalcode: null,
  shipcountry: null,
  orderDetails: OrderDetail[],
  cust: null,
  emp: null,
  shipper: null
}



export interface Order {
  orderid: number;
  custId?: number;
  empid: number;
  orderdate: Date;
  requireddate: Date;
  shippeddate?: Date;
  shipperid: number;
  freight: number;
  shipname: string;
  shipaddress: string;
  shipcity: string;
  shipregion?: string;
  shippostalcode?: string;
  shipcountry: string;
  orderDetails: OrderDetail[];
}

export interface OrderDetail {
  orderid?: number;
  productid: number;
  unitprice: number;
  qty: number;
  discount: number;
}

export interface ApiResponse<T> {
  data: T[];
  total: number;
}
