import { Component, ViewEncapsulation, HostListener, EventEmitter, Input, Output, AfterViewInit, ChangeDetectionStrategy } from '@angular/core';
import { NgClass } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GridSetings } from '../GridSetings';

function coerceToBoolean(input: string | boolean): boolean {
    return !!input && input !== 'false';
}

export interface Page {
    label: string;
    value: any;
}

@Component({
    selector: 'GridPagination',
    templateUrl: './gridpagination.html',
    styleUrls: ['./gridpagination.css'],
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.Default
})
export class GridPaginationComponent {
    private _directionLinks: boolean = true;
    private _autoHide: boolean = false;
    private _responsive: boolean = false;
     pages: Page[] = new Array<Page>();

    @Input() previousLabel: string = 'Previous';
    @Input() nextLabel: string = 'Next';
    @Input() screenReaderPaginationLabel: string = 'Pagination';
    @Input() screenReaderPageLabel: string = 'page';
    @Input() screenReaderCurrentLabel: string = `You're on page`;
    @Input() public setings: GridSetings = new GridSetings();
    @Output() pageChange: EventEmitter<number> = new EventEmitter<number>();

    @Input() id: string;
    @Input() maxSize: number = 7;
    @Input()
    get directionLinks(): boolean {
        return this._directionLinks;
    }
    set directionLinks(value: boolean) {
        this._directionLinks = coerceToBoolean(value);
    }
    @Input()
    get autoHide(): boolean {
        return this._autoHide;
    }
    set autoHide(value: boolean) {
        this._autoHide = coerceToBoolean(value);
    }
    @Input()
    get responsive(): boolean {
        return this._responsive;
    }
    set responsive(value: boolean) {
        this._responsive = coerceToBoolean(value);
    }
    get getCurrent() {
        return this.setings.RequestSettings.CurrentPage;
    }
    get getLastPage(): number {
        if (this.setings.TotalItems < 1) {
            return 1;
        }
        return Math.ceil(this.setings.TotalItems / this.setings.RequestSettings.ItemsPerPage);
    }
    get isFirstPage() {
        return this.getCurrent == 1;
    }
    get isLastPage() {
        return this.getCurrent == this.getLastPage;
    }

    public OnLoad() {
        this.pages = this.createPageArray(this.getCurrent, this.setings.RequestSettings.ItemsPerPage, this.setings.TotalItems, this.maxSize);;
    }

    private onPageChange(page) {
        this.pageChange.emit(page.value);
    }
    private next() {
        if (this.getCurrent < this.getLastPage) {
            this.pageChange.emit(this.getCurrent + 1);
        }
    }
    private previous() {
        if (this.getCurrent > 1) {
            this.pageChange.emit(this.getCurrent - 1);
        }
    }
    private createPageArray(currentPage: number, itemsPerPage: number, totalItems: number, paginationRange: number): Page[] {
        // paginationRange could be a string if passed from attribute, so cast to number.
        paginationRange = +paginationRange;
        let pages = [];
        const totalPages = Math.ceil(totalItems / itemsPerPage);
        const halfWay = Math.ceil(paginationRange / 2);

        const isStart = currentPage <= halfWay;
        const isEnd = totalPages - halfWay < currentPage;
        const isMiddle = !isStart && !isEnd;

        let ellipsesNeeded = paginationRange < totalPages;
        let i = 1;

        while (i <= totalPages && i <= paginationRange) {
            let label;
            let pageNumber = this.calculatePageNumber(i, currentPage, paginationRange, totalPages);
            let openingEllipsesNeeded = (i === 2 && (isMiddle || isEnd));
            let closingEllipsesNeeded = (i === paginationRange - 1 && (isMiddle || isStart));
            if (ellipsesNeeded && (openingEllipsesNeeded || closingEllipsesNeeded)) {
                label = '...';
            } else {
                label = pageNumber;
            }
            pages.push({
                label: label,
                value: pageNumber
            });
            i++;
        }
        return pages;
    }
    private calculatePageNumber(i: number, currentPage: number, paginationRange: number, totalPages: number) {
        let halfWay = Math.ceil(paginationRange / 2);
        if (i === paginationRange) {
            return totalPages;
        } else if (i === 1) {
            return i;
        } else if (paginationRange < totalPages) {
            if (totalPages - halfWay < currentPage) {
                return totalPages - paginationRange + i;
            } else if (halfWay < currentPage) {
                return currentPage - halfWay + i;
            } else {
                return i;
            }
        } else {
            return i;
        }
    }
}