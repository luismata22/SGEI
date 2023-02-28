import { AfterViewInit, Component, ElementRef, EventEmitter, NgZone, OnInit, Output, ViewChild } from '@angular/core';
import { NextConfig } from 'src/app/app-config';
import { NavigationItem } from '../navigation';
import { Location } from '@angular/common';
import { AuthService } from 'src/app/modules/authentication/shared/auth.service';
import { NotificationService } from 'src/app/modules/utils/notification.service';
import * as Permissions from 'src/app/modules/utils/permissions';
import { UserPermissionsService } from 'src/app/modules/authentication/shared/user-permissions.service';

@Component({
  selector: 'app-nav-content',
  templateUrl: './nav-content.component.html',
  styleUrls: ['./nav-content.component.scss']
})
export class NavContentComponent implements OnInit, AfterViewInit {

  public flatConfig: any;
  public navigation: any;
  public prevDisabled: string;
  public nextDisabled: string;
  public contentWidth: number;
  public wrapperWidth: any;
  public scrollWidth: any;
  public windowWidth: number;
  public isNavProfile: boolean;
  permissionsIDs = { ...Permissions };

  @Output() onNavMobCollapse = new EventEmitter();

  @ViewChild('navbarContent') navbarContent: ElementRef;
  @ViewChild('navbarWrapper') navbarWrapper: ElementRef;

  constructor(public nav: NavigationItem, private zone: NgZone, private location: Location,
    public authService: AuthService, private notificationService: NotificationService,
    public userPermissions: UserPermissionsService) {
    this.flatConfig = NextConfig.config;
    this.windowWidth = window.innerWidth;

    //this.navigation = this.nav.get();
    this.getModules();
    this.prevDisabled = 'disabled';
    this.nextDisabled = '';
    this.scrollWidth = 0;
    this.contentWidth = 0;

    this.isNavProfile = false;
  }

  ngOnInit() {
    if (this.windowWidth < 992) {
      this.flatConfig['layout'] = 'vertical';
      setTimeout(() => {
        document.querySelector('.pcoded-navbar').classList.add('menupos-static');
        (document.querySelector('#nav-ps-flat-able') as HTMLElement).style.maxHeight = '100%';
      }, 500);
    }
  }

  getModules() {
    this.authService.getModules()
      .then(data => {
        var NavigationItems = [
          {
            id: 'menu',
            title: 'Menú',
            type: 'group',
            icon: 'feather icon-monitor',
            children: []
          }
        ];
        if (data.length > 0) {
          var modulescollapse = data.filter(x => x.modulo.idpadre == 0);

          modulescollapse.forEach(module => {
            var addmodule = {
              id: module.id,
              title: module.modulo.nombre,
              type: 'collapse',
              icon: module.modulo.icon,
              children: []
            };
            data.filter(x => x.modulo.idpadre == module.modulo.id).forEach(submodule => {
              if (this.userPermissions.allowed(submodule.id)) {
                addmodule.children.push({
                  id: submodule.id,
                  title: submodule.modulo.nombre,
                  type: 'item',
                  icon: submodule.modulo.icon,
                  url: submodule.modulo.url,
                })
              }
            })
            if(addmodule.children.length > 0){
              NavigationItems.find(x => x.id == "menu").children.push(addmodule);
            }
          })
        }
        this.navigation = NavigationItems;
      })
      .catch(error => {
        console.log(error)
        this.notificationService.showError("Ha ocurrido un error al obtener los modulos", "Error")
      });
  }

  ngAfterViewInit() {
    if (this.flatConfig['layout'] === 'horizontal') {
      this.contentWidth = this.navbarContent.nativeElement.clientWidth;
      this.wrapperWidth = this.navbarWrapper.nativeElement.clientWidth;
    }
  }

  scrollPlus() {
    this.scrollWidth = this.scrollWidth + (this.wrapperWidth - 80);
    if (this.scrollWidth > (this.contentWidth - this.wrapperWidth)) {
      this.scrollWidth = this.contentWidth - this.wrapperWidth + 80;
      this.nextDisabled = 'disabled';
    }
    this.prevDisabled = '';
    if (this.flatConfig.rtlLayout) {
      (document.querySelector('#side-nav-horizontal') as HTMLElement).style.marginRight = '-' + this.scrollWidth + 'px';
    } else {
      (document.querySelector('#side-nav-horizontal') as HTMLElement).style.marginLeft = '-' + this.scrollWidth + 'px';
    }
  }

  scrollMinus() {
    this.scrollWidth = this.scrollWidth - this.wrapperWidth;
    if (this.scrollWidth < 0) {
      this.scrollWidth = 0;
      this.prevDisabled = 'disabled';
    }
    this.nextDisabled = '';
    if (this.flatConfig.rtlLayout) {
      (document.querySelector('#side-nav-horizontal') as HTMLElement).style.marginRight = '-' + this.scrollWidth + 'px';
    } else {
      (document.querySelector('#side-nav-horizontal') as HTMLElement).style.marginLeft = '-' + this.scrollWidth + 'px';
    }

  }

  fireLeave() {
    const sections = document.querySelectorAll('.pcoded-hasmenu');
    for (let i = 0; i < sections.length; i++) {
      sections[i].classList.remove('active');
      sections[i].classList.remove('pcoded-trigger');
    }

    let current_url = this.location.path();
    if (this.location['_baseHref']) {
      current_url = this.location['_baseHref'] + this.location.path();
    }
    const link = "a.nav-link[ href='" + current_url + "' ]";
    const ele = document.querySelector(link);
    if (ele !== null && ele !== undefined) {
      const parent = ele.parentElement;
      const up_parent = parent.parentElement.parentElement;
      const last_parent = up_parent.parentElement;
      if (parent.classList.contains('pcoded-hasmenu')) {
        parent.classList.add('active');
      } else if (up_parent.classList.contains('pcoded-hasmenu')) {
        up_parent.classList.add('active');
      } else if (last_parent.classList.contains('pcoded-hasmenu')) {
        last_parent.classList.add('active');
      }
    }
  }

  navMob() {
    if (this.windowWidth < 992 && document.querySelector('app-navigation.pcoded-navbar').classList.contains('mob-open')) {
      this.onNavMobCollapse.emit();
    }
  }

  fireOutClick() {
    let current_url = this.location.path();
    if (this.location['_baseHref']) {
      current_url = this.location['_baseHref'] + this.location.path();
    }
    const link = "a.nav-link[ href='" + current_url + "' ]";
    const ele = document.querySelector(link);
    if (ele !== null && ele !== undefined) {
      const parent = ele.parentElement;
      const up_parent = parent.parentElement.parentElement;
      const last_parent = up_parent.parentElement;
      if (parent.classList.contains('pcoded-hasmenu')) {
        if (this.flatConfig['layout'] === 'vertical') {
          parent.classList.add('pcoded-trigger');
        }
        parent.classList.add('active');
      } else if (up_parent.classList.contains('pcoded-hasmenu')) {
        if (this.flatConfig['layout'] === 'vertical') {
          up_parent.classList.add('pcoded-trigger');
        }
        up_parent.classList.add('active');
      } else if (last_parent.classList.contains('pcoded-hasmenu')) {
        if (this.flatConfig['layout'] === 'vertical') {
          last_parent.classList.add('pcoded-trigger');
        }
        last_parent.classList.add('active');
      }
    }
  }
}
