import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { CampaignPost } from "../models/campaign.post.model";
import { CampaignPatch } from "../models/campaign.patch.model";
import { HttpClient } from "@angular/common/http";
import { CampaignGet } from "../models/campaign.get.model";
import { Observable } from "rxjs";

@Injectable({ providedIn: 'root' })
export class CampaignService{

    constructor(private httpClient: HttpClient){}

    fetchCampaigns(): Observable<CampaignGet[]>{
      return this.httpClient.get<CampaignGet[]>(environment.campaignServiceApiUrl + 'campaigns');
    }

    deleteCampaign(id: string): Observable<any>{
      return this.httpClient.delete(environment.campaignServiceApiUrl + 'campaigns/' + id);
    }

    postCampaign(campaign: CampaignPost): Observable<any>{
        return this.httpClient.post(
          environment.campaignServiceApiUrl + 'campaigns',
          campaign
        );
    }

    fetchCampaign(id: string): Observable<CampaignGet>{
      return this.httpClient.get<CampaignGet>(environment.campaignServiceApiUrl + 'campaigns/' + id);
    }

    changeCampaignStatus(id: string, campaign: CampaignPatch){
      return this.httpClient.patch(environment.campaignServiceApiUrl + 'campaigns/' + id,
        campaign);
    }
}