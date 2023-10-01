import { Component, EventEmitter, Output } from '@angular/core';
import { CampaignPost } from 'src/core/models/campaign.post.model';
import { CampaignService } from 'src/core/services/campaign.service';

@Component({
  selector: 'app-create-campaign',
  templateUrl: './create-campaign.component.html',
  styleUrls: ['./create-campaign.component.css']
})
export class CreateCampaignComponent {
  @Output() campaignCreated: EventEmitter<CampaignPost> = new EventEmitter<CampaignPost>()

  @Output() declined: EventEmitter<void> = new EventEmitter();

  constructor(private campaignService: CampaignService){}
  
  onSubmit(form){    
    let campaign = new CampaignPost();
    campaign.name = form.value.name;
    campaign.destinations = [form.value.destinationA, form.value.destinationA];

    this.campaignService.postCampaign(campaign)
      .then(response =>
      {
        this.campaignCreated.emit(campaign)
      });
  }
}
