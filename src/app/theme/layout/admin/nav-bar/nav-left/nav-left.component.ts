import { Component, OnInit } from '@angular/core';
import { NextConfig } from 'src/app/app-config';

@Component({
  selector: 'app-nav-left',
  templateUrl: './nav-left.component.html',
  styleUrls: ['./nav-left.component.scss']
})
export class NavLeftComponent implements OnInit {

  public flatConfig: any;

  constructor() {
    this.flatConfig = NextConfig.config;
  }

  ngOnInit(): void {
  }

}
