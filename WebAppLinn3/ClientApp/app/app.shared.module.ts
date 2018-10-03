import { NgModule } from '@angular/core';
import { CategoryService } from './services/categoryservice.service'
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchCategoryComponent } from './components/fetchcategory/fetchcategory.component'
import { createcategory } from './components/addcategory/AddCategory.component'

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        FetchCategoryComponent,
        createcategory,
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'fetch-category', component: FetchCategoryComponent },
            { path: 'register-category', component: createcategory },
            { path: 'category/edit/:id', component: createcategory },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [CategoryService]
})
export class AppModuleShared {
}
