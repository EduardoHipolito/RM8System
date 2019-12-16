import { Component, OnInit } from '@angular/core';

import { Observable } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PageLoaderService } from '../_framework/pageLoader/page-loader.service';
import { AuthService } from '../_framework/auth/auth.service'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
  form: FormGroup;
  error = false;
  errorMessage = '';

  constructor(
    private pageLoaderService: PageLoaderService,
    private fb: FormBuilder,
    private authService: AuthService) {
    this.pageLoaderService.LoadOff();
  }

  onSignin() {
    this.authService.signIn(this.form.value);
  }

  ngOnInit(): any {
    this.form = this.fb.group({
      UserName: ['', Validators.required],
      Password: ['', Validators.required],
    })
  }

}
