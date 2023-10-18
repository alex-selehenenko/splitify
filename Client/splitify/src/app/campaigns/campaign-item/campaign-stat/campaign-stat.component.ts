import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CampaignGet } from 'src/core/models/campaign.get.model';
import { DestinationStat } from 'src/core/models/destination-stat.get.model';
import { StatisticsService } from 'src/core/services/statistics.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-campaign-stat',
  templateUrl: './campaign-stat.component.html',
  styleUrls: ['./campaign-stat.component.css']
})
export class CampaignStatComponent implements OnInit{
  
  @Output() statClosed: EventEmitter<void> = new EventEmitter<void>();
  @Input() campaign: CampaignGet;

  dataLoaded = false;
  destinationStats: DestinationStat[];

  constructor(private statisticsService: StatisticsService){}

  ngOnInit(): void {
    this.statisticsService
      .fetchStatistics(this.campaign.id)
      .subscribe({
        next: data => {
          this.destinationStats = data;
          this.dataLoaded = true;
        },
        error: err => {
          this.dataLoaded = true;
        }
      });
  }

  getRedirectUrl(){
    return environment.redirectUrl + this.campaign.id;
  }

  onClose(){
    this.statClosed.emit();
  }
}
