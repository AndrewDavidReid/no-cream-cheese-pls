import { Action } from "@ngrx/store";
import { ShoppingList } from "../../models/shopping-list";
import { ApiError } from "../../../shared/models/api-error";

export enum ShoppingListActionTypes {
  LOAD_SHOPPING_LISTS = "[Shopping List] Load Shopping Lists",
  LOAD_SHOPPING_LISTS_FAIL = "[Shopping List] Load Shopping Lists Success",
  LOAD_SHOPPING_LISTS_SUCCESS = "[Shopping List] Load Shopping Lists Failure"
}

export class LoadShoppingLists implements Action {
  readonly type: string = ShoppingListActionTypes.LOAD_SHOPPING_LISTS;
}

export class LoadShoppingListsSuccess implements Action {
  readonly type: string = ShoppingListActionTypes.LOAD_SHOPPING_LISTS_SUCCESS;

  constructor(public payload: ShoppingList[])
  {}
}

export class LoadShoppingListsFailure implements Action {
  readonly type: string = ShoppingListActionTypes.LOAD_SHOPPING_LISTS_SUCCESS;

  constructor(public payload: ApiError)
  {}
}

export type LoadShoppingListActions = LoadShoppingLists | LoadShoppingListsFailure | LoadShoppingListsSuccess;
