import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';


import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';

import { AppComponent } from './app.component';

import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { StoreModule } from '@ngrx/store';
import { appReducer } from './state/app.reducer';
import { EffectsModule } from '@ngrx/effects';
import { AppEffect } from './state/app.effect';
import { Dispatcher } from './state/dispatcher';
import { AppService } from './app.service';
import { AppSelector } from './state/app.selector';
import { AuthInterceptor } from './auth.interceptor';

@NgModule({
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ComponentsModule,
    RouterModule,
    AppRoutingModule,
    // Use ngrx store and effect to loose coupling between component and external resource
    StoreModule.forRoot({ app: appReducer}),
    EffectsModule.forRoot([AppEffect])
  ],
  declarations: [
    AppComponent,
    AdminLayoutComponent
  ],
  providers: [
    Dispatcher,
    AppService,
    AppSelector,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
