import { Component, OnInit } from '@angular/core';
import { FormControl, FormBuilder, FormGroup, Validator } from '@angular/forms'
import { from } from 'rxjs';
import { StudentService } from '../service/student.service';
import { Router } from '@angular/router'


@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.sass']
})
export class StudentComponent implements OnInit {
  students: any = [];
  StudentFrom: FormGroup;
  stdData = [];
  userId: number;
  updateBtn: boolean = false;
  submitBtn: boolean = true;
  constructor(
    private formbuilder: FormBuilder,
    private router: Router,
    private studentService: StudentService
  ) {
    this.StudentFrom = this.formbuilder.group({
      name: [''],
      email: [''],
      mobile: [''],
      age: [''],
      password: ['']
    });
  }

  ngOnInit() {
    this.getStudent();
  }

  CreateStudent() {
    console.log("form value", this.StudentFrom.value);
    this.studentService.post('CreateStudent', this.StudentFrom.value).subscribe(res => {
      console.log("student data inserted ", res);
      this.ngOnInit();
    })
    this.StudentFrom.reset();
  }

  getStudent() {
    this.studentService.get('GetStudent').subscribe(res => {
      this.students = res.Data;
      console.log('Student Data Fatch ', res);
    });
  }

  DeleteStudent(id) {
    console.log('student deleted', id);
    let data = { Id: id };
    this.studentService.post('DeleteStudent', data).subscribe(res => {
      console.log('student deleted', res);
      this.ngOnInit();
    })
  }

  update(data) {
    console.log('student Fill Values in form', data);
    this.updateBtn = true;
    this.submitBtn = false;
    this.userId = data.Id;
    this.StudentFrom.get('name').setValue(data.Name);
    this.StudentFrom.get('email').setValue(data.Email);
    this.StudentFrom.get('mobile').setValue(data.Mobile);
    this.StudentFrom.get('password').setValue(data.Password);
    this.StudentFrom.get('age').setValue(data.Age);
  }

  updateStudent() {
    let data = {
      id: this.userId,
      name: this.StudentFrom.value.name,
      email: this.StudentFrom.value.email,
      mobile: this.StudentFrom.value.mobile,
      password: this.StudentFrom.value.password,
      age: this.StudentFrom.value.age,
    };
    this.studentService.post('UpdateStudent', data).subscribe(res => {
      console.log('student updated', res);
      this.StudentFrom.reset();
      this.ngOnInit();
    })
    this.updateBtn = false;
    this.submitBtn = true;
  }

  Logout() {
    localStorage.removeItem("userData");
    this.router.navigateByUrl("/login");
  }
}
