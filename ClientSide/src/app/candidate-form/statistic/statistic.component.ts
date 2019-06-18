import { Component, OnInit, Input } from '@angular/core';
import { MatSnackBar } from '@angular/material';

import { first } from 'rxjs/operators';

import { GenderEnum } from '../../enums/gender.enum';
import { UserStatusEnum } from '../../enums/user-status.enum';
import { StatisticService } from '../../services/statistic.service';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-statistic',
  templateUrl: './statistic.component.html',
  styleUrls: ['./statistic.component.scss']
})
export class StatisticComponent implements OnInit {
  isLoading:boolean = false;
  statistic;
  chartLabels = [];
  chartData = [];
  chartType:string = 'doughnut';

  constructor(
    private statisticService: StatisticService
    ) {
  }

  ngOnInit() {
    this.isLoading = true;
    this.statisticService.getStatistic().subscribe((data) => {
      this.statistic = data;
      this.generateGraphData();
      this.generateGraphLabels();
      this.isLoading = false;
    },
    err => {
      this.isLoading = false;
    })
  }

  generateGraphData(): void {
    this.statistic.users.forEach(item => {
        this.chartData.push(item.value);
    });
  }

  generateGraphLabels(): void {
    this.statistic.users.forEach(item => {
        this.chartLabels.push(UserStatusEnum[item.key]);
    });
  }

  getEnumVal(val: number) {
    return GenderEnum[val];
  }
}
