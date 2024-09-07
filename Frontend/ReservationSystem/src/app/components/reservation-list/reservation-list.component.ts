import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReservationService } from '../../services/reservation.service';
import { CustomerService } from '../../services/customer.service';
import { ServiceService } from '../../services/service.service';
import { Reservation } from '../../models/reservation.model';
import { Customer } from 'src/app/models/customer.model';
import { Service } from 'src/app/models/service.model';

@Component({
  selector: 'app-reservation-list',
  templateUrl: './reservation-list.component.html',
  styleUrls: ['./reservation-list.component.scss'],
})
export class ReservationListComponent implements OnInit {

  constructor(
      private reservationService: ReservationService,
      private customerService: CustomerService,
      private serviceService: ServiceService, 
      private router: Router
    ) {}

  reservations: Reservation[] = [];
  customers: Customer[] = [];
  services: Service[] = [];
  dataGrid: Reservation[] = [];

  // Filtros
  filterDate: string | null = null;
  filterServiceId: number | null = null;
  filterCustomerId: number | null = null;

  ngOnInit(): void {
    this.loadCustomers();
    this.loadServices();
    this.loadReservations();
  }

  loadCustomers(): void {
    this.customerService.getAllCustomers().subscribe((customers: Customer[]) => {
      this.customers = customers;
    });
  }

  loadServices(): void {
    this.serviceService.getAllServices().subscribe((services: Service[]) => {
      this.services = services;
    });
  }

  loadReservations() {
    this.reservationService.getReservations().subscribe({
      next: (reservations: Reservation[]) => {
        this.reservations = reservations;
        this.GroupDataReservations();
      },
      error: (error) => {
        console.error('Error loading reservations:', error);
      }
    });
  }

  GroupDataReservations(): void{
    this.dataGrid = this.reservations.map(reservation => {
      const customer = this.customers.find(c => c.customerId === reservation.customerId);
      const service = this.services.find(s => s.serviceId=== reservation.serviceId);

      return {
        ...reservation,
        customerName: customer ? customer.firstName : 'Unknown Customer', 
        serviceName: service ? service.name : 'Unknown Service' 
      };
    });
  }

  goToCreateReservation(): void {
    this.router.navigate(['/reservations/new']);
  }
  
  editReservation(reservationId: number): void {
    this.router.navigate(['/reservations/edit', reservationId]);
  }

  deleteReservation(id: number): void {
    this.reservationService.deleteReservation(id).subscribe(() => {
      this.loadReservations();
    });
  }

  applyFilters(): void {
    this.reservationService.filterReservations(
      this.filterDate ? new Date(this.filterDate) : undefined,
      this.filterServiceId ? this.filterServiceId : undefined,
      this.filterCustomerId ? this.filterCustomerId : undefined
    ).subscribe((reservations: Reservation[]) => {
      this.reservations = reservations;
      this.GroupDataReservations();
    });
  }
}
