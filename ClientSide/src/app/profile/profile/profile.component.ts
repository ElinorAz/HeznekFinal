import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { UserStatusEnum } from '../../enums/user-status.enum';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  userStatus: string = null;
  constructor(public auth: AuthService) { 
    
  }

  ngOnInit() {
    this.auth.currentUser.subscribe((user) => {
      const status = !!user ? user.userStatus : null;
      if (user) {
        if (status == 0 || status == 1 || status == 2) {
          this.userStatus = 'candidate';
        }
        else if (status == 3 || status == 4 || status == 5 || status == 6) {
          this.userStatus = 'student';
        }
      }
    });
  }

}
