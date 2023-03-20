import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model:any = {}
  registerForm: FormGroup = new FormGroup({});
  

  constructor(private accountService: AccountService, private toastr:ToastrService) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    this.registerForm = new FormGroup({
        username: new FormControl('Initial Input', Validators.required),
        password: new FormControl(),
        confirmPassword: new FormControl([this.matchValues('password')]),
    })
  }

  matchValues(matchTo: string): ValidatorFn{          //validatore personalizzato
    return(control:AbstractControl) => {
      return control.value == control.parent?.get(matchTo)?.value ? null: {notMatching: true}
    }
  }
  register(){
    
    this.accountService.register(this.model).subscribe({
      next:() => {
        
        this.cancel();
      },
    
      error: error => this.toastr.error(error.error)
    })

    /*
      this.accountService.register(this.model).subscribe({
        next: () => {
          this.router.navigateByUrl('/members')
        },
        error: ......
        
      })
    */ 
    
  }

  cancel(){
    this.cancelRegister.emit(false);
  }
}
