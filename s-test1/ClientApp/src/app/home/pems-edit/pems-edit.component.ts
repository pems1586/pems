import { HttpClient } from '@angular/common/http';
import { Component, Inject, Input } from '@angular/core';
import { BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { PEMS } from '../home.component';

@Component({
  selector: 'app-pems-edit',
  templateUrl: './pems-edit.component.html',
  styleUrls: ['./pems-edit.component.scss']
})
export class PemsEditComponent {

  @Input() pemsRecord: PEMS | undefined;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private modalService: BsModalService) {
    
  }


}
