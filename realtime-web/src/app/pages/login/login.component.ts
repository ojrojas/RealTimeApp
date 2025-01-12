import { FormBuilder, FormsModule, FormControl, ReactiveFormsModule, Validators, FormGroup } from '@angular/forms';
import { ChangeDetectionStrategy, Component, inject, isDevMode } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { UserStore } from '../../core/stores/identity.store';
import { MatButtonModule } from '@angular/material/button';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  imports: [
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent {
  loginValid = false;
  formBuilder = inject(FormBuilder);
  store = inject(UserStore);
  loginForm = new FormGroup({
    userName: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  });


  onSubmit = (loginForm: FormGroup) => {
    this.store.login(loginForm.value);
    if (isDevMode())
      console.log(loginForm.value);
  }

  validationForm = (loginForm: FormGroup): boolean => {
    console.log("validation", loginForm.valid)
    if (!loginForm.valid)
      return true;
    else
      return false;
  }
}
