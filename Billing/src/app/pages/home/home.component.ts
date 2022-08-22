import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DxButtonModule } from 'devextreme-angular';
import { navigation } from '../../app-navigation';


@Component({
  selector: 'app-home',
  templateUrl: 'home.component.html',
  styleUrls: [ './home.component.scss' ]
})

export class HomeComponent implements OnInit {
  constructor(private router: Router) {}

  ngOnInit(): void {
  }

  page(ruta: any){
    this.router.navigateByUrl(`/${ruta}`);
  }

  private _items!: Record <string, unknown>[];
  get items() {
    if (!this._items) {
      this._items = navigation.map((item) => {
        if(item.path && !(/^\//.test(item.path))){
          item.path = `/${item.path}`;
        }
         return { ...item}
        });
    }
    return this._items;
  }

}
