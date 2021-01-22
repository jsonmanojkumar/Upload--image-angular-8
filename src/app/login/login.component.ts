import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validator } from '@angular/forms'
import { Router } from '@angular/router'
import { StudentService } from '../service/student.service'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent implements OnInit {

  LoginForm: FormGroup;
  datas: any = {};
  constructor(public formbuilder: FormBuilder, public router: Router, private studentService: StudentService) {
    this.LoginForm = this.formbuilder.group({
      email: [''],
      password: ['']
    })
  }
  ngOnInit() {
  }


  Login() {
    console.log("Form Data", this.LoginForm.value);
    this.studentService.post('userLogin', this.LoginForm.value).subscribe(res => {
      console.log("res", res);
      if (res.Message == "Not Found") {
        alert(" User Not Found");
      }
      else {
        let uData = {
          email: res.Data.Email,
          password: res.Data.Password
        }
        localStorage.setItem('userData', JSON.stringify(uData));
        this.router.navigateByUrl("/student");
      }
    });
  }




}
