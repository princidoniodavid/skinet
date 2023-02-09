import { Component, OnInit } from '@angular/core';
import {BasketService} from "../../basket/basket.service";
import {Observable} from "rxjs";
import {IBasket} from "../../shared/models/basket";
import {AccountService} from "../../account/account.service";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
basket$: Observable<IBasket>
  constructor(private basketService: BasketService, public accountService: AccountService) { }

  ngOnInit(): void {
  this.basket$ = this.basketService.basket$;
  }

}
