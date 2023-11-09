import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { ConfirmationComponent } from '../common/confirmation/confirmation.component';
import { ApiService } from '../services/api.service';
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
  selectedDelete: PEMS | undefined;
  selectedAll: boolean = false;

  fileidOptions: Array<number> = [];
  sourceOptions: Array<string> = [];
  targetOptions: Array<string> = [];
  filenameOptions: Array<string> = [];
  datatypeOptions: Array<string> = [];

  constructor(public http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private modalService: BsModalService,
    private apiService: ApiService) {
    this.getResources();
  }

  getResources() {
    this.apiService.get(`${this.baseUrl}api/pems`).subscribe((result: any) => {
      result.forEach((item: PEMS) => {
        item.isSelected = false;
      })
      this.pemsRecords = result;
      this.filteredRecords = result;

      this.fileidOptions = this.pemsRecords.filter(_ => !!_.FLE_ID).map(item => item.FLE_ID).filter((item, index, arr) => arr.indexOf(item) === index);
      this.sourceOptions = this.pemsRecords.filter(_ => !!_.SRC_SYS_ID).map(item => item.SRC_SYS_ID as string).filter((item, index, arr) => arr.indexOf(item) === index);
      this.targetOptions = this.pemsRecords.filter(_ => !!_.TRGT_SYS_ID).map(item => item.TRGT_SYS_ID as string).filter((item, index, arr) => arr.indexOf(item) === index);
      this.filenameOptions = this.pemsRecords.filter(_ => !!_.FLE_NAM).map(item => item.FLE_NAM as string).filter((item, index, arr) => arr.indexOf(item) === index);
      this.datatypeOptions = this.pemsRecords.filter(_ => !!_.FILE_TYPE_CODE).map(item => item.FILE_TYPE_CODE as string).filter((item, index, arr) => arr.indexOf(item) === index);
    }, (error: any) => { });
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
    }
  }

  createNew() {
    const initialState = {
      model: {
        item: { ID: 0 },
        action: this.onSave.bind(this)
      }
    };
    let config: ModalOptions = {
      class: 'modal-lg modal-dialog-centered',
      ignoreBackdropClick: true,
      initialState
    }
    this.modalService.show(PemsEditComponent, config);
  }

  onDeleteSelected() {
    if (!!this.filteredRecords && !!this.filteredRecords.filter(_ => !!_.isSelected).length) {
      this.filteredRecords.filter(_ => !!_.isSelected).forEach(item => {
        this.selectedDelete = item;
        this.deleteItem();
      })
    }
  }

  selectAllToggle() {
    this.filteredRecords.forEach(item => {
      item.isSelected = this.selectedAll;
    })
  }

  isDisableDelete() {
    return !this.filteredRecords.filter(_ => !!_.isSelected).length;
  }

  onDeleteBulk() {
    const initialState = {
      model: {
        action: this.onDeleteSelected.bind(this)
      }
    };
    let config: ModalOptions = {
      class: 'modal-md modal-dialog-centered',
      ignoreBackdropClick: true,
      initialState
    }
    this.modalService.show(ConfirmationComponent, config);
  }

  onDelete(item: PEMS) {
    const initialState = {
      model: {
        item: item.FLE_ID,
        action: this.deleteItem.bind(this)
      }
    };
    let config: ModalOptions = {
      class: 'modal-md modal-dialog-centered',
      ignoreBackdropClick: true,
      initialState
    }
    this.modalService.show(ConfirmationComponent, config);
    this.selectedDelete = item;
  }

  onSearch() {
    if (!!this.pemsRecords && !!this.pemsRecords.length) {
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
        item: record,
        action: this.onSave.bind(this)
      }
    };
    let config: ModalOptions = {
      class: 'modal-lg modal-dialog-centered',
      ignoreBackdropClick: true,
      initialState
    }
    this.modalService.show(PemsEditComponent, config);
  }

  onSave() {
    this.getResources();
  }

  deleteItem() {
    if (!!this.selectedDelete && this.selectedDelete.FLE_ID) {
      this.apiService.delete(`${this.baseUrl}api/pems?id=${this.selectedDelete.FLE_ID}`).subscribe((res: any) => {
        if (!!res) {
          this.getResources();
        }
      });
    }
  }
}

export interface PEMS {
  FLE_ID: number;
  TST_PGM_CDE: string;
  TST_ADM__TST_DTE: Date;
  SRC_SYS_ID: string;
  TRGT_SYS_ID: string;
  FLE_NAM: string;
  DTA_TYP_NAM: string;
  FLE_SEQ_NO: string;
  FILE_TYPE_CODE: string;
  FLE_PRCSD_DTE: Date;
  TOT_RCD_CNT: number;
  PPR_BSD_TSTG_MC_RCD_CN: number;
  PPR_BSD_TSTG_CR_RCD_CNT: number;
  CBT_MC_RCD_CNT: number;
  CBT_CR_RCD_CNT: number;
  FLE_CRETN_DTM: Date;
  LST_UPDT_DTM: Date;
  isSelected: boolean;
}

interface ISelectedFilters {
  TST_PGM_CDE: Array<string>;
  SRC_SYS_ID: Array<string>;
  TRGT_SYS_ID: Array<string>;
  FLE_NAM: Array<string>;
  DTA_TYP_NAM: Array<string>;
}
