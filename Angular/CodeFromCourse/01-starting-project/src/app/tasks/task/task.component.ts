import { Component, Input } from '@angular/core';
import { DatePipe } from '@angular/common';
import { type Task } from './task.model';
import { CardComponent } from '../../shared/card/card.component';
import { TasksService } from '../tasks.service';

@Component({
  selector: 'app-task',
  standalone: true,
  imports: [CardComponent, DatePipe],
  templateUrl: './task.component.html',
  styleUrl: './task.component.css',
})
export class TaskComponent {
  constructor(private tasksService: TasksService) {}

  @Input({ required: true }) task!: Task;

  onCompleteClick() {
    // this.completeTask.emit(this.task.id);
    this.tasksService.removeTask(this.task.id);
  }
}
