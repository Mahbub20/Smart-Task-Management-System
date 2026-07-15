import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {

  projectId!: number;


  tasks: any[] = [];



  title = '';

  description = '';

  priority = 2;
  status = 1;

  dueDate = '';

  editMode = false;

  selectedTaskId!: number;

  constructor(private route: ActivatedRoute, private service: TaskService, private toastr: ToastrService) { }

  ngOnInit(): void {

    this.projectId =
      Number(
        this.route.snapshot.paramMap.get('id')
      );

    this.loadTasks();
  }

  loadTasks() {

    this.service.getTasks(
      this.projectId
    )
      .subscribe(res => {

        this.tasks = res;

      });

  }



  // addTask() {


  //   const data = {

  //     title: this.title,

  //     description: this.description,

  //     priority: this.priority,
  //     status: this.status,

  //     dueDate: this.dueDate

  //   };



  //   this.service.createTask(
  //     this.projectId,
  //     data
  //   )
  //     .subscribe(() => {


  //       this.title = '';

  //       this.description = '';


  //       this.loadTasks();


  //     });


  // }


  // saveTask() {


  //   const data = {

  //     title: this.title,

  //     description: this.description,

  //     priority: this.priority,

  //     status: this.status,

  //     dueDate: this.dueDate

  //   };


  //   if (this.editMode) {

  //     this.service.updateTask(
  //       this.selectedTaskId,
  //       data
  //     )
  //       .subscribe(() => {

  //         this.resetForm();

  //         this.loadTasks();

  //       });


  //   }
  //   else {

  //     this.service.createTask(
  //       this.projectId,
  //       data
  //     )
  //       .subscribe(() => {

  //         this.resetForm();

  //         this.loadTasks();

  //       });

  //   }

  // }

  saveTask() {

    const data = {

      title: this.title,

      description: this.description,

      priority: this.priority,

      status: this.status,

      dueDate: this.dueDate

    };

    if (this.editMode) {

      this.service.updateTask(this.selectedTaskId, data).subscribe({

        next: () => {

          this.toastr.success(
            'Task updated successfully.',
            'Success'
          );

          this.resetForm();
          this.loadTasks();

        },

        error: () => {

          this.toastr.error(
            'Update failed.',
            'Error'
          );

        }

      });

    }
    else {

      this.service.createTask(
        this.projectId,
        data
      ).subscribe({

        next: () => {

          this.toastr.success(
            'Task created successfully.',
            'Success'
          );

          this.resetForm();
          this.loadTasks();

        },

        error: () => {

          this.toastr.error(
            'Creation failed.',
            'Error'
          );

        }

      });

    }

  }


  editTask(task: any) {

    this.editMode = true;


    this.selectedTaskId = task.id;


    this.title = task.title;

    this.description = task.description;

    this.priority = task.priority;

    this.status = task.status;

    this.dueDate = task.dueDate.substring(0, 10);


  }

  resetForm() {

    this.title = '';

    this.description = '';

    this.priority = 2;

    this.status = 1;

    this.dueDate = '';

    this.editMode = false;

    this.selectedTaskId = 0;

  }

  cancelEdit() {

    this.resetForm();

  }

  updateStatus(
    task: any,
    event: any
  ) {


    task.status =
      Number(event.target.value);



    this.service.updateTask(
      task.id,
      task
    )
      .subscribe();


  }



  delete(id: number) {


    this.service.deleteTask(id)
      .subscribe(() => {

        this.loadTasks();

      });


  }

  confirmDelete() {

    this.service.deleteTask(this.selectedTaskId)
      .subscribe({

        next: () => {

          this.toastr.success(
            'Task deleted successfully.',
            'Success'
          );

          this.loadTasks();

        },

        error: () => {

          this.toastr.error(
            'Failed to delete task.',
            'Error'
          );

        }

      });

  }

}
