import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NewTaskModel } from '../task/NewTask.model';
import { TasksService } from '../tasks.service';

@Component({
  selector: 'app-new-task',  
  templateUrl: './new-task.component.html',
  styleUrl: './new-task.component.css',
})
export class NewTaskComponent {
  @Output() close = new EventEmitter();
  @Output() onSubmisstion = new EventEmitter<NewTaskModel>();
  @Input({ required: true }) userId!: string;
  enteredTitle = '';
  enteredSummary = '';
  enteredDueDate = '';

  private tasksService = inject(TasksService);

  closeWindow() {
    this.close.emit(false);
  }
  onSubmit() {
    // this.onSubmisstion.emit({
    //   title: this.enteredTitle,
    //   summary: this.enteredSummary,
    //   dueDate: this.enteredDueDate,
    // });
    //this.onSubmisstion.emit(NewTaskModel);

    this.tasksService.addTasks(
      {
        title: this.enteredTitle,
        summary: this.enteredSummary,
        dueDate: this.enteredDueDate,
      },
      this.userId,
    );
    this.close.emit(false);
  }
}
