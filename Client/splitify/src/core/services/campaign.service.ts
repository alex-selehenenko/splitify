import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { CampaignPost } from "../models/campaign.post.model";

@Injectable({ providedIn: 'root' })
export class CampaignService{
    fetchCampaigns(){
        return fetch(environment.campaignServiceApiUrl + 'campaigns');
    }

    postCampaign(campaign: CampaignPost){
        return fetch(environment.campaignServiceApiUrl + 'campaigns', {
            method: "POST",
            body: JSON.stringify(campaign),
            headers: {
              "Content-type": "application/json; charset=UTF-8"
            }
          });
    }
}