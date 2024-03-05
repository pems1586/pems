import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { ApiService } from './services/api.service';
import { BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { NoDBComponent } from './common/nodb/nodb.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'app';

  constructor(public http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private apiService: ApiService,
    private modalService: BsModalService,  ) {
    this.initialize();
    }

  initialize() {
    this.apiService.get(`${this.baseUrl}api/pems/candbconnect`).subscribe((result: any) => {
      if (!result) {
        const initialState = {
          model: {
          }
        };
        let config: ModalOptions = {
          class: 'modal-lg modal-dialog-centered',
          ignoreBackdropClick: true,
          initialState
        }
        this.modalService.show(NoDBComponent, config);
      }
    }, (error: any) => { });
  }
  
}
