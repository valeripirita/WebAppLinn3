import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class CategoryService {
    myAppUrl: string = "";

    constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.myAppUrl = baseUrl;
    }

    getCategories() {
        return this._http.get(this.myAppUrl + 'api/Category/Index')
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }

    getCategoryById(id: string) {
        return this._http.get(this.myAppUrl + "api/Category/Details/" + id)
            .map((response: Response) => response.json())
            .catch(this.errorHandler)
    }

    saveCategory(category) {
        return this._http.post(this.myAppUrl + 'api/Category/Create', category)
            .map((response: Response) => response.json())
            .catch(this.errorHandler)
    }

    updateCategory(category) {
        return this._http.put(this.myAppUrl + 'api/Category/Edit', category)
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }

    deleteCategory(id) {
        return this._http.delete(this.myAppUrl + "api/Category/Delete/" + id)
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }

    errorHandler(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }
}
