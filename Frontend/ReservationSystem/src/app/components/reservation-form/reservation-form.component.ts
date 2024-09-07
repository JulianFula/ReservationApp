import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ReservationService } from '../../services/reservation.service';
import { Reservation } from '../../models/reservation.model';
import { CustomerService } from '../../services/customer.service';
import { ServiceService } from '../../services/service.service';
import { Customer } from '../../models/customer.model';
import { Service } from '../../models/service.model';

@Component({
  selector: 'app-reservation-form',
  templateUrl: './reservation-form.component.html',
  styleUrls: ['./reservation-form.component.scss']
})
export class ReservationFormComponent implements OnInit {
  reservationForm: FormGroup;
  isEditMode: boolean = false;
  reservationId?: number;
  customers: Customer[] = [];
  services: Service[] = [];    

  constructor(
    private fb: FormBuilder,
    private reservationService: ReservationService,// Inyectar el servicio de Reservation
    private customerService: CustomerService,  // Inyectar el servicio de Customer
    private serviceService: ServiceService,    // Inyectar el servicio de Service
    private router: Router
  ) {
    this.reservationForm = this.fb.group({
      reservationId: 0,
      customerId: ['', Validators.required],
      serviceId: ['', Validators.required],
      reservationDate: ['', Validators.required],
    });
  }

  ngOnInit(): void {

    this.loadCustomers();  // Cargar Customers
    this.loadServices();   // Cargar Services

    // Determinar si el formulario está en modo de edición o de creación.
    const url = this.router.url;
    if (url.includes('edit')) {
      this.isEditMode = true;
      this.reservationId = Number(url.split('/').pop());
      this.loadReservation(this.reservationId);
    }
  }

  loadCustomers(): void {
    this.customerService.getAllCustomers().subscribe((data: Customer[]) => {
      this.customers = data;
    });
  }

  loadServices(): void {
    this.serviceService.getAllServices().subscribe((data: Service[]) => {
      this.services = data;
    });
  }

  loadReservation(id: number): void {
    this.reservationService.getReservationById(id).subscribe((reservation: Reservation) => {
      this.reservationForm.patchValue({
        customerId: reservation.customerId,
        serviceId: reservation.serviceId,
        reservationDate: new Date(reservation.reservationDate).toISOString().substring(0, 10),
      });
    });
  }

  onSubmit(): void {
    if (this.reservationForm.valid) {
      const reservation: Reservation = this.reservationForm.value;
      if (this.isEditMode && this.reservationId) {
        this.reservationService.updateReservation(this.reservationId, reservation).subscribe(() => {
          alert('Reservation updated successfully!');
          this.router.navigate(['/reservations']);  // Redirigir después de actualizar
        });
      } else {
        console.log('datos',reservation)
        this.reservationService.createReservation(reservation).subscribe(() => {
          alert('Reservation created successfully!');
          this.router.navigate(['/reservations']);  // Redirigir después de crear
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/reservations']);
  }
}