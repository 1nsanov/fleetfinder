import {
  Component, EventEmitter,
  forwardRef,
  Input,
  OnChanges,
  OnInit, Output,
  SimpleChanges,
} from '@angular/core';

export interface PaginationValue {
  page: number;
  pageSize: number;
  total: number;
}

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent
  implements OnInit, OnChanges
{
  @Input() value: PaginationValue = { page: 1, pageSize: 6, total: 6 };
  @Output() pageChange = new EventEmitter<PaginationValue>();

  totalPages: number;
  visiblePages: number[];
  visibleRangeLength = 5;

  ngOnInit(): void {
    this.updateVisiblePages();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.updateVisiblePages()
  }

  selectPage(page: number): void {
    if (this.value.page === page) return;
    this.value = { ...this.value, page };
    this.updateVisiblePages();
    this.pageChange.emit(this.value)
  }

  updateVisiblePages(): void {
    this.updateTotalPages();
    const length = Math.min(this.totalPages, this.visibleRangeLength);

    const startIndex = Math.max(
      Math.min(
        this.value.page - Math.ceil(length / 2),
        this.totalPages - length
      ),
      0
    );

    this.visiblePages = Array.from(
      new Array(length).keys(),
      (item) => item + startIndex + 1
    );
  }
  updateTotalPages(): void {
    this.totalPages = Math.ceil(this.value.total / this.value.pageSize);
  }
}
