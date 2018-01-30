import { Component, OnInit } from '@angular/core';

import { Author } from '../../models/author.model';
import { SearchModel } from '../../models/search.model';
import { SearchFilter } from '../../models/search.model';
import { SearchResult } from '../../models/search.model';

import { AuthorsService } from '../../services/author.service';

import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { DataTableModule } from 'primeng/datatable';
import { ButtonModule } from 'primeng/button';

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

    private author: Author;
    private displayDialog: boolean;
    private newAuthor: boolean;

    constructor(private authorService: AuthorsService, private toastr: ToastrService) {
        this._toastService = toastr;
        this._totalRecords = 10;
    }

    ngOnInit() {
        this.author = new Author();

        this.authorService.count()
            .then(total => {
                this._totalRecords = total;
            })
            .catch(error => { this._toastService.error(error.message, error.name); });
    }

    loadData(event: LazyLoadEvent) {
        var rows = event.rows != undefined ? (event.rows) : 1;

        var searchModel = new SearchModel();
        searchModel.PageIndex = event.first != undefined ? (event.first / rows + 1) : null;
        searchModel.RecordsPerPage = event.rows != undefined ? (event.rows) : null;

        if (event.filters != undefined) {

            for (let filter in event.filters) {
                searchModel.Filters = [];
                var newFilter = new SearchFilter();
                newFilter.Column = filter;
                newFilter.FilterValue = event.filters[filter].value;
                newFilter.Type = 2;

                searchModel.Filters.push(newFilter);
            }
        }

        searchModel.SortColumn = event.sortField != undefined ? event.sortField : null;
        searchModel.IsAscendingSort = event.sortOrder == 1 ? true : false;

        this.authorService.getAuthorsPage(searchModel)
            .then(searchResult => {
                this._totalRecords = searchResult.TotalRecords;
                this.authors = searchResult.Records;
            })
            .catch(error => { this._toastService.error(error.message, error.name); });
    }

    showDialogToAdd() {
        this.newAuthor = true;
        this.author = new Author();
        this.displayDialog = true;
    }

    cancel() {
        this.author = new Author();
        this.displayDialog = false;
    }
}