import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoadingService
{
  constructor()
  {
    this.LoadingSource$ = new Subject<boolean>();
    this.Loading$ = this.LoadingSource$.asObservable();
  }

  public Loading$: Observable<boolean>;
  private LoadingSource$: Subject<boolean>;

  public HideOverlay()
  {
    this.LoadingSource$.next(false);
  }

  public ShowOverlay()
  {
    this.LoadingSource$.next(true);
  }
}
