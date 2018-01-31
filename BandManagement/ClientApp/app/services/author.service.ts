import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders, HttpRequest } from '@angular/common/http';

import { Author } from '../models/author.model';
import { SearchModel } from '../models/search.model';
import { SearchResult } from '../models/search.model';
import { ValidationResult } from '../models/validationResult.model';

@Injectable()
export class AuthorsService {
    private _getPage = "/Author/GetPage";
    private _countUrl = "/Author/Count";
    private _getAllUrl = "/Author/GetAll";
    public _saveUrl: string = '/Author/Save/';
    public _deleteByIdUrl: string = '/Author/Delete/';

    private _apiUrl: string;


    constructor(private http: HttpClient, @Inject('API_URL') apiUrl: string) {
        this._apiUrl = apiUrl;
    }

    getAuthors() {
        var getAuthorsUrl = this._apiUrl + this._getAllUrl;

        return this.http.get(getAuthorsUrl)
            .toPromise()
            .then(res => <Author[]>res)
    }

    getAuthorsPage(searchModel: SearchModel) {

        var getAuthorsUrl = this._apiUrl + this._getPage;

        return this.http.post(getAuthorsUrl, searchModel)
            .toPromise()
            .then(res => <SearchResult<Author>>res)
    }

    count() {
        return this.http.get(this._apiUrl + this._countUrl)
            .toPromise()
            .then(
            res => <number>res)
    }

    save(author: Author) {
        var saveUrl = this._apiUrl + this._saveUrl;

        return this.http.post(saveUrl, author)
            .toPromise()
            .then(res => <ValidationResult[]>res);
    }

    delete(author: Author) {
        var deleteURL = this._apiUrl + this._deleteByIdUrl;

        return this.http.post(deleteURL, author)
            .toPromise();
    }

}