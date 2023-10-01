import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable({ providedIn: 'root' })
export class CampaignService{
    fetchCampaigns(){
        return fetch(environment.campaignServiceApiUrl + 'campaigns');
    }
}