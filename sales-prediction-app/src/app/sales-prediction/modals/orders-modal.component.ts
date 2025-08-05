// src/app/components/orders-modal/orders-modal.component.ts
import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { CommonModule } from '@angular/common';
import { Order } from '../interfaces/sales-prediction.interfaces';
import { SalesPredictionService } from '../services/sales-prediction.service';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ViewChild, AfterViewInit } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';

@Component({
  selector: 'app-orders-modal',
  templateUrl: './orders-modal.component.html',
  styleUrls: ['./orders-modal.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatDialogModule,
    MatButtonModule,
    MatPaginatorModule,
    MatSortModule,
    MatDatepickerModule,
    MatInputModule, 
    MatNativeDateModule,
    MatIconModule,
  ]
})
export class OrdersModalComponent implements OnInit, AfterViewInit {
  displayedColumns = ['orderid', 'orderdate', 'requireddate', 'shippeddate'];
  dataSource = new MatTableDataSource<Order>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { custId: number; customerName: string },
    private service: SalesPredictionService,
    private dialogRef: MatDialogRef<OrdersModalComponent>
  ) {
    console.log('🧾 Datos recibidos en modal:', data);
  }

  ngOnInit(): void {
    console.log('🔍 Customer ID para consulta:', this.data.custId);
    this.service.getOrdersByCustomerId(this.data.custId).subscribe(orders => {
      this.dataSource.data = orders;
    });
  }

  onClose(): void {
    this.dialogRef.close();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
}
