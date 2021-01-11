import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { Observable, BehaviorSubject } from 'rxjs';

import { AuthRequest } from 'src/app/shared/message-detail.model';
import { AuthResponse } from 'src/app/shared/message-detail.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  AUTH_SERVER_ADDRESS: string = 'http://localhost:5005';
  authSubject =  new BehaviorSubject(false);

  constructor(private httpClient: HttpClient, private storage: Storage) { }

  authenticate(request: AuthRequest): Observable<AuthResponse> {
    return this.httpClient.post<AuthResponse>(`${this.AUTH_SERVER_ADDRESS}/user/authenticate`, request).pipe(
      tap(async (res: AuthResponse) => {
        if(res){
          await this.storage.set("ACCESS_TOKEN", res.token);
          this.authSubject.next(true);
        }
      })
    );
  }

  async logout() {
    await this.storage.remove("ACCESS_TOKEN");
    this.authSubject.next(false);
  }

  isLoggedIn() {
    return this.authSubject.asObservable();
  }
}
