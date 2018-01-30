export class SearchModel {
    PageIndex: number | null;
    RecordsPerPage: number | null;
    SortColumn: string | null;
    IsAscendingSort: boolean | null;
    Filters: SearchFilter[]
}

export class SearchFilter {
    Column: string | null;
    FilterValue: string | null;
    Type: number | null;
}

export class SearchResult<T> {
    Records: T[];
    TotalRecords: number;
}