import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-monkey-report-table',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './monkey-report-table.component.html'
})
export class MonkeyTableComponent {
  @Input() data: any[] = [];
  @Input() columns: { field: string, header: string }[] = [];
}
