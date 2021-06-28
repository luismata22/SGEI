import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NextConfig } from 'src/app/app-config';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {

  public windowWidth: number;
  public flatConfig: any;
  @Output() onNavMobCollapse = new EventEmitter();
  
  constructor() { 
    this.flatConfig = NextConfig.config;
    this.windowWidth = window.innerWidth;
  }

  ngOnInit(): void {
  }

  navMobCollapse() {
    if (this.windowWidth < 992) {
      this.onNavMobCollapse.emit();
    }
  }

}
