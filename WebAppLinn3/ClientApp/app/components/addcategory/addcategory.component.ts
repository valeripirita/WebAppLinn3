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
    categoryId: number;
    errorMessage: any;
    cityList: Array<any> = [];

    constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute,
        private _categoryService: CategoryService, private _router: Router) {
        if (this._avRoute.snapshot.params["id"]) {
            this.categoryId = this._avRoute.snapshot.params["id"];
        }

        this.categoryForm = this._fb.group({
            categoryId: 0,
            name: ['', [Validators.required]],
            gender: ['', [Validators.required]],
            department: ['', [Validators.required]],
            city: ['', [Validators.required]]
        })
    }

    ngOnInit() {

        this._categoryService.getCityList().subscribe(
            data => this.cityList = data
        )

        if (this.categoryId > 0) {
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

    get name() { return this.categoryForm.get('name'); }
    get gender() { return this.categoryForm.get('gender'); }
    get department() { return this.categoryForm.get('department'); }
    get city() { return this.categoryForm.get('city'); }
}
