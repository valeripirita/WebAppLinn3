import { Component, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { CategoryService } from '../../services/categoryservice.service'

@Component({
    templateUrl: './fetchcategory.component.html'
})

export class FetchCategoryComponent {

    public apiToken: string = "097b0f85-7a6d-44ef-9aac-abbdd994bcc4";
    public catList: CategoryData[] = [];

    constructor(public http: Http, private _router: Router, private _categoryService: CategoryService) {
        this.getCategories();
    }

    setApiToken(apiToken: string) {
        if (this.apiToken === apiToken)
            return;
        this.apiToken = apiToken;
        this.getCategories();
    }

    getCategories() {
        this._categoryService.getCategories().subscribe(
            data => this.catList = data
        )
    }

    delete(categoryID, categoryName) {
        var ans = confirm("Do you want to delete category \"" + categoryName + "\"?");
        if (ans) {
            this._categoryService.deleteCategory(categoryID).subscribe((data) => {
                this.getCategories();
            }, error => console.error(error))
        }
    }
}

interface CategoryData {
    categoryId: string;
    categoryName: string;
    categoryStock: number;
}
