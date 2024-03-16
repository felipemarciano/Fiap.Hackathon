import { Routes } from '@angular/router';
import { InitialComponent } from './Views/initial/initial.component';

export const routes: Routes = [
    {path: '**', component: InitialComponent}
];
