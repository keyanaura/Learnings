import { Component, Input } from '@angular/core'; 
import { DUMMY_USERS } from './dummy-users'; 

type ClickedUser = {
  id: string;
  avatar: string;
  name: string;
};

@Component({
  selector: 'app-root',
  standalone: false, 
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  @Input({ required: true }) clickedUser?: ClickedUser;
  // clickedUser!: string;
  users = DUMMY_USERS;

  onSelectUser(id: string) {
    this.clickedUser = DUMMY_USERS.find((x) => x.id == id);
    console.log('selected user with id ' + id);
  }
}
