import { Component, OnInit } from '@angular/core';
import { Campaign } from 'src/core/models/campaign.model';
import { CampaignService } from 'src/core/services/campaign.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-campaigns',
  templateUrl: './campaigns.component.html',
  styleUrls: ['./campaigns.component.css']
})
export class CampaignsComponent implements OnInit{
  campaigns: Campaign[];

  constructor(private campaignService: CampaignService){}

  ngOnInit(){
    this.campaignService.fetchCampaigns()
      .then(data => data.json())
      .then(json => this.campaigns = json);
  }

  resolveRedirectUrl(campaign: Campaign){
    return environment.redirectUrl + campaign.id;
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
