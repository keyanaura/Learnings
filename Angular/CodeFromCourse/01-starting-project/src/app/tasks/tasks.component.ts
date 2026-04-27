import { Component, Input } from '@angular/core';
import { TaskComponent } from './task/task.component';

import { NewTaskComponent } from './new-task/new-task.component';
import { NewTaskModel } from './task/NewTask.model';
import { TasksService } from './tasks.service';
import { flatMap } from 'rxjs';

@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [TaskComponent, NewTaskComponent],
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
