import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { error } from 'console';
import { ToastrService } from 'ngx-toastr';
import { ProjectService } from 'src/app/services/project.service';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {

  deleteProjectId!: number;

  projects: any[] = [];


  name = '';

  description = '';

  editMode = false;

  selectedProjectId!: number;

  constructor(private service: ProjectService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadProjects();
  }

  loadProjects() {

    this.service.getProjects()
      .subscribe(res => {

        this.projects = res;

      });


  }

  openTasks(id: number) {

    this.router.navigate([
      '/projects',
      id,
      'tasks'
    ]);

  }

  // save() {

  //   const data = {

  //     name: this.name,

  //     description: this.description

  //   };


  //   if (this.editMode) {

  //     this.service.updateProject(
  //       this.selectedProjectId,
  //       data
  //     )
  //       .subscribe(() => {

  //         next: () => {
  //           this.toastr.success(
  //             'Project updated successfully.',
  //             'Success'
  //           );

  //           this.resetForm();
  //           this.loadProjects();
  //         },

  //           error: () => {
  //             this.toastr.error(
  //               'Update failed.',
  //               'Error'
  //             );
  //           }
  //       });


  //   }
  //   else {

  //     this.service.createProject(data)
  //       .subscribe(() => {

  //         this.resetForm();

  //         this.loadProjects();

  //       });

  //   }

  // }

  save() {

    if (!this.name.trim()) {

      this.toastr.warning(
        'Project name is required.',
        'Validation'
      );

      return;

    }

    if (!this.description.trim()) {

      this.toastr.warning(
        'Project description is required.',
        'Validation'
      );

      return;

    }

    if (this.editMode) {

      this.service.updateProject(this.selectedProjectId, {
        name: this.name,
        description: this.description
      }).subscribe({

        next: () => {

          this.toastr.success(
            'Project updated successfully.',
            'Success'
          );

          this.loadProjects();

          this.cancelEdit();

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

      this.service.createProject({
        name: this.name,
        description: this.description
      }).subscribe({

        next: () => {

          this.toastr.success(
            'Project created successfully.',
            'Success'
          );

          this.loadProjects();

          this.name = '';
          this.description = '';

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

  editProject(project: any) {

    this.editMode = true;


    this.selectedProjectId = project.id;


    this.name = project.name;

    this.description = project.description;


  }

  resetForm() {

    this.name = '';

    this.description = '';

    this.editMode = false;

    this.selectedProjectId = 0;

  }

  cancelEdit() {

    this.resetForm();

  }

  delete(id: number) {
    this.service.deleteProject(id)
      .subscribe(() => {

        this.loadProjects();

      });
  }

  confirmDelete() {

    this.service.deleteProject(this.selectedProjectId)
      .subscribe({

        next: () => {

          this.toastr.success(
            'Project deleted successfully.',
            'Success'
          );

          this.loadProjects();

        },

        error: () => {

          this.toastr.error(
            'Failed to delete project.',
            'Error'
          );

        }

      });

  }

}
