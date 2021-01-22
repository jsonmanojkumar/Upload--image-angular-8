import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validator } from '@angular/forms';
import { Router } from '@angular/router';
import { StudentService } from '../service/student.service'
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.sass']
})
export class RegisterComponent implements OnInit {

  RegisterForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private studentService: StudentService,
    private router: Router
  ) {
    this.RegisterForm = this.formBuilder.group({
      name: [''],
      email: [''],
      password: ['']
    });
  }

  ngOnInit() {
  }

  register() {
    console.log("Register Data", this.RegisterForm.value)
    this.studentService.post('signUp', this.RegisterForm.value).subscribe(res => {
      console.log("user inserted", res);
      if (res.Description == "successfully") {
        this.router.navigateByUrl("/student");
      }
      else {
        this.router.navigateByUrl("/register");
      }
    })
    localStorage.setItem('usersData', JSON.stringify(this.RegisterForm.value));
    this.RegisterForm.reset();
  }

}
