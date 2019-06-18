import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators, FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { MessagesService } from '../../services/messages.service';
import { MessageModel } from '../../models/message/message.model';
import { UserStatusEnum } from '../../enums/user-status.enum';

@Component({
  selector: 'app-new-message',
  templateUrl: './new-message.component.html',
  styleUrls: ['./new-message.component.scss']
})
export class NewMessageComponent implements OnInit {
  messageForm: FormGroup;
  isLoading: boolean = false;
  isLoadedItems: boolean = false;
  fileName: string;
  file: File = null;
  userStatuses: { id: number; value: string }[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar,
    private messagesService: MessagesService
  ) {
    this.initEnums();
    this.buildForm();
  }

  get formData() { 
    return <FormArray>this.messageForm.get('sendtowho'); 
  }

  private buildForm() {
    const recipientsControls = this.userStatuses.map(control => new FormControl(false));
    this.messageForm = this.formBuilder.group({
      topic: ['', [Validators.required]],
      content: ['', [Validators.required]],
      file: [''],
      sendtowho: new FormArray(recipientsControls)
    });
  }


  private initEnums() {
    for (const i in UserStatusEnum) {
      if (typeof UserStatusEnum[i] === 'number') {
        this.userStatuses.push({ id: <any>UserStatusEnum[i], value: i });
      }
    }
  }

  ngOnInit() {

  }

  uploadFile(event) {
    if (event.target.files && event.target.files.length) {
      this.fileName = event.target.files[0].name;
      this.file = event.target.files[0];
    }
  }

  mapRecipients(selectecRecipients) {
    const whoIDs: number[] = [];
    selectecRecipients.forEach((item, i: number) => {
      item ? whoIDs.push(i) : null;
    });

    return whoIDs;
  }

  save() {
    this.isLoading = true;
    let model: MessageModel = this.messageForm.value;
    model.file = this.file;
    model.sendtowho = this.mapRecipients(this.messageForm.value.sendtowho);
    this.messagesService.create(model).subscribe(() => {
      this.isLoading = false;
      this.snackBar.open("ההודעה נשלחה", "", { duration: 2000 });
    }, (err) => {
      this.isLoading = false;
      this.snackBar.open("הפעולה נכשלה", "", { duration: 2000 });
    });
  }

}
