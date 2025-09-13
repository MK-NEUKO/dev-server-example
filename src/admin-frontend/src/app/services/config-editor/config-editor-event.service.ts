import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConfigEditorEventService {
  public mouseDown$ = new Subject<MouseEvent>();
  public keyDown$ = new Subject<KeyboardEvent>();

  constructor() { }

  emitMouseDown(event: MouseEvent) {
    this.mouseDown$.next(event);
  }

  emitKeyDown(event: KeyboardEvent) {
    this.keyDown$.next(event);
  }
}
