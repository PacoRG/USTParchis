import { Component, OnInit } from '@angular/core';

import { Author } from '../../models/author.model';
import { SearchModel } from '../../models/search.model';
import { SearchFilter } from '../../models/search.model';
import { SearchResult } from '../../models/search.model';
import { ValidationResult } from '../../models/validationResult.model';
import { ValidationField } from '../../models/validationField.model';
import { Dictionary } from '../../models/dictionary.model';

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
    private _totalRecords: number;
    private _toastService: ToastrService;
    private _lastLoadEvent: LazyLoadEvent;

    private authorIdentifier: string = "Domain.Model.Author.Name";

    private validationFields: Dictionary<ValidationField>;
    private authors: Author[];
    private author: Author;
    private displayDialog: boolean;
    private newAuthor: boolean;
    private nameCorrect: boolean;

    constructor(private authorService: AuthorsService, private toastr: ToastrService) {
        this._toastService = toastr;
        this._totalRecords = 10;
        this.nameCorrect = true;
    }

    ngOnInit() {
        this.validationFields = new Dictionary<ValidationField>();
        this.validationFields.Add(this.authorIdentifier,new ValidationField());

        this.author = new Author();

        this.authorService.count()
            .then(total => {
                this._totalRecords = total;
            })
            .catch(error => { this._toastService.error(error.message, error.name); });
    }

    loadData(event: LazyLoadEvent) {
        var rows = event.rows != undefined ? (event.rows) : 1;
        this._lastLoadEvent = event;

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
        this.clearValidations();
    }

    save() {
        this.authorService.save(this.author)
            .then(results => {
                if (results.length > 0) {
                    this.displayValidation(results);
                }
                else {
                    this._toastService.success("El autor se ha guardado correctamente");
                    this.loadData(this._lastLoadEvent);
                    this.displayDialog = false;
                }

            })
            .catch(error => { this._toastService.error(error.message, error.name); })
    }

    clearValidations() {
        for (let field of this.validationFields.Values()) {
            field.Visible = false;
            field.Message = "";
        }
    }

    displayValidation(validationResults : ValidationResult[]) {
        for (let validationResult of validationResults) {
            var fieldName = validationResult.Namespace + "." + validationResult.Class + "." + validationResult.Identifier;

            var field = this.validationFields.Item(fieldName);
            if (field != undefined) {
                field.Visible = true;
                field.Message = validationResult.Message;
            }

        }
    }

    cancel() {
        this.author = new Author();
        this.displayDialog = false;
    }
}