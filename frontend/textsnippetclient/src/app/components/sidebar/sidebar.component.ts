import { Component, OnInit } from '@angular/core';

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

  constructor() { 
  }

  ngOnInit() {
  }

  search() {
  }

  getSelected(id: any) {
  }

  moveNext() {
  }

  moveBack() {
  }
}
