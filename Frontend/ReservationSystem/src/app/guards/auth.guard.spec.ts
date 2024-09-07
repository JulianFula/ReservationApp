import { TestBed } from '@angular/core/testing';
import { Router, UrlTree } from '@angular/router';
import { AuthGuard } from './auth.guard';
import { AuthService } from '../services/auth.service';

describe('AuthGuard', () => {
  let authGuard: AuthGuard;
  let authService: jasmine.SpyObj<AuthService>;
  let router: jasmine.SpyObj<Router>;

  beforeEach(() => {
    const authServiceSpy = jasmine.createSpyObj('AuthService', ['isLoggedIn']);
    const routerSpy = jasmine.createSpyObj('Router', ['createUrlTree']);

    TestBed.configureTestingModule({
      providers: [
        AuthGuard,
        { provide: AuthService, useValue: authServiceSpy },
        { provide: Router, useValue: routerSpy }
      ]
    });

    authGuard = TestBed.inject(AuthGuard);
    authService = TestBed.inject(AuthService) as jasmine.SpyObj<AuthService>;
    router = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  });

  it('should allow activation if user is logged in', () => {
    authService.isLoggedIn.and.returnValue(true); // Simula que el usuario está autenticado

    expect(authGuard.canActivate()).toBeTrue(); // Debería devolver `true` porque el usuario está autenticado
  });

  it('should not allow activation if user is not logged in', () => {
    authService.isLoggedIn.and.returnValue(false); // Simula que el usuario no está autenticado
    const urlTree = jasmine.createSpyObj('UrlTree', []); // Crear un espía de `UrlTree`
    router.createUrlTree.and.returnValue(urlTree as unknown as UrlTree); // Asegurar que devuelva un `UrlTree`

    expect(authGuard.canActivate()).toBe(urlTree); // Comprobar que devuelve el `UrlTree` simulado
  });
});
