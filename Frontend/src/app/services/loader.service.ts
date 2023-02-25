import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LoaderService {
  loader: boolean = false;
  constructor() {}

  loaderState(state: boolean = true) {
    this.loader = state;
  }
}
