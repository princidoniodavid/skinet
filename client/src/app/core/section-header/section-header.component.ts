import {Component, OnInit} from '@angular/core';
import {BreadcrumbService} from "xng-breadcrumb";
import {Observable} from "rxjs";

@Component({
  selector: 'app-section-header',
  templateUrl: './section-header.component.html',
  styleUrls: ['./section-header.component.scss']
})
export class SectionHeaderComponent implements OnInit {
  bradCrumb$: Observable<any[]>;

  constructor(private breadcrumbService: BreadcrumbService) {
  }

  ngOnInit(): void {
    this.bradCrumb$ = this.breadcrumbService.breadcrumbs$;
  }

}
