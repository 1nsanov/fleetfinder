import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-layout-header-nav',
  templateUrl: './layout-header-nav.component.html',
  styleUrls: ['./layout-header-nav.component.scss']
})
export class LayoutHeaderNavComponent implements OnInit{
  ngOnInit(): void {
    this.fixedHeaderByScroll()
  }

  fixedHeaderByScroll(){
    const isPreviewHeader = true //TODO: add valid onroute;
    const header = document.getElementById("header-nav");
    const backgroundContent = document.getElementById("background-content");
    if (!header || !backgroundContent) return;
    if (!isPreviewHeader) {
      header.classList.add("fixed-header");
      backgroundContent.classList.add("margin-top-content")
      return;
    }
    window.onscroll = function() {
      if (window.pageYOffset > 236) {
        header.classList.add("fixed-header");
        backgroundContent.classList.add("margin-top-content")
      } else {
        header.classList.remove("fixed-header");
        backgroundContent.classList.remove("margin-top-content")
      }
    };
  }
}
