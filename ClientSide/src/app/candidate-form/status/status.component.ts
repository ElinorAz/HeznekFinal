import { Component, OnInit } from '@angular/core';
import { FormService } from '../../services/form.service';
import { first } from 'rxjs/internal/operators/first';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-status',
  templateUrl: './status.component.html',
  styleUrls: ['./status.component.scss']
})
export class StatusComponent implements OnInit {
  isLoading: boolean = false;
  status: number = null;
  userStatus = null;
  constructor(
    private formService: FormService,
    private auth: AuthService
  ) { }

  ngOnInit() {
    this.getStatus();
    this.isLoading = true;
    this.formService.getForm()
    .pipe(first())
    .subscribe(form => {
      this.isLoading = false;
      this.status = form.status;
    },
    err => this.isLoading = false);
  }


  private getStatus(): void {
    this.auth.currentUser.subscribe((user) => {
      this.userStatus = !!user ? user.userStatus : null;
    });
  }

}
