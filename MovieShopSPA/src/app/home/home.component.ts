import { Component, OnInit } from '@angular/core';
import { MovieService } from '../core/services/movie.service';
import { MovieCard } from '../shared/models/moviecard';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  myPageTitle  = "Movie Shop SPA";
  movieCards !: MovieCard[];

  constructor(private movieService: MovieService) { }

  ngOnInit(): void {
    // it is one of the most important life cycle hooks method in angular
    // it is recommanded to use this method to call the api and initialize any data properties
    // will be called automatically by your anuglar componet after call constructor

    // only when you subscribe to the observable you get the data
    this.movieService.getTopRevenueMovies().subscribe(
      m => {
        this.movieCards = m;
        console.log("inside the ngOnInit")
        console.table(this.movieCards);
      }
    );
  }

}
