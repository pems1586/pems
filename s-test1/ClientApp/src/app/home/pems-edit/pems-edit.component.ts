import { Component, Inject, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { ApiService } from '../../services/api.service';
import { PEMS } from '../home.component';

@Component({
  selector: 'app-pems-edit',
  templateUrl: './pems-edit.component.html',
  styleUrls: ['./pems-edit.component.scss']
})
export class PemsEditComponent {

  pemsRecord: PEMS | undefined;
  action: any;
  minDate: any = new Date('1-1-1090');

  pemsForm: FormGroup = new FormGroup({});

  constructor(@Inject('BASE_URL') private baseUrl: string, private modalService: BsModalService, private apiService: ApiService) {
    this.pemsForm = new FormGroup({
      FLE_ID: new FormControl(0, []),
      TST_PGM_CDE: new FormControl('', [Validators.required, Validators.maxLength(5)]),
      TST_ADM__TST_DTE: new FormControl('', [Validators.required]),
      SRC_SYS_ID: new FormControl('', [Validators.required]),
      TRGT_SYS_ID: new FormControl('', [Validators.required]),
      FLE_NAM: new FormControl('', [Validators.required]),
      DTA_TYP_NAM: new FormControl('', [Validators.required]),
      FLE_SEQ_NO: new FormControl('', [Validators.required]),
      FILE_TYPE_CODE: new FormControl('', [Validators.required]),
      FLE_PRCSD_DTE: new FormControl('', [Validators.required]),
      TOT_RCD_CNT: new FormControl('', [Validators.required, Validators.max(999999999)]),
      PPR_BSD_TSTG_MC_RCD_CNT: new FormControl('', [Validators.required, Validators.max(999999999)]),
      PPR_BSD_TSTG_CR_RCD_CNT: new FormControl('', [Validators.required, Validators.max(999999999)]),
      CBT_MC_RCD_CNT: new FormControl('', [Validators.required, Validators.max(999999999)]),
      CBT_CR_RCD_CNT: new FormControl('', [Validators.required, Validators.max(999999999)]),
      FLE_CRETN_DTM: new FormControl('', [Validators.required]),
      LST_UPDT_DTM: new FormControl('', [Validators.required])
    });

    if (!!this.modalService.config && !!this.modalService.config.initialState && !!this.modalService.config.initialState.model) {
      let model: any = this.modalService.config.initialState.model;
      this.pemsRecord = !!model && model.item ? model.item : undefined;
      this.action = !!model && model.action ? model.action : undefined;

      this.pemsForm.patchValue(model.item);
    }
  }

  close() {
    this.modalService.hide();
  }

  onSave() {
    if (this.pemsForm.valid) {
      this.apiService.post(`${this.baseUrl}api/pems`, this.pemsForm.value).subscribe(res => {
        if (!!res) {
          this.modalService.hide();
          this.action();
        }
      })
    } else {
      alert('Please fill all the details.');
    }
  }
}
