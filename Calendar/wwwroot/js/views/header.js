import { Dropdown } from '../models/Dropdown.js';
import { ViewMode } from './ViewMode.js';
import { MainCalendar } from './MainCalendar.js';
import { DateParse } from '../models/share/DateParse.js';

let Header = {
  data: {
    dropdown: null,

    selectors: {
      s_date: '.header-date',
      s_next: '.switch-date #next',
      s_prev: '.switch-date #prev',
      s_today: '#today',
      s_dropdownTrigger: '.view-mode',
      s_dropdownItem: '#view-mode a'
    }
  },

  run() {
    let date = new Date(sessionStorage.getItem('currentDate'));

    this.renderDate(date);
    this.renderDropdown();
    this.setUpListeners();
  },

  setUpListeners() {
    let s = this.data.selectors;

    $(s.s_dropdownItem).click((e) => this.onDropdownSelect(e));
    $(s.s_next).click(() => this.onNext());
    $(s.s_prev).click(() => this.onPrev());
    $(s.s_today).click(() => this.onToday());
  },

  onNext() {
    let date = new Date(sessionStorage.getItem('currentDate'));
    date.setDate(date.getDate() + 1);

    MainCalendar.setDate(date);
  },

  onPrev() {
    let date = new Date(sessionStorage.getItem('currentDate'));
    date.setDate(date.getDate() - 1);

    MainCalendar.setDate(date);    
  },

  onToday() {
    MainCalendar.setDate(new Date());  
  },

  onDropdownSelect(e) {
    // TODO: change view mode
    this.data.dropdown.clickHandler(e);
  },

  renderDate(date) {
    let s = this.data.selectors;
    var dateParse = new DateParse(date);

    let year = date.getYear() + 1900;
    let month = dateParse.getMonthOfYear();

    $(s.s_date).text(`${month} ${year}`);
  },

  renderDropdown() {
    let s = this.data.selectors;

    this.data.dropdown = new Dropdown(s.s_dropdownTrigger, { constrainWidth: false });
    this.data.dropdown.runDropdown();
  }
};

export { Header };