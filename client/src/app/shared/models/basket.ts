import {IBasketItem} from "./basket-item";
import {v4 as uuid} from "uuid";


export interface IBasket {
  id: string
  items: IBasketItem[]
}

export class Basket implements IBasket {
  id = uuid();
  items: IBasketItem[] = [];
}
