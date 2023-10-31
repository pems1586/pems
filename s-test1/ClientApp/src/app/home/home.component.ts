import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { PemsEditComponent } from './pems-edit/pems-edit.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  pemsRecords: Array<PEMS> = [];
  filteredRecords: Array<PEMS> = [];
  selectedFilters: ISelectedFilters = { TST_PGM_CDE: [], SRC_SYS_ID: [], TRGT_SYS_ID: [], FLE_NAM: [], DTA_TYP_NAM: [] };

  fileidOptions: Array<string> = [];
  sourceOptions: Array<string> = [];
  targetOptions: Array<string> = [];
  filenameOptions: Array<string> = [];
  datatypeOptions: Array<string> = [];

  search: string = '';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private modalService: BsModalService) {
    http.get<PEMS[]>(baseUrl + 'api/pems').subscribe(result => {
      result.forEach(item => {
        item.isSelected = false;
      })
      this.pemsRecords = result;
      this.filteredRecords = result; 

      this.fileidOptions = this.pemsRecords.filter(_ => !!_.FLE_ID).map(item => item.FLE_ID as string);
      this.sourceOptions = this.pemsRecords.filter(_ => !!_.SRC_SYS_ID).map(item => item.SRC_SYS_ID as string);
      this.targetOptions = this.pemsRecords.filter(_ => !!_.TRGT_SYS_ID).map(item => item.TRGT_SYS_ID as string);
      this.filenameOptions = this.pemsRecords.filter(_ => !!_.FLE_NAM).map(item => item.FLE_NAM as string);
      this.datatypeOptions = this.pemsRecords.filter(_ => !!_.FILE_TYPE_CODE).map(item => item.FILE_TYPE_CODE as string);

    }, error => console.error(error));
  }

  onFilterChange(data: any, type: string) {
    if (!!type) {
      switch (type) {
        case 'program':
          this.selectedFilters.TST_PGM_CDE = data;
          break;

        case 'source':
          this.selectedFilters.SRC_SYS_ID = data;
          break;

        case 'target':
          this.selectedFilters.TRGT_SYS_ID = data;
          break;

        case 'filename':
          this.selectedFilters.FLE_NAM = data;
          break;

        case 'datatypename':
          this.selectedFilters.DTA_TYP_NAM = data;
          break;
      }

      this.onSearch();
    }
  }

  onDeleteSelected() {

  }

  onSearch() {
    if (this.search && !!this.pemsRecords && !!this.pemsRecords.length) {
      this.filteredRecords = [...this.pemsRecords].filter(item =>
        (!!this.selectedFilters.TST_PGM_CDE && this.selectedFilters.TST_PGM_CDE.length ? this.selectedFilters.TST_PGM_CDE.includes(item.TST_PGM_CDE) : true)
        && (!!this.selectedFilters.SRC_SYS_ID && this.selectedFilters.SRC_SYS_ID.length ? this.selectedFilters.SRC_SYS_ID.includes(item.SRC_SYS_ID) : true)
        && (!!this.selectedFilters.TRGT_SYS_ID && this.selectedFilters.TRGT_SYS_ID.length ? this.selectedFilters.TRGT_SYS_ID.includes(item.TRGT_SYS_ID) : true)
        && (!!this.selectedFilters.FLE_NAM && this.selectedFilters.FLE_NAM.length ? this.selectedFilters.FLE_NAM.includes(item.FLE_NAM) : true)
        && (!!this.selectedFilters.DTA_TYP_NAM && this.selectedFilters.DTA_TYP_NAM.length ? this.selectedFilters.DTA_TYP_NAM.includes(item.DTA_TYP_NAM) : true)
      );
    } else {
      this.filteredRecords = JSON.parse(JSON.stringify(this.pemsRecords));
    }
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

interface ISelectedFilters {
  TST_PGM_CDE: Array<string>;
  SRC_SYS_ID: Array<string>;
  TRGT_SYS_ID: Array<string>;
  FLE_NAM: Array<string>;
  DTA_TYP_NAM: Array<string>;
}
