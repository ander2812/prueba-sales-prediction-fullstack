// src/app/components/sales-prediction/sales-prediction.component.ts
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatSort, MatSortModule, Sort } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { CustomerPrediction } from '../interfaces/sales-prediction.interfaces';
import { SalesPredictionService } from '../services/sales-prediction.service';
import { OrdersModalComponent } from '../modals/orders-modal.component';
import { NewOrderModalComponent } from '../modals/new-order-modal.component';
import { debounceTime, Subject } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';

@Component({
  selector: 'app-sales-prediction',
  templateUrl: './sales-prediction.component.html',
  styleUrls: ['./sales-prediction.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    FormsModule,
    MatPaginatorModule,
    MatSortModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    MatToolbarModule,
    MatDialogModule,
  ]
})
export class SalesPredictionComponent implements OnInit {
  displayedColumns = ['customerName', 'lastOrderDate', 'nextPredictedOrder', 'actions'];
  dataSource = new MatTableDataSource<CustomerPrediction>([]);
  totalItems = 0;
  pageSize = 10;
  pageIndex = 0;
  sortField = 'customerName';
  sortOrder: 'asc' | 'desc' = 'asc';
  filterValue = '';
  private filterSubject = new Subject<string>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private service: SalesPredictionService, private dialog: MatDialog) {}

  ngAfterViewInit() {
  this.dataSource.paginator = this.paginator;
  this.dataSource.sort = this.sort;
  }

  ngOnInit() {
    this.loadData();
    this.filterSubject.pipe(debounceTime(300)).subscribe(value => {
      this.filterValue = value;
      this.pageIndex = 0;
      this.loadData();
    });
  }

  loadData() {
    this.service.getCustomerPredictions(this.pageIndex + 1, this.pageSize, this.sortField, this.sortOrder, this.filterValue)
      .subscribe(res => {
        console.log('Respuesta completa del backend:', res);
        this.dataSource.data = res.data;
        this.totalItems = res.total;
        console.log('Datos recibidos:', res.data);
      });
  }

  onPageChange(event: PageEvent) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadData();
  }

  onSortChange(sort: Sort) {
    this.sortField = sort.active;
    this.sortOrder = sort.direction === '' ? 'asc' : sort.direction;
    this.loadData();
  }

  onFilterChange(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();
    this.filterSubject.next(filterValue);
  }

  openOrdersModal(customer: CustomerPrediction) {
    this.dialog.open(OrdersModalComponent, {
      width: '800px',
      data: { 
        customerName: customer.customerName,
        custId: customer.custId


       }
    });
  }

  openNewOrderModal(customer: CustomerPrediction) {
    const dialogRef = this.dialog.open(NewOrderModalComponent, {
      width: '800px',
      data: { 
        customerName: customer.customerName,
        customerId: customer.custId
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result === true){
        this.loadData();
      }
    });
  }
}
