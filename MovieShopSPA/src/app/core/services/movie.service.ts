import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { MovieCard } from 'src/app/shared/models/moviecard';
@Injectable({
  providedIn: 'root'
})
export class MovieService {
  // inject httpclient

  constructor(private http : HttpClient) { }
  // home component call this function
  //https://localhost:44360/api/Movies/toprevenue
  getTopRevenueMovies():Observable<MovieCard[]> {
    // call api using httpClient (XMLHttpRequest) to make a Get
    // HttpClient is from HttpClientModule
    // import HttpClientModule inside app module
    return this.http.get<MovieCard[]>("https://localhost:44360/api/Movies/toprevenue");

  }
}
