import { Directive, Input, ElementRef, Renderer2, OnInit, SimpleChanges, OnChanges } from '@angular/core';

@Directive({
  selector: '[CheckCancellability]'
})
export class CheckCancellabilityDirective implements OnInit, OnChanges {

  @Input() status!: number;
  @Input() scheduledFor!: Date;

  constructor(private el: ElementRef, private renderer: Renderer2) { }

  ngOnInit() {
    this.setVisibility();
  }

  ngOnChanges(changes: SimpleChanges) {
    if ('status' in changes) {
      this.setVisibility();
    }
  }

  private setVisibility() {
    const currentDate = new Date();
    const twoDaysBeforeScheduled = new Date(this.scheduledFor);
    twoDaysBeforeScheduled.setDate(twoDaysBeforeScheduled.getDate() - 2);

    if (this.status === 0 && currentDate <= twoDaysBeforeScheduled) {
      //this.renderer.addClass(this.el.nativeElement, 'btn-cancel');
      this.renderer.setStyle(this.el.nativeElement, 'display', 'inline-block');
    } else {
      //this.renderer.removeClass(this.el.nativeElement, 'btn-cancel');
      this.renderer.setStyle(this.el.nativeElement, 'display', 'none');
    }
  }
}
