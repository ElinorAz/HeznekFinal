import { Component, OnInit, Input } from '@angular/core';

import { CSVService } from 'src/app/services/csv.service';

@Component({
  selector: 'app-csv',
  templateUrl: './csv.component.html',
  styleUrls: ['./csv.component.scss']
})
export class CSVComponent implements OnInit {
  @Input() items = [];
  isLoading: boolean = false;

  constructor(
    private csvService: CSVService
  ) { }

  ngOnInit() {

  }

  downloadCSV(): void {
    const ids = this.getCSVItemsIDS(this.items);
    if (ids && ids.length) {
      this.isLoading = true;
      this.csvService.downloadCSV(ids)
      .subscribe((data) => {
        const downloadLink = document.createElement('a');
        downloadLink.style.display = 'none';
        document.body.appendChild(downloadLink);
        downloadLink.setAttribute('href', window.URL.createObjectURL(data));
        downloadLink.setAttribute('download', `List ${Date.now()}.csv`);
        downloadLink.click();
        document.body.removeChild(downloadLink);
        this.isLoading = false;
      },
      err => {
        this.isLoading = false;
      });
    }
  }

  private getCSVItemsIDS(list): number[] {
    if (list && list.filteredData) {
      return list.filteredData.map((item) => {
        return item.id;
      });
    }
  }
}
