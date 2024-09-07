import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS  } from '@angular/common/http';
import { MatTableModule } from '@angular/material/table'; // Importa MatTableModule
import { MatPaginatorModule } from '@angular/material/paginator'; // Importa MatPaginatorModule
import { MatSortModule } from '@angular/material/sort'; // Importa MatSortModule
import { MatButtonModule } from '@angular/material/button'; // Importa MatButtonModule
import { FormsModule } from '@angular/forms'; // Importar FormsModule
import { JwtModule } from '@auth0/angular-jwt';
import { AuthInterceptor } from './interceptors/auth.interceptor';

import { AppComponent } from './app.component';
import { ReservationService } from './services/reservation.service';
import { CustomerService } from './services/customer.service';
import { ServiceService } from './services/service.service';
import { ReservationListComponent } from './components/reservation-list/reservation-list.component';
import { ReservationFormComponent } from './components/reservation-form/reservation-form.component';
import { CustomerListComponent } from './components/customer-list/customer-list.component';
import { CustomerFormComponent } from './components/customer-form/customer-form.component';
import { ServiceListComponent } from './components/service-list/service-list.component';
import { ServiceFormComponent } from './components/service-form/service-form.component';
import { AppRoutingModule } from './app-routing.module';
import { LoginComponent } from './components/login/login.component';
import { AuthService } from './services/auth.service';
import { RegisterComponent } from './components/register/register.component';


@NgModule({
  declarations: [
      AppComponent,
      LoginComponent,
      ReservationListComponent,
      ReservationFormComponent,
      CustomerListComponent,
      CustomerFormComponent,
      ServiceListComponent,
      ServiceFormComponent,
      LoginComponent,
      RegisterComponent
      
    ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    MatTableModule,   // Agrega MatTableModule
    MatPaginatorModule, // Agrega MatPaginatorModule
    MatSortModule,     // Agrega MatSortModule
    MatButtonModule,   // Agrega MatButtonModule
    FormsModule, // Agregar FormsModule aquí
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => localStorage.getItem('token'),
        allowedDomains: ['localhost:7069'],
        disallowedRoutes: ['https://localhost:7069/api/Auth']
      }
    }),
  ],
  providers: [
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent],
})
export class AppModule {}