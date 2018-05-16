import { ShoppingListItem } from './shopping-list-item';

export interface ShoppingList
{
  Id?: number;
  CreatedOn?: Date;
  LastUpdatedOn?: Date;

  Items?: ShoppingListItem[];
}
