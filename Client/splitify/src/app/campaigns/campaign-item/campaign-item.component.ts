import { Component, Input } from '@angular/core';
import { Campaign } from 'src/core/models/campaign.model';

@Component({
  selector: 'app-campaign-item',
  templateUrl: './campaign-item.component.html',
  styleUrls: ['./campaign-item.component.css']
})
export class CampaignItemComponent {
  @Input() campaign : Campaign;

  resolveRedirectUrl(){
    return 'https://splitify.com/' + this.campaign.id;
  }

  resolveStatusName(){
    switch (this.campaign.status){
      case 0: return 'Preparing';
      case 1: return 'Active';
      case 2: return 'Inactive';
      default: return 'Inactive';
    }
  }

  resolveStatusClass(){
    switch (this.campaign.status){
      case 0: return '.status-active';
      case 1: return '.status-preparing';
      case 2: return '.status-inactive';
      default: return '.status-inactive';
    }
  }
}
