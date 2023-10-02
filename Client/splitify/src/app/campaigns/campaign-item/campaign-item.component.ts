import { DatePipe } from '@angular/common';
import { Component, Input } from '@angular/core';
import { CampaignGet } from 'src/core/models/campaign.get.model';
import { CampaignPatch } from 'src/core/models/campaign.patch.model';
import { CampaignService } from 'src/core/services/campaign.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-campaign-item',
  templateUrl: './campaign-item.component.html',
  styleUrls: ['./campaign-item.component.css']
})
export class CampaignItemComponent {
  @Input() campaign: CampaignGet;

  constructor(private datePipe: DatePipe, private campaignService: CampaignService){}

  onCampaignStatusChanged(event: Event){
    const checkbox = event.target as HTMLInputElement;
    let campaignPatch = new CampaignPatch();
    
    if (!checkbox.checked){
        campaignPatch.status = 2;
        this.campaignService.deactivateCampaign(this.campaign.id, campaignPatch);
    }
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

  resolveDateTime(inputDate: Date): string {
    return this.datePipe.transform(inputDate, 'dd.MM.yy HH:mm') || '';
  }
}
