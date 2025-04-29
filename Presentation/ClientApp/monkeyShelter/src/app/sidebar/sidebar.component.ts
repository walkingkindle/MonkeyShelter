import { Component } from '@angular/core';
import { RouterLink, RouterModule } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  imports: [RouterModule,RouterLink],
  templateUrl: './sidebar.component.html',
  standalone:true,
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {

}
