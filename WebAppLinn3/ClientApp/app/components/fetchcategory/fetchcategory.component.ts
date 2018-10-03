import { Component, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { CategoryService } from '../../services/categoryservice.service'

@Component({
    templateUrl: './fetchcategory.component.html'
})

export class FetchCategoryComponent {

    public catList: CategoryData[] = [];
    public apiStatus: ApiStatus = { apiToken: "", apiServer: "", statusMsg: "" };

    constructor(public http: Http,
        private _router: Router, private _categoryService: CategoryService) {

        this.getApiStatus(true);
    }

    getApiStatus(thenUpdateCategories: boolean = false) {
        this._categoryService.getApiStatus().subscribe(data => {
            this.apiStatus = data;
            if (this.apiStatus.statusMsg.includes("loading"))
                setTimeout(() => { this.getApiStatus(); }, 500);
            if (thenUpdateCategories)
                this.getCategories();
        }, error => console.error(error))
    }

    setApiToken(apiToken: string = "097b0f85-7a6d-44ef-9aac-abbdd994bcc4") {
        
        if (!apiToken.length) {
            console.warn("apiToken can't be empty!")
            this.getApiStatus();
            return;
        }
        this._categoryService.setApiToken(apiToken).subscribe(data => {
            this.apiStatus = data;
            this.getCategories(true);
        }, error => console.error(error))
    }

    getCategories(thenUpdateStatus: boolean = false) {
        this._categoryService.getCategories().subscribe(data => {
            this.catList = data;
            if (thenUpdateStatus)
                this.getApiStatus();
        }, error => console.error(error))
    }

    delete(categoryID, categoryName) {
        var ans = confirm("Do you want to delete category \"" + categoryName + "\"?");
        if (ans) {
            this._categoryService.deleteCategory(categoryID).subscribe((data) => {
                this.apiStatus = data;
                this.getCategories();
            }, error => console.error(error))
        }
    }
}

interface ApiStatus {
    apiToken: string;
    apiServer: string;
    statusMsg: string;
}

interface CategoryData {
    categoryId: string;
    categoryName: string;
    categoryStock: number;
}
