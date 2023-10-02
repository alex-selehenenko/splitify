import { DatePipe } from '@angular/common';
import { Component, Input, EventEmitter, Output } from '@angular/core';
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
  private readonly statusPreparing = 0;
  private readonly statusActive = 1;
  private readonly statusInactive = 2;

  @Input() campaign: CampaignGet;
  @Output() campaignChanged: EventEmitter<CampaignGet> = new EventEmitter<CampaignGet>();
  
  constructor(private datePipe: DatePipe, private campaignService: CampaignService){}

  onCampaignStatusChanged(event: Event){
    const checkbox = event.target as HTMLInputElement;
    
    let campaignPatch = new CampaignPatch();    
    campaignPatch.status = checkbox.checked ? this.statusInactive : this.statusActive;
    
    this.campaignService.changeCampaignStatus(this.campaign.id, campaignPatch)
      .then(response => {
          this.campaignChanged.emit(this.campaign);
        });
  }

  resolveRedirectUrl(campaign: CampaignGet){
    return environment.redirectUrl + campaign.id;
  }

  resolveStatusName(campaign: CampaignGet){
    switch (campaign.status){
      case this.statusPreparing: return 'Preparing';
      case this.statusActive: return 'Active';
      case this.statusInactive: return 'Inactive';
      default: return 'Inactive';
    }
  }

  resolveStatusClass(campaign: CampaignGet){
    switch (campaign.status){
      case this.statusPreparing: return 'status-preparing';
      case this.statusActive: return 'status-active';
      case this.statusInactive: return 'status-inactive';
      default: return 'status-inactive';
    }
  }

  resolveCheckboxStatus(campaign: CampaignGet){
    return campaign.status === this.statusPreparing || campaign.status === this.statusActive;
  }

  resolveDateTime(inputDate: Date): string {
    return this.datePipe.transform(inputDate, 'dd.MM.yy HH:mm') || '';
  }
}
