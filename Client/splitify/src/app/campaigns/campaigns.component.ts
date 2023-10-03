import { Component, HostBinding, OnInit } from '@angular/core';
import { CampaignGet } from 'src/core/models/campaign.get.model';
import { CampaignService } from 'src/core/services/campaign.service';

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

  onCampaignChanged(campaign: CampaignGet){
    this.campaignService.fetchCampaign(campaign.id)
    .then(response => response.json())
    .then(json => {
        const index = this.campaigns.findIndex(x => x.id === campaign.id);
        this.campaigns[index] = json;
    });
  }

  onCreateCampaignDeclined(){
    this.displayCreateForm = false;
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
