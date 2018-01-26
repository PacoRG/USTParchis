import { Injectable, Inject } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Author } from '../models/author.model';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";

@Injectable()
export class AuthorsService {

    private _getAllUrl = "/Author/GetAll";
    public _saveUrl: string = '/Author/Save/';
    public _deleteByIdUrl: string = '/Author/Delete/';

    private _apiUrl: string;

    constructor(private http: Http, @Inject('API_URL') apiUrl: string) {
        this._apiUrl = apiUrl;
    }

    getAuthors() {
        var getAuthorsUrl = this._apiUrl + this._getAllUrl;
        return this.http.get(getAuthorsUrl)
            .map(response => <any>(<Response>response).json());
    }

    saveAuthor(author: Author): Observable<string> {
        let body = JSON.stringify(author);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        var saveUrl = this._apiUrl + this._saveUrl;
        return this.http.post(saveUrl, body, options)
            .map(res => res.json().message)
            .catch(this.handleError);
    }

    deleteAuthor(id: number): Observable<string> {
        var deleteByIdUrl = this._apiUrl + this._deleteByIdUrl + '/' + id

        return this.http.delete(deleteByIdUrl)
            .map(response => response.json().message)
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        return Observable.throw(error.json().error || 'Opps!! Server error');
    }

}