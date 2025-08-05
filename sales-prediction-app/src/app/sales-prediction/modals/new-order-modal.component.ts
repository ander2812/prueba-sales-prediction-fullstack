// src/app/components/new-order-modal/new-order-modal.component.ts
import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators, FormArray, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { SalesPredictionService } from '../services/sales-prediction.service';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-new-order-modal',
  templateUrl: './new-order-modal.component.html',
  styleUrls: ['./new-order-modal.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule
  ]
})
export class NewOrderModalComponent {
  form: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { custId: number; customerName: string },
    private fb: FormBuilder,
    private service: SalesPredictionService,
    private dialogRef: MatDialogRef<NewOrderModalComponent>
  ) {
    this.form = this.fb.group({
      empid: ['', Validators.required],
      shipperid: ['', Validators.required],
      shipname: ['', Validators.required],
      shipaddress: ['', Validators.required],
      shipcity: ['', Validators.required],
      shipcountry: ['', Validators.required],
      shipregion: [''],
      shippostalcode: [''],
      freight: [0, Validators.required],
      orderdate: ['', Validators.required],
      requireddate: ['', Validators.required],
      shippeddate: [''],
      orderDetails: this.fb.array([this.createOrderDetail()])
    });
  }

  createOrderDetail(): FormGroup {
    return this.fb.group({
      productid: ['', Validators.required],
      unitprice: ['', Validators.required],
      qty: ['', Validators.required],
      discount: [0]
    });
  }

  get orderDetails() {
    return this.form.get('orderDetails') as FormArray;
  }

  addOrderDetail() {
    this.orderDetails.push(this.createOrderDetail());
  }

  removeOrderDetail(index: number) {
    if (this.orderDetails.length > 1) {
      this.orderDetails.removeAt(index);
    }
  }

  submit() {
  if (this.form.valid) {
    const formValues = this.form.value;

    // Convertir los campos de cadena a número
    const newOrder = {
      ...formValues,
      custId: this.data.custId,
      empid: +formValues.empid, // Convierto a número usando el operador unario +
      shipperid: +formValues.shipperid,
      freight: +formValues.freight,
      // Mapear los orderDetails para convertir productid, unitprice, qty y discount a números
      orderDetails: formValues.orderDetails.map((detail: any) => ({
        orderid: 0,
        productid: +detail.productid,
        unitprice: +detail.unitprice,
        qty: +detail.qty,
        discount: +detail.discount
      }))
    };

    console.log('Objeto de la orden que se enviará:', newOrder);

    this.service.createOrder(newOrder).subscribe({
      next: (response) => {
        console.log('Respuesta exitosa:', response);
        this.dialogRef.close(true);
      },
      error: (err) => {
        console.error('Error al crear la orden:', err);
      }
    });
  }
}
}
