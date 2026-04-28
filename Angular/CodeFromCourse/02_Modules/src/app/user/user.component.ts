import { Component, EventEmitter, input, Input, Output } from '@angular/core';
import { User } from './user.model'; 
// type User = {
//   id: string;
//   avatar: string;
//   name: string;
// };

@Component({
  selector: 'app-user',   
  templateUrl: './user.component.html',
  styleUrl: './user.component.css',
})
export class UserComponent {
  // @Input({ required: true }) User!: {
  //   id: string;
  //   avatar: string;
  //   name: string;
  // };

  // @Input({ required: true }) Id!: string;
  // @Input({ required: true }) Avatar!: string;
  // @Input({ required: true }) Name!: string;

  @Input({ required: true }) user!: User;
  @Output() selectedUser = new EventEmitter();
  @Input({ required: true }) selected!: boolean; 
  get imagePath() {
    return 'assets/users/' + this.user.avatar;
  }

  onSelectedUser() {
    this.selectedUser.emit(this.user.id); 
  }
}
