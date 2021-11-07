import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MoviesRoutingModule } from './movies-routing.module';
import { MovieDetailsComponent } from './movie-details/movie-details.component';
import { CastDetailsComponent } from './cast-details/cast-details.component';
import { TopratedComponent } from './toprated/toprated.component';
import { MoviesComponent } from './movies.component';
import { MovieCardComponent } from '../shared/components/movie-card/movie-card.component';


@NgModule({
  declarations: [
    MovieDetailsComponent,
    CastDetailsComponent,
    TopratedComponent,
    MoviesComponent,
  ],
  imports: [
    CommonModule,
    MoviesRoutingModule,
    // MovieCardComponent
  ]
})
export class MoviesModule { }
