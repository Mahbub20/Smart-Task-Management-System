import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  private url = environment.apiUrl + '/tasks';

  constructor(private http: HttpClient) { }

  getTasks(projectId: number) {

    return this.http.get<any[]>(
      `${this.url}/projects/${projectId}/tasks`
    );

  }

  createTask(projectId: number, data: any) {

    return this.http.post(
      `${this.url}/projects/${projectId}/tasks`,
      data
    );

  }

  updateTask(id: number, data: any) {

    return this.http.put(
      `${this.url}/tasks/${id}`,
      data
    );

  }

  deleteTask(id: number) {

    return this.http.delete(
      `${this.url}/tasks/${id}`
    );

  }
}
