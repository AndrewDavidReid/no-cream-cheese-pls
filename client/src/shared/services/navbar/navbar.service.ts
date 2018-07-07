import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NavbarService
{
  constructor()
  {
    this.TitleSource$ = new Subject<string>();
    this.Title$ = this.TitleSource$.asObservable();
  }

  public Title$: Observable<string>;
  private TitleSource$: Subject<string>;

  public SetTitle(titleP: string)
  {
    this.TitleSource$.next(titleP);
  }
}
