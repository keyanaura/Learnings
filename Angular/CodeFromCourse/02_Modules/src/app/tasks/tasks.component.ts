import { Component, Input } from '@angular/core'; 
import { TasksService } from './tasks.service'; 

@Component({
  selector: 'app-tasks', 
  templateUrl: './tasks.component.html',
  styleUrl: './tasks.component.css',
})
export class TasksComponent {
  @Input({ required: true }) userId!: string;
  @Input({ required: true }) Name!: string;
  isAddingTask = false;

  constructor(private taskService: TasksService) {}

  get SelectedUserTasks() {
    return this.taskService.getUserTasks(this.userId);
  }
   
  OnStartAddTask() {
    this.isAddingTask = true;
  }
  OnCancelCliked() {
    this.isAddingTask = false;
  }
  
}
