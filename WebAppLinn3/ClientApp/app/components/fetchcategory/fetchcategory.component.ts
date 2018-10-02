import { Component, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { CategoryService } from '../../services/categoryservice.service'

@Component({
    templateUrl: './fetchcategory.component.html'
})

export class FetchCategoryComponent {
    public empList: CategoryData[];

    constructor(public http: Http, private _router: Router, private _categoryService: CategoryService) {
        this.getCategories();
    }

    getCategories() {
        this._categoryService.getCategories().subscribe(
            data => this.empList = data
        )
    }

    delete(categoryID) {
        var ans = confirm("Do you want to delete customer with Id: " + categoryID);
        if (ans) {
            this._categoryService.deleteCategory(categoryID).subscribe((data) => {
                this.getCategories();
            }, error => console.error(error))
        }
    }
}

interface CategoryData {
    categoryId: number;
    name: string;
    gender: string;
    city: string;
    department: string;

}
