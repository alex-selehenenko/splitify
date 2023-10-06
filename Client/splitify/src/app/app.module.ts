import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { CampaignsComponent } from './campaigns/campaigns.component';
import { CommonModule, DatePipe } from '@angular/common';
import { CreateCampaignComponent } from './campaigns/create-campaign/create-campaign.component';
import { FormsModule } from '@angular/forms';
import { CampaignItemComponent } from './campaigns/campaign-item/campaign-item.component';
import { AuthComponent } from './auth/auth.component';
import { VerifyComponent } from './verify/verify.component';
import { AuthInterceptor } from 'src/interceptors/auth.interceptor';
import { ResetPasswordComponent } from './auth/reset-password/reset-password.component';

@NgModule({
  declarations: [
    AppComponent,
    CampaignsComponent,
    CreateCampaignComponent,
    CampaignItemComponent,
    AuthComponent,
    VerifyComponent,
    ResetPasswordComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    FormsModule,
    CommonModule 
  ],
  providers: [
    DatePipe,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
