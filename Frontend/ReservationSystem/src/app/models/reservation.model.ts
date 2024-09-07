import { Customer } from 'src/app/models/customer.model';
import { Service } from 'src/app/models/service.model';

export class Reservation {
  reservationId!: number;
  customerId!: number;
  serviceId!: number;
  reservationDate!: Date;
  customer!: Customer; 
  service!: Service;  
}