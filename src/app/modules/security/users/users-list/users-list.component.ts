import { Component, OnInit, ViewChild } from '@angular/core';
import { ActiveReportsModule, AR_EXPORTS, HtmlExportService, PdfExportService, ViewerComponent, XlsxExportService } from '@grapecity/activereports-angular';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.scss']
})
export class UsersListComponent  implements OnInit {

  constructor(){

  }
  
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

}
