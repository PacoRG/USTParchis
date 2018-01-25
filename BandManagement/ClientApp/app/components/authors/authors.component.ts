import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { AuthorModel } from '../../models/author.model';

@Component({
    selector: 'authors',
    templateUrl: './authors.component.html'
})
export class AuthorsComponent {
    public authors: AuthorModel[];

    constructor(http: Http, @Inject('API_URL') baseUrl: string) {
        http.get(baseUrl + 'api/Authors').subscribe(result => {
            this.authors = result.json() as AuthorModel[];
        }, error => console.error(error));
    }
}
