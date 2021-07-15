import { Component, OnInit } from '@angular/core';
import { GetPaginationTextSnippetAction, GetSelectedTextSnippetAction } from 'app/state/app.action';
import { AppSelector } from 'app/state/app.selector';
import { Dispatcher } from 'app/state/dispatcher';

declare const $: any;
declare interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
}

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  menuItems: any[];
  data: any[];
  pageNumber = 0;
  pageSize = 5;
  query = "";
  disableNextButton = false;
  disableBeforeButton = false;

  constructor(private dispatcher: Dispatcher, private appSelector: AppSelector) { 
    this.appSelector.getPaging$.subscribe(x => {
      if (!!x.data.length) {
        this.data = x.data;
        this.pageSize = x.paging.pageSize;
        this.pageNumber = x.paging.pageNumber;
        this.query = x.paging.query;
        this.disableBeforeButton = x.paging.pageNumber == 0;
        this.disableNextButton = x.total - ((this.pageNumber + 1) * this.pageSize) <= 0;
      }
    });
  }

  ngOnInit() {
    var paging = {
      query: "",
      pageSize: this.pageSize,
      pageNumber: this.pageNumber
    }
    this.dispatcher.fire(new GetPaginationTextSnippetAction(paging));
  }

  search() {
    var paging = {
      query: this.query,
      pageSize: this.pageSize,
      pageNumber: 0
    }
    this.dispatcher.fire(new GetPaginationTextSnippetAction(paging));
  }

  getSelected(id: any) {
    var data = this.data.find(x => x.id == id);
    this.dispatcher.fire(new GetSelectedTextSnippetAction(data));
  }

  moveNext() {
    this.pageNumber++;
    var paging = {
      query: this.query,
      pageSize: this.pageSize,
      pageNumber: this.pageNumber
    }
    this.dispatcher.fire(new GetPaginationTextSnippetAction(paging));
  }

  moveBack() {
    this.pageNumber--;
    var paging = {
      query: this.query,
      pageSize: this.pageSize,
      pageNumber: this.pageNumber
    }
    this.dispatcher.fire(new GetPaginationTextSnippetAction(paging));
  }
}
