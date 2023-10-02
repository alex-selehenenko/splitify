import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { CampaignPost } from "../models/campaign.post.model";
import { CampaignPatch } from "../models/campaign.patch.model";

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

    deactivateCampaign(id: string, campaign: CampaignPatch){
      return fetch(environment.campaignServiceApiUrl + 'campaigns/' + id ,{
        method: 'PATCH',
        body: JSON.stringify(campaign),
        headers: {
          "Content-type": "application/json; charset=UTF-8"
        }
      });
    }
}