import { Component, OnInit } from '@angular/core';
import { Author } from '../../models/author.model';
import { AuthorsService } from '../../services/author.service';

import {DialogModule} from 'primeng/dialog';
import {InputTextModule} from 'primeng/inputtext';
import {DataTableModule} from 'primeng/datatable';
import {ButtonModule} from 'primeng/button';

import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'author',
    templateUrl: './author.component.html',
    providers: [AuthorsService]
})

export class AuthorComponent implements OnInit {

    private authors: Author[];
    private totalRecords: number;

 
    private _toastService: ToastrService;

    constructor(private authorService: AuthorsService, private toastr: ToastrService) {
        this._toastService = toastr;
   }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
        this.authorService.getAuthors()
            .then(authors => this.authors = authors)
            .catch(error => { this._toastService.error(error.message, error.name); });
    }
}