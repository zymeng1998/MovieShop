import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Action } from 'rxjs/internal/scheduler/Action';
import { MovieService } from 'src/app/core/services/movie.service';
import { Movie } from 'src/app/shared/models/movie';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {
  movie!: Movie;
  id: number = 0;
  priceDisplay: string = "wrong value";
  ratingDisplay: string = "wrong value";
  releaseYear: string = "wrong val";
  constructor(private activatedRoute: ActivatedRoute, private movieService: MovieService) {

  }

  ngOnInit(): void {
    // ActivatedRoute 
    // get id from url
    // call api
    this.activatedRoute.paramMap.subscribe(
      p => {
        this.id = Number(p.get('id'));
        console.log('movieId = '+ this.id);
        this.movieService.getMovieDetails(this.id).subscribe(
          m => {
            this.movie = m;
            console.log(this.movie);
            this.priceDisplay = this.movie.price.toFixed(2).toString();
            this.ratingDisplay = this.movie.rating.toFixed(2).toString();
            this.releaseYear = new Date(this.movie.releaseDate).getFullYear.toString();
          }
        );
      }
    );

    console.log("inside movie details");
  }

}
