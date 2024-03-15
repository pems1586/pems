import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDatepickerConfig, BsDatepickerModule } from 'ngx-bootstrap/datepicker';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { CommonModule } from '@angular/common';
import { PemsEditComponent } from './home/pems-edit/pems-edit.component';
import { ConfirmationComponent } from './common/confirmation/confirmation.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NoDBComponent } from './common/nodb/nodb.component';
import { NgxMultiselectModule } from '@ngx-lib/multiselect';
import { ErrorComponent } from './common/error/error.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    PemsEditComponent,
    ConfirmationComponent,
    NoDBComponent,
    ErrorComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
    ]),
    CommonModule,
    ModalModule.forRoot(),
    ReactiveFormsModule,
    BsDatepickerModule.forRoot(),
    BrowserAnimationsModule,
    NgxMultiselectModule
  ],
  providers: [BsDatepickerConfig],
  exports: [PemsEditComponent, ConfirmationComponent, NoDBComponent, ErrorComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
