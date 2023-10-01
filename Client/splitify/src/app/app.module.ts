import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { CampaignsComponent } from './campaigns/campaigns.component';
import { CampaignItemComponent } from './campaigns/campaign-item/campaign-item.component';

@NgModule({
  declarations: [
    AppComponent,
    CampaignsComponent,
    CampaignItemComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
