import { Component, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { FetchCategoryComponent } from '../fetchcategory/fetchcategory.component';
import { CategoryService } from '../../services/categoryservice.service';

@Component({
    templateUrl: './AddCategory.component.html'
})

export class createcategory implements OnInit {
    categoryForm: FormGroup;
    title: string = "Create";
    categoryId: string = "";
    errorMessage: any;

    constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute,
        private _router: Router, private _categoryService: CategoryService) {

        if (this._avRoute.snapshot.params["id"]) {
            this.categoryId = this._avRoute.snapshot.params["id"];
        }

        this.categoryForm = this._fb.group({
            categoryId: "",
            categoryName: ['', [Validators.required]],
            categoryStock: 0,
        })
    }

    ngOnInit() {

        if (this.categoryId.length > 0) {
            this.title = "Edit";
            this._categoryService.getCategoryById(this.categoryId)
                .subscribe(resp => this.categoryForm.setValue(resp)
                , error => this.errorMessage = error);
        }
    }

    save() {

        if (!this.categoryForm.valid) {
            return;
        }

        if (this.title == "Create") {
            this._categoryService.saveCategory(this.categoryForm.value)
                .subscribe((data) => {
                    this._router.navigate(['/fetch-category']);
                }, error => this.errorMessage = error)
        }
        else if (this.title == "Edit") {
            this._categoryService.updateCategory(this.categoryForm.value)
                .subscribe((data) => {
                    this._router.navigate(['/fetch-category']);
                }, error => this.errorMessage = error)
        }
    }

    cancel() {
        this._router.navigate(['/fetch-category']);
    }

    get categoryName() { return this.categoryForm.get('categoryName'); }
    //get categoryStock() { return 0; }
}
