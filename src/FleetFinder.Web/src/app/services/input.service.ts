import {BehaviorSubject} from 'rxjs'
import {icons} from "../components/ui/input/icons.data";
import {DomSanitizer, SafeHtml} from "@angular/platform-browser";

export class InputService {
  isTransform$ = new BehaviorSubject<boolean>(false)
  isFocus$ = new BehaviorSubject<boolean>(false)
  id: string;
  icon: string;
  svg: SafeHtml | null = null;
  colorBlueSaturated: string = "#3F68CF";
  colorBlack75: string = "#565656"
  disableTransform: boolean;

  constructor(sanitizer: DomSanitizer, icon : string) {
    this.id = this.generateGuid();
    this.icon = icon;
    this.initSvg(sanitizer);
  }

  switchState = (isTransform: boolean) => this.isTransform$.next(isTransform || this.disableTransform)
  onFocus = () => this.isFocus$.next(true);
  onBlur = () => this.isFocus$.next(false);

  onFocusEvent (){
    this.changeColorSvg(this.colorBlueSaturated)
    this.switchState(true);
    this.onFocus();
  }

  onBlurEvent(isTransform: boolean){
    this.changeColorSvg(this.colorBlack75)
    this.switchState(isTransform);
    this.onBlur();
  }

  changeColorSvg (color: string) : void {
    const el = this.getElementIcon();
    if (el) el.style.fill = color;
  }

  blockState(){
    this.disableTransform = true;
    this.switchState(true);
  }

  private initSvg(sanitizer: DomSanitizer) : void{
    const icon = icons.find(i => i.Name === this.icon);
    if (icon)
      this.svg = sanitizer.bypassSecurityTrustHtml(icon.Svg);
    setTimeout(() => {
      const el = this.getElementIcon();
      if (el) el.style.transition = "ease-in-out 0.15s";
    })
  }

  private getElementIcon() : HTMLElement | null | undefined {
    const root = document.getElementById(this.id);
    return root?.querySelector(`#${this.icon}`);
  }

  private generateGuid() : string {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
      let r = Math.random() * 16 | 0,
        v = c == 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }
}
