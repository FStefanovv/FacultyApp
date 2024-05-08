import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SelectionItem } from '../../models/selectionItem';

@Component({
  selector: 'dropdown-select',
  templateUrl: './dropdown-select.component.html',
  styleUrl: './dropdown-select.component.css'
})
export class DropdownSelectComponent implements OnInit {
  @Input()
  itemType!: string;

  @Input()
  items: SelectionItem[] = [];

  @Input()
  selectedItem!: string;

  @Output()
  selectionChange = new EventEmitter<string>();

  constructor(){}

  ngOnInit(){
    if(this.selectedItem)
      this.selectionChange.emit(this.selectedItem);
  }

  onChange(){
    this.selectionChange.emit(this.selectedItem);
  }


}
