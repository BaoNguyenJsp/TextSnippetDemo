import { Component, OnInit } from '@angular/core';
import { CreateTextSnippetAction, DeleteTextSnippetAction, UpdateTextSnippetAction } from 'app/state/app.action';
import { AppSelector } from 'app/state/app.selector';
import { Dispatcher } from 'app/state/dispatcher';

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

  constructor(private dispatcher: Dispatcher, private appSelector: AppSelector) { 
    this.appSelector.getRoles$.subscribe(x => {
      this.editMode = x.some(y => y == "admin");
    });
    this.appSelector.getSelected$.subscribe(x => {
      if (x.id != 0) {
        this.id = x.id;
        this.title = x.title;
        this.content = x.content;
      }
    });
  }

  ngOnInit() {

  }

  edit() {
    if (this.id == 0) return;
    var payload = {
      id: this.id,
      data: {
        title: this.title,
        content: this.content
      }
    };
    this.dispatcher.fire(new UpdateTextSnippetAction(payload));
  }

  create() {
    var data = {
      title: this.title,
      content: this.content
    };
    this.dispatcher.fire(new CreateTextSnippetAction(data));
  }

  delete() {
    if (this.id == 0) return;
    this.dispatcher.fire(new DeleteTextSnippetAction(this.id));
    this.id = 0;
    this.title = "";
    this.content = "";
  }
}
