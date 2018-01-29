import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders, HttpRequest } from '@angular/common/http';

import { Author } from '../models/author.model';


@Injectable()
export class AuthorsService {
    private _getPage = "/Author/GetPage";
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

    getAuthorsPage(pageIndex?:number, recordsPerPage?:number) {
        var getAuthorsUrl = this._apiUrl + this._getPage + "?pageNumber=" + pageIndex + "&recordsPerPage=" + recordsPerPage;

        return this.http.get(getAuthorsUrl)
            .toPromise()
            .then(res => <Author[]>res)
    }

}