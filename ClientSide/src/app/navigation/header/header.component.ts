import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from 'src/app/services/auth.service';

import { UserRoleEnum } from '../../enums/user-role.enum';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  isLogged: boolean = false;
  isAdmin: boolean;
  isSemiAdmin: boolean;
  isStudent: boolean;
  
  constructor(
    private auth: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
    this.auth.currentUser.subscribe((user) => {
      this.isLogged = !!user;
      const role: any = !!user ? user.userRole : null;
      this.isSemiAdmin = !!user && role === 'SemiAdmin' ? true : false;
      this.isAdmin = !!user && role === 'Admin' ? true : false;
      const status = !!user ? user.userStatus : null;
      if (status == 3 || status == 4 || status == 5 || status == 6) {
        this.isStudent = true;
      }
    });
  }

  logout() {
    this.auth.logout();
    this.router.navigate(["login"]);
  }

  profile() {
    this.isAdmin ? this.router.navigate(["adminSystemDetails"]) : this.router.navigate(["profile"]);
  }

  status() {
    this.router.navigate(["status"]);
  }

  scholarships() {
    this.router.navigate(["scholarships"]);
  }

  candidates() {
    this.router.navigate(["candidates"]);
  }

  students() {
    this.router.navigate(["students"]);
  }

  messages() {
    this.router.navigate(["messages"]);
  }

  events() {
    this.router.navigate(["events"]);
  }

  form() {
    this.router.navigate(["form"]);
  }

}
