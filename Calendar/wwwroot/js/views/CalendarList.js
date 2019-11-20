import { ViewMode } from "./ViewMode.js";
import { CalendarRepository } from '../models/mvc/CalendarRepository.js';

export let CalendarList = {
  data: {
    selectors: {
      s_calendarList: '#calendar-list',
      s_calendar: '.calendar',
      s_displayCalendar: '.display-calendar',
      s_deleteCalendar: '.delete-calendar'
    },

    url: {
      u_loadList: '/CalendarView/GetList'
    }
  },

  run(callback) {
    this.renderCalendarList(callback);
  },

  setUpListeners() {
    let s = this.data.selectors;

    $(s.s_displayCalendar).change(e => this.onShowCalendarEvents(e));
    $(s.s_deleteCalendar).click(e => this.onDeleteCalendar(e));
    $(s.s_calendar).mouseenter(e => this.onShowDeleteBtn(e));
    $(s.s_calendar).mouseleave(e => this.onHideDeleteBtn(e));
  },

  renderCalendarList(callback) {
    let url = this.data.url.u_loadList;
    let container = this.data.selectors.s_calendarList;

    $(container).load(url, () => {
      this.setUpListeners();
      callback();
    });
  },

  onShowCalendarEvents(e) {
    let target = e.currentTarget;
    let checked = target.checked;
    let root = target.parentElement.parentElement;
    let id = $(root).find('input[name="calendarId"]').val();

    if (checked) {      
      ViewMode.showEventsByCalendarId(id);
    } else {
      ViewMode.hideEventsByCalendarId(id);
    }
  },

  onDeleteCalendar(e) {
    let root = e.currentTarget.parentElement;
    let id = $(root).find('input[name="calendarId"]').val();
    
    // new CalendarRepository().delete(id);
  },

  onShowDeleteBtn(e) {
    let s = this.data.selectors;
    let target = e.currentTarget;
    let deleteBtn = $(target).find(s.s_deleteCalendar);
    
    $(deleteBtn).css('display', 'flex');
  },

  onHideDeleteBtn(e) {
    let s = this.data.selectors;
    let target = e.currentTarget;
    let deleteBtn = $(target).find(s.s_deleteCalendar);
    
    $(deleteBtn).css('display', 'none');
  },

  getSelectedCalendars() {
    let calendars = $(this.data.selectors.s_calendar);
    let checkedArray = [];

    if (calendars.length != 0) {
      for(let calendar of calendars) {      
        let checked = $(calendar).find('input[type="checkbox"]')[0].checked;
        if (checked) {
          let id = $(calendar).find('input[name="calendarId"]').val();
          checkedArray.push(+id);
        }        
      }
    }

    return checkedArray;
  },
};