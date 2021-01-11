import { Component, OnInit, Output, EventEmitter, Input } from "@angular/core";
import { MatDialog } from "@angular/material";
import { AddMessageComponent } from "../add-message/add-message.component";
import { ToastrService } from "ngx-toastr";
import { Router } from "@angular/router";
import * as jwt_decode from "jwt-decode";
import { FormBuilder } from "@angular/forms";
import { first } from "rxjs/operators";
import { SearchDataService } from 'src/app/shared/search-data.service';
import {
  faTrash,
  faComment,
  faExpandAlt,
  faFileDownload,
  faDownload,
  faThumbsUp,
  faThumbsDown,
  faBell,
  faSearch,
  faBars,
  faSignOutAlt,
  faBuilding,
  faUserFriends,
  faNewspaper,
  faCalendarAlt,
  faBook,
  faFileAlt,
  faInbox
} from "node_modules/@fortawesome/free-solid-svg-icons";
import { News } from 'src/app/shared/message-detail.model';

@Component({
  selector: "app-show-message",
  templateUrl: "./show-message.component.html",
  styleUrls: ["./show-message.component.css"]
})
export class ShowMessageComponent implements OnInit {
  faExpandAlt = faExpandAlt;
  faTrash = faTrash;
  faComment = faComment;
  faFileDownload = faFileDownload;
  faDownload = faDownload;
  faThumbsUp = faThumbsUp;
  faThumbsDown = faThumbsDown;
  faBell = faBell;
  faSearch = faSearch;
  faBars = faBars;
  faSignOutAlt = faSignOutAlt;
  faBuilding = faBuilding;
  faUserFriends = faUserFriends;
  faNewspaper = faNewspaper;
  faCalendarAlt = faCalendarAlt;
  faBook = faBook;
  faFileAlt = faFileAlt;
  faInbox = faInbox;

  getEmail: any;
  searchText;
  deleteForm: any;
  restoreForm: any;
  countmessages: number;
  public showcountmessages: boolean;
  mobile: boolean = false;
  showSeachBar: boolean = false;
  countlikes: number = 0;
  isLikedByUser: boolean;
  public toggleSidebar: boolean = true;

  isAdmin: boolean = false;

  searchToggle: Boolean = false;

  messages: News[];
  searchMessages: News[];

  constructor(
    public dialog: MatDialog,
    public toastr: ToastrService,
    public router: Router,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {

    if (window.screen.width <= 660) {
      this.mobile = true;
      this.toggleSidebar = false;
    }

    this.deleteForm = this.formBuilder.group({
      IsDeleted: "true"
    });
    this.restoreForm = this.formBuilder.group({
      IsDeleted: "false"
    });
  }

  public toggleMenu() {
    if (this.toggleSidebar == true) {
      this.toggleSidebar = false;
    }
    else {
      this.toggleSidebar = true;
    }

  }

  searchMsgs(arg) {
    this.router.navigateByUrl("/News");
    this.searchToggle = true;
    this.search(arg);
  }

  search(searchValue) {
    var tempMsgs = [];

    for (let msg of this.messages) {
      if (msg.Title.includes(searchValue.trim())) {
        tempMsgs.push(msg);
      }
    }
    this.searchMessages = [];
    this.searchMessages = tempMsgs;
  }


  cancleToggleSearch() {
    this.searchToggle = false;
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(AddMessageComponent, {
      width: "800px",
      height: "auto"
    });
  }

  toggleSearchBar() {
    if (this.showSeachBar == false) {
      this.showSeachBar = true;
    }
    else {
      this.showSeachBar = false;
    }
  }

}
