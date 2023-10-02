import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { CampaignsComponent } from './campaigns/campaigns.component';
import { CommonModule, DatePipe } from '@angular/common';
import { CreateCampaignComponent } from './campaigns/create-campaign/create-campaign.component';
import { FormsModule } from '@angular/forms';
import { CampaignItemComponent } from './campaigns/campaign-item/campaign-item.component';

@NgModule({
  declarations: [
    AppComponent,
    CampaignsComponent,
    CreateCampaignComponent,
    CampaignItemComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    CommonModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
