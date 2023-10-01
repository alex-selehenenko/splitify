import { Component } from '@angular/core';
import { Campaign } from 'src/core/models/campaign.model';

@Component({
  selector: 'app-campaigns',
  templateUrl: './campaigns.component.html',
  styleUrls: ['./campaigns.component.css']
})
export class CampaignsComponent {
  campaigns: Campaign[];

  constructor(){
    let c1 = new Campaign();
    c1.id = 'bdyujw23erS';
    c1.links = ['https://powercoders.org', 'https://google.com'];
    c1.name = 'First Campaign';
    c1.status = 2;

    let c2 = new Campaign();
    c2.id = 'bdyujw23erS';
    c2.links = ['https://powercoders.org', 'https://google.com'];
    c2.name = 'First Campaign';
    c2.status = 1;

    let c3 = new Campaign();
    c3.id = 'bdyujw23erS';
    c3.links = ['https://powercoders.org', 'https://google.com'];
    c3.name = 'First Campaign';
    c3.status = 2;

    this.campaigns = [c1, c2, c3];
  }

  resolveRedirectUrl(campaign: Campaign){
    return 'https://splitify.com/' + campaign.id;
  }

  resolveStatusName(campaign: Campaign){
    switch (campaign.status){
      case 0: return 'Preparing';
      case 1: return 'Active';
      case 2: return 'Inactive';
      default: return 'Inactive';
    }
  }

  resolveStatusClass(campaign: Campaign){
    switch (campaign.status){
      case 0: return 'status-preparing';
      case 1: return 'status-active';
      case 2: return 'status-inactive';
      default: return 'status-inactive';
    }
  }

  resolveCheckboxStatus(campaign: Campaign){
    return campaign.status === 0 || campaign.status === 1;
  }
}
