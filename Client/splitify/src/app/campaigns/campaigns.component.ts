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

  onCampaignCreated(id: string) {
    this.displayCreateForm = false;

    let campaign = undefined;
    this.campaignService.fetchCampaign(id)
      .then(response => response.json())
      .then(json => {
        campaign = json;
        this.campaigns = [campaign, ...this.campaigns];
        
        const currentStatus = campaign.status;
        const interval = setInterval(() => {
          this.campaignService.fetchCampaign(campaign.id)
            .then(response => response.json())
            .then(json => campaign = json);
          if (campaign.status !== currentStatus){
            this.campaigns[0] = campaign;
            clearInterval(interval);
          }
        }, 2000);
      });
  }

  onCampaignDeleted(campaign: CampaignGet){
    const index = this.campaigns.findIndex(x => x.id === campaign.id);
    this.campaigns.splice(index, 1);
  }

  onCreateCampaignCanceled(){
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
