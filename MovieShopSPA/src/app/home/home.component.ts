import { Component, OnInit } from '@angular/core';
import { GenreService } from '../core/services/genre.service';
import { MovieService } from '../core/services/movie.service';
import { Movie } from '../shared/models/movie';
import { MovieCard } from '../shared/models/moviecard';
import { Genre } from '../shared/models/movie';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  myPageTitle  = "Movie Shop SPA";
  movieCardsTopRevenue !: MovieCard[];
  movieCardsTopRated !: MovieCard[];
  movieTest !: Movie;
  genreLst !: Genre[];
  constructor(private movieService: MovieService, private genreService: GenreService) { }

  ngOnInit(): void {
    // it is one of the most important life cycle hooks method in angular
    // it is recommanded to use this method to call the api and initialize any data properties
    // will be called automatically by your anuglar componet after call constructor

    // only when you subscribe to the observable you get the data
    this.movieService.getTopRevenueMovies().subscribe(
      m => {
        this.movieCardsTopRevenue = m;
        console.log("top revenue")
        console.table(this.movieCardsTopRevenue);
      }
    );
    this.movieService.getTopRatedMovies().subscribe(
      m => {
        this.movieCardsTopRated = m;
        console.log("top rated")
        console.table(this.movieCardsTopRated);
      }
    );
    this.movieService.getMovieDetails(2).subscribe(
      m => {
        this.movieTest = m;
        console.table(this.movieTest);
      }
    );
    this.genreService.getAllGenres().subscribe(
      g => {
        this.genreLst = g;
        console.table(this.genreLst);
      }
    );
  }

}
