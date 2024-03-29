import { Dropdown } from '../models/dropdown.js';
import { ViewMode } from './view-mode.js';
import { MainCalendar } from './main-calendar.js';
import { Modal } from '../views/pop-ups/modal.js';
import { Key } from '../models/share/key-bind.js';

export let Header = {
  data: {
    dropdown: null,

    cache: {
      userMenu: null
    },

    selectors: {
      s_avatar: '.avatar-small',
      s_date: '.header-date',
      s_next: '.switch-date #next',
      s_prev: '.switch-date #prev',
      s_today: '#today',      
      s_dropdownTrigger: '.view-mode',
      s_dropdownItem: '#view-mode a'
    },

    css: {
      stateClose: 'state-close'
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

    $(s.s_avatar).click(e => this.onOpenUserMenu(e));
    $(s.s_dropdownItem).click((e) => this.onDropdownSelect(e));
    $(s.s_next).click(() => this.onNext());
    $(s.s_prev).click(() => this.onPrev());
    $(s.s_today).click(() => this.onToday());        
  },

  onOpenUserMenu(e) {
    Modal.open(this.onCloseUserMenu);

    let close = this.data.css.stateClose;
    let menu = e.currentTarget.nextElementSibling;    
    $(menu).removeClass(close);    
    
    this.data.cache.userMenu = menu;
    Key.ecs(() => this.onCloseUserMenu());
  },

  onCloseUserMenu() {   
    let _this = Header;
    
    let close = _this.data.css.stateClose;
    let menu = _this.data.cache.userMenu;
    
    $(menu).addClass(close);
    Modal.close();
  },

  onNext() {
    MainCalendar.setDate(
      moment(new Date(sessionStorage.getItem('currentDate')))
        .add(1, 'days')
        .startOf('day')
        .toDate()
    );
  },

  onPrev() {
    MainCalendar.setDate(
      moment(new Date(sessionStorage.getItem('currentDate')))
      .add(-1, 'days')
      .startOf('day')
      .toDate()
    );    
  },

  onToday() {
    MainCalendar.setDate(
      moment(new Date())
      .startOf('day')
      .toDate()
    );  
    
    sessionStorage.setItem('currentDate', new Date());
  },

  onDropdownSelect(e) {
    // TODO: change view mode
    this.data.dropdown.clickHandler(e);
  },

  renderDate(date) {
    let s = this.data.selectors;

    $(s.s_date).text(moment(date).format('MMMM YYYY'));
  },

  renderDropdown() {
    let s = this.data.selectors;

    this.data.dropdown = new Dropdown(s.s_dropdownTrigger, { constrainWidth: false });
    this.data.dropdown.runDropdown();
  }
};
