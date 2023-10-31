import { HttpClient } from '@angular/common/http';
import { Component, Inject, Input } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-confirmation',
  templateUrl: './confirmation.component.html',
  styleUrls: ['./confirmation.component.scss']
})
export class ConfirmationComponent {

  data: any;
  action: any;

  constructor(private modalService: BsModalService) {
    if (!!this.modalService.config && !!this.modalService.config.initialState && !!this.modalService.config.initialState.model) {
      let model: any = this.modalService.config.initialState.model;

      this.data = !!model.item ? model.item : undefined;
      this.action = !!model.action ? model.action : undefined;
    }
  }

  onConfirm() {
    if (!!this.action)
      this.action(this.data);

    this.close();
  }

  close() {
    this.modalService.hide();
  }
}
