import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { enviroment } from 'src/enviroments/enviroment';
import { Member } from '../_models/members';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = 'https://localhost:7255/api/';

  members:Member[]= [];

  constructor(private http:HttpClient) { }

  getMembers(){
    if(this.members.length>0) return of(this.members)
    return this.http.get<Member[]>(this.baseUrl + 'users').pipe(
      map(members =>{
        this.members = members;
        return members;
      })
    )
  }

  getMember(username:string){
    const member = this.members.find(x => x.userName == username);
    if(member) return of(member)
    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

  upDateMember(member:Member){
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = {...this.members[index], ...member}  //... è l'operatore di spread, sparpaglia gli elementi di members[index] in member
      })
    );
  }

  /*getHttpOptions(){
    const userString = localStorage.getItem('user');        Avendo creato un interceptor che aggiunge automaticamente i token di autenticazione, 
    if (!userString) return;                                questo codice non ci serve più
    const user = JSON.parse(userString);
    return{
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + user.token
      })
    }
  }
  */
}
