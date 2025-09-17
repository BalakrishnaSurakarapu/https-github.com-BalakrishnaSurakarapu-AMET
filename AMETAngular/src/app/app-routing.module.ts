import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewPopupComponent } from './Components/new-popup/new-popup.component';
import { NewDataComponent } from './Components/new-data/new-data.component';


const routes: Routes = [
  {path:'newpopup',component:NewPopupComponent},
  {path:'data',component:NewDataComponent},
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
