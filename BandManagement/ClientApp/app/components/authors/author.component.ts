import { Component, OnInit } from '@angular/core';
import { Author } from '../../models/author.model';
import { AuthorsService } from '../../services/author.service';
//import { ToastsManager } from 'ng2-toastr/ng2-toastr';
import {DialogModule} from 'primeng/dialog';
import {InputTextModule} from 'primeng/inputtext';
import {DataTableModule} from 'primeng/datatable';
import {ButtonModule} from 'primeng/button';

@Component({
    selector: 'author',
    templateUrl: './author.component.html',
    providers: [AuthorsService]
})

export class AuthorComponent implements OnInit {

    private rowData: any[];
    displayDialog: boolean;
    displayDeleteDialog: boolean;
    newAuthor: boolean;
    author: Author = new Author();
    authors: Author[];
    public editAuthorId: any;
    public fullname: string;

   constructor(private authorService: AuthorsService) {
    }

    ngOnInit() {
        this.editAuthorId = 0;
        //this.loadData();
    }

    loadData() {
        this.authorService.getAuthors()
            .subscribe(res => {
                this.rowData = res.result;
            });
    }

    showDialogToAdd() {
        this.newAuthor = true;
        this.editAuthorId = 0;
        this.author = new Author();
        this.displayDialog = true;
    }

    showDialogToEdit(author: Author) {
        this.newAuthor = false;
        this.author = new Author();
        this.author.id = author.id;
        this.author.name = author.name;
        this.displayDialog = true;
    }

    save() {
        this.authorService.saveAuthor(this.author)
            .subscribe(response => {
                //this.author.id > 0 ? this.toastrService.success('Data updated Successfully') :
                //    this.toastrService.success('Data inserted Successfully');
                this.loadData();
            });
        this.displayDialog = false;
    }

    cancel() {
        this.author = new Author();
        this.displayDialog = false;
    }


    showDialogToDelete(author: Author) {
        this.fullname = author.name;
        this.editAuthorId = author.id;
        this.displayDeleteDialog = true;
    }

    okDelete(isDeleteConfirm: boolean) {
        if (isDeleteConfirm) {
            this.authorService.deleteAuthor(this.editAuthorId)
                .subscribe(response => {
                    this.editAuthorId = 0;
                    this.loadData();
                });
            //this.toastrService.error('Data Deleted Successfully');
        }
        this.displayDeleteDialog = false;
    }
}