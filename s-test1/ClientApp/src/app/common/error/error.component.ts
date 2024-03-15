import { Component, Input } from '@angular/core';
import { AbstractControl, AbstractControlDirective } from '@angular/forms';

@Component({
  selector: 'form-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.scss']
})

export class ErrorComponent {
  @Input() controlName: AbstractControl | AbstractControlDirective | undefined

  errorMessage: any = {
    required: () => `This field is required`,
    maxlength: (params: any) => `Maximum ${params.requiredLength} characters are allowed`,
    minlength: (params: any) => `Minimum ${params.requiredLength} characters are required`,
    min: (params: any) => `Minimum value must be ${params.min}`,
    max: (params: any) => `Maximum value must be ${params.max}`
  };

  getError() {
    if (!this.controlName)
      return '';

    if (this.controlName.errors) {
      let errorMsg = '';
      let error = Object.keys(this.controlName.errors)[0];
      if (!!this.controlName)
        errorMsg = (this.controlName.touched || this.controlName.dirty) ? this.errorMessage[error](this.controlName.errors[error]) : '';

      return errorMsg;
    }
    else {
      return '';
    }
  }
}
