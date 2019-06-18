import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatTableDataSource, MatSnackBar } from '@angular/material';
import { first } from 'rxjs/operators';
import { FormService } from '../../services/form.service';
import { FormModel } from '../../models/candidate/fom.model';
import { TaskModel } from '../../models/candidate/task.model';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { UserRoleEnum } from '../../enums/user-role.enum';
import { UserStatusEnum } from '../../enums/user-status.enum';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent implements OnInit {

  dataSource = new MatTableDataSource();
  displayedColumns = [ 'task', 'updated', 'file', 'actions'];
  form:FormModel = null;
  isLoading:boolean[];
  isLoadingPage: boolean = false;
  isAdmin: boolean = false;
  userStatus: string = null;
  constructor(
    private formService: FormService,
    private router: Router,
    private snackBar: MatSnackBar,
    private auth: AuthService
  ) { }

  ngOnInit() {
    this.getStatus();
    this.getForm();
  }

  getForm()
  {
    const today = new Date();
    this.isLoadingPage = true;
    this.formService.getForm()
    .pipe(first())
    .subscribe(dn => {
      this.form = dn as FormModel;
      const data = [];
      data.push({id: 0,name: 'מילוי פרטים אישיים', updated: "", fileName: "", link: "profile", isForm: true});
      data.push({id: 0, name: this.form.parentsSalary.name, updated: this.form.parentsSalary.lastUpdated, fileName: "", link: "parentsSalary", isForm: true});
      this.isLoading = new Array(this.form.tasks.length + 1);
      this.isLoading[0] = false;
      this.form.tasks.forEach((element:TaskModel)=>{
        data.push({
          id: element.id,
          name: element.name,
          updated: element.lastUpdated,
          fileName: element.fileName,
          isForm: false
        });
        this.isLoading[element.id] = false;
      });
      this.dataSource.data = data;
      this.isLoadingPage = false;
    });
  }

  upload(event, id)
  {
    if(event.target.files && event.target.files.length) {
      this.isLoading[id] = true;
      let task = {id:id, file:event.target.files[0]} as TaskModel;
      this.formService.updateTask(task).subscribe((data)=>{
        this.isLoading[id] = false;
        this.dataSource.data.forEach(element =>{
          if(element['id'] == id)
          {
            element['updated'] = data.lastUpdated;
            element['fileName'] = data.fileName;
          }
        });
      },(err)=>{
        this.isLoading[id] = false;
      });
    }
  }

  private getStatus() {
    this.auth.currentUser.subscribe((user) => {
      const role: any = !!user ? user.userRole : null;
      this.isAdmin = !!user && role === 'Admin' ? true : false;
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

  submit() {
    this.formService.submitToReview().subscribe(() => {
      this.snackBar.open("הבקשה נשלחה בהצלחה", "",{duration : 4000});
    },  
    err => {});
  }

  redirectTo(link: string) {
    this.router.navigate([link]);
  }
}
