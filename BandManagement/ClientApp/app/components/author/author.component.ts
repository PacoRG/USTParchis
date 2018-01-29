import { Component, OnInit } from '@angular/core';
import { Author } from '../../models/author.model';
import { AuthorsService } from '../../services/author.service';

import {DialogModule} from 'primeng/dialog';
import {InputTextModule} from 'primeng/inputtext';
import {DataTableModule} from 'primeng/datatable';
import {ButtonModule} from 'primeng/button';

import { ToastrService } from 'ngx-toastr';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';

@Component({
    selector: 'author',
    templateUrl: './author.component.html',
    providers: [AuthorsService]
})

export class AuthorComponent implements OnInit {

    private authors: Author[];
    private _totalRecords: number;

 
    private _toastService: ToastrService;

    constructor(private authorService: AuthorsService, private toastr: ToastrService) {
        this._toastService = toastr;
        this._totalRecords = 10;
   }

    ngOnInit() {
    }

    loadData(event: LazyLoadEvent) {
        var pageNumber = event.first != undefined ? event.first + 1 : 1;
        this.authorService.getAuthorsPage(pageNumber, event.rows)
            .then(authors => this.authors = authors)
            .catch(error => { this._toastService.error(error.message, error.name); });
    }

    loadAll() {
        this.authorService.getAuthorsPage()
            .then(authors => this.authors = authors)
            .catch(error => { this._toastService.error(error.message, error.name); });
    }
}