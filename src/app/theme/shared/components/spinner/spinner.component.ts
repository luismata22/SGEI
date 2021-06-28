import { DOCUMENT } from '@angular/common';
import { Spinkit } from './spinkits';
import { Component, Inject, Input, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { NavigationCancel, NavigationEnd, NavigationError, NavigationStart, Router } from '@angular/router';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: [
    './spinner.component.scss',
    './spinkit-css/sk-line-material.scss'
  ],
  encapsulation: ViewEncapsulation.None
})
export class SpinnerComponent implements OnDestroy {
  public isSpinnerVisible = true;
  public Spinkit = Spinkit;
  @Input() public backgroundColor = '#1abc9c';
  @Input() public spinner = Spinkit.skLine;
  constructor(private router: Router, @Inject(DOCUMENT) private document: Document) {
      this.router.events.subscribe(event => {
          if (event instanceof NavigationStart) {
              this.isSpinnerVisible = true;
          } else if ( event instanceof NavigationEnd || event instanceof NavigationCancel || event instanceof NavigationError) {
              this.isSpinnerVisible = false;
          }
      }, () => {
          this.isSpinnerVisible = false;
      });
  }

  ngOnDestroy(): void {
      this.isSpinnerVisible = false;
  }
}

