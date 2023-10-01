import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { CampaignsComponent } from './campaigns/campaigns.component';
import { CommonModule } from '@angular/common';
import { CreateCampaignComponent } from './campaigns/create-campaign/create-campaign.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    CampaignsComponent,
    CreateCampaignComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    CommonModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
