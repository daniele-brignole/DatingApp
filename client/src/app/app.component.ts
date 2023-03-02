import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Dating App';
  
  constructor(private accountService: AccountService){}
  
  ngOnInit(): void {
    //richiesta all'API script
    
    this.setCurrentUser();
  }

  
  setCurrentUser(){//controlla all'avvio del browser se user è presente nella local storage, se così è è ancor loggato e lo imposta come user corrente nel servizio account
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);
  }

  
}
