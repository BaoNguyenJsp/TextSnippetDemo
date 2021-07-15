import { Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

@Pipe({name: 'customhighlight'})
export class HightlightTextPipe implements PipeTransform {
    constructor(private sanitizer: DomSanitizer) {}

    transform(value: string, query: string) {
    if (!query) {
        return value;
    }
    // Escape special character first
    query =  query.replace(/[.*+?^${}()|[\]\\]/g, '\\$&'); // $& means the whole matched string
    // Match global insensitve-case and replace all but keep the original case
    var re = new RegExp(query, "ig");
    var result = value.replace(re, '<span style="font-weight:bold; color: red;">$&</span>')
    return this.sanitizer.bypassSecurityTrustHtml(result);
  }
}