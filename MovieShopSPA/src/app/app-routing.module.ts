import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';

// specifiy all the routes required by the angular app
const routes: Routes = [
  {path: "", component: HomeComponent},
  //{path: "movie/:id", component: MovieDetailsComponent},
  // {path: "admin/createmovie", component, CreateMovieComponent},
  

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
