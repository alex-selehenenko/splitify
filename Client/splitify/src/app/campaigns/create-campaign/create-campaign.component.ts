import { Component, EventEmitter, Output } from '@angular/core';
import { CampaignPost } from 'src/core/models/campaign.post.model';
import { CampaignService } from 'src/core/services/campaign.service';

@Component({
  selector: 'app-create-campaign',
  templateUrl: './create-campaign.component.html',
  styleUrls: ['./create-campaign.component.css']
})
export class CreateCampaignComponent {
  @Output() campaignCreated: EventEmitter<string> = new EventEmitter<string>()

  @Output() canceled: EventEmitter<void> = new EventEmitter();

  errorMessage = '';

  constructor(private campaignService: CampaignService){}
  
  onSubmit(form){
    this.errorMessage = '';
    let campaign = new CampaignPost();
    campaign.name = form.value.name;
    campaign.destinations = [form.value.destinationA, form.value.destinationB];

    this.campaignService.postCampaign(campaign)
      .subscribe({
        next: json => this.campaignCreated.emit(json.campaignId),
        error: err => {
          this.errorMessage = err.error === undefined || err.error === null
            ? 'Something went wrong. Please, try later.'
            : err.error.detail;
        }
      });
  }
}
