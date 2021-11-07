import { Component, OnInit } from '@angular/core';
import { MovieService } from 'src/app/core/services/movie.service';
import { MovieCard } from 'src/app/shared/models/moviecard';

@Component({
  selector: 'app-toprated',
  templateUrl: './toprated.component.html',
  styleUrls: ['./toprated.component.css']
})
export class TopratedComponent implements OnInit {
  movieCardsTopRated !: MovieCard[];
  constructor(private movieService: MovieService) { }

  ngOnInit(): void {
    this.movieService.getTopRatedMovies().subscribe(
      m => {
        this.movieCardsTopRated = m;
        console.log("top rated")
        console.table(this.movieCardsTopRated);
      }
    );
  }

}
