import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { CampaignsComponent } from './campaigns/campaigns.component';
import { CommonModule, DatePipe } from '@angular/common';
import { CreateCampaignComponent } from './campaigns/create-campaign/create-campaign.component';
import { FormsModule } from '@angular/forms';
import { CampaignItemComponent } from './campaigns/campaign-item/campaign-item.component';
import { AuthComponent } from './auth/auth.component';

@NgModule({
  declarations: [
    AppComponent,
    CampaignsComponent,
    CreateCampaignComponent,
    CampaignItemComponent,
    AuthComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    FormsModule,
    CommonModule 
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
