import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProjectService } from 'src/app/services/project.service';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {

  projects: any[] = [];


  name = '';

  description = '';

  editMode = false;

  selectedProjectId!: number;

  constructor(private service: ProjectService, private router: Router) { }

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
  //   this.service.createProject({

  //     name: this.name,

  //     description: this.description

  //   })
  //     .subscribe(() => {


  //       this.name = '';

  //       this.description = '';

  //       this.loadProjects();
  //     });


  // }

  save() {

    const data = {

      name: this.name,

      description: this.description

    };


    if (this.editMode) {

      this.service.updateProject(
        this.selectedProjectId,
        data
      )
        .subscribe(() => {

          this.resetForm();

          this.loadProjects();

        });


    }
    else {

      this.service.createProject(data)
        .subscribe(() => {

          this.resetForm();

          this.loadProjects();

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

}
