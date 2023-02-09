import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AnimationModalComponent } from './animation-modal/animation-modal.component';
import { UiModalComponent } from './ui-modal/ui-modal.component';



@NgModule({
  declarations: [
    AnimationModalComponent,
    UiModalComponent
  ],
  exports: [UiModalComponent, AnimationModalComponent],
  imports: [
    CommonModule
  ]
})
export class ModalModule { }
