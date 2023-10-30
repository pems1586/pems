import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { PemsEditComponent } from './pems-edit/pems-edit.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public pemsRecords: Array<PEMS> = [{ CBT_CR_RCD_CNT: '1', isSelected: false } as PEMS];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private modalService: BsModalService) {
    //http.get<PEMS[]>(baseUrl + 'api/pems').subscribe(result => {
    //  this.pemsRecords = result;
    //}, error => console.error(error));
  }

  onSearch() {

  }

  onEdit(record: PEMS) {
    const initialState = {
      model: {
        item: record
      }
    };
    let config: ModalOptions = {
      class: 'modal-lg modal-dialog-centered',
      ignoreBackdropClick: true,
      initialState
    }
    this.modalService.show(PemsEditComponent, config);
  }

  onDelete(record: PEMS) {

  }
}

export interface PEMS {
  FLE_ID: string;
  TST_PGM_CDE: string;
  TST_ADM__TST_DTE: string;
  SRC_SYS_ID: string;
  TRGT_SYS_ID: string;
  FLE_NAM: string;
  DTA_TYP_NAM: string;
  FLE_SEQ_NO: string;
  FILE_TYPE_CODE: string;
  FLE_PRCSD_DTE: string;
  TOT_RCD_CNT: string;
  PPR_BSD_TSTG_MC_RCD_CN: string;
  PPR_BSD_TSTG_CR_RCD_CNT: string;
  CBT_MC_RCD_CNT: string;
  CBT_CR_RCD_CNT: string;
  FLE_CRETN_DTM: string;
  LST_UPDT_DTM: string;
  isSelected: boolean;
}
