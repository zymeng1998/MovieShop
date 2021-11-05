import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Genre } from "src/app/shared/models/Genre";
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class GenreService {
  constructor(private http : HttpClient) { }
  getAllGenres() : Observable<Genre[]> {
    return this.http.get<Genre[]>(`${environment.apiBaseUrl}Genres`);
  }
}
