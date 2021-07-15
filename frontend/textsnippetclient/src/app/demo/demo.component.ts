import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-demo',
  templateUrl: './demo.component.html',
  styleUrls: ['./demo.component.css']
})
export class DemoComponent implements OnInit {
  id = 0;
  title = "";
  content = "";
  editMode = false;
  query = null;

  constructor() { 
  }

  ngOnInit() {

  }

  edit() {
  }

  create() {
  }

  delete() {
  }
}
