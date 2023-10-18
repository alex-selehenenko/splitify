import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { CampaignPost } from "../models/campaign.post.model";
import { CampaignPatch } from "../models/campaign.patch.model";
import { HttpClient } from "@angular/common/http";
import { CampaignGet } from "../models/campaign.get.model";
import { Observable } from "rxjs";
import { DestinationStat } from "../models/destination-stat.get.model";

@Injectable({ providedIn: 'root' })
export class StatisticsService{

    constructor(private httpClient: HttpClient){}

    fetchStatistics(campaignId: string): Observable<DestinationStat[]>{
        return this.httpClient.get<DestinationStat[]>(
            environment.statisticsApiUrl + 
            'link/' +
            campaignId);
    }
}