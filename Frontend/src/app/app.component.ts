import { AfterViewChecked, ChangeDetectorRef, Component } from '@angular/core';
import { LoaderService } from './services/loader.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements AfterViewChecked {
  constructor(
    public loaderService: LoaderService,
    private changeDetector: ChangeDetectorRef
  ) {}
  ngAfterViewChecked() {
    this.changeDetector.detectChanges();
  }
}
