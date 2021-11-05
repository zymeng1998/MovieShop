import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Genre } from 'src/app/shared/models/movie';
@Injectable({
  providedIn: 'root'
})
export class GenreService {
  constructor(private http : HttpClient) { }
  getAllGenres() : Observable<Genre[]> {
    return this.http.get<Genre[]>("https://localhost:44360/api/Genres");
  }
}
