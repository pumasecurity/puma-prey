import { Component } from '@angular/core';
import { faPenToSquare} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  faPenToSquare = faPenToSquare;
}

