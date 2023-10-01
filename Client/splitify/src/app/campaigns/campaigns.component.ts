import { Component, OnInit } from '@angular/core';
import { CampaignGet } from 'src/core/models/campaign.get.model';
import { CampaignService } from 'src/core/services/campaign.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-campaigns',
  templateUrl: './campaigns.component.html',
  styleUrls: ['./campaigns.component.css']
})
export class CampaignsComponent implements OnInit{
  campaigns: CampaignGet[];
  displayCreateForm = false;

  constructor(private campaignService: CampaignService){}

  ngOnInit(){
    this.fetchCampaigns();
  }

  onCreateCampaign(){
    this.displayCreateForm = true;
  }

  onCampaignCreated() {
    this.fetchCampaigns();
  }

  onCreateCampaignDeclined(){
    this.displayCreateForm = false;
  }

  resolveRedirectUrl(campaign: CampaignGet){
    return environment.redirectUrl + campaign.id;
  }

  resolveStatusName(campaign: CampaignGet){
    switch (campaign.status){
      case 0: return 'Preparing';
      case 1: return 'Active';
      case 2: return 'Inactive';
      default: return 'Inactive';
    }
  }

  resolveStatusClass(campaign: CampaignGet){
    switch (campaign.status){
      case 0: return 'status-preparing';
      case 1: return 'status-active';
      case 2: return 'status-inactive';
      default: return 'status-inactive';
    }
  }

  resolveCheckboxStatus(campaign: CampaignGet){
    return campaign.status === 0 || campaign.status === 1;
  }

  private fetchCampaigns(){
    this.campaignService.fetchCampaigns()
      .then(data => data.json())
      .then(json => {
        this.campaigns = json;
        this.displayCreateForm = false;
      });
  }
}
