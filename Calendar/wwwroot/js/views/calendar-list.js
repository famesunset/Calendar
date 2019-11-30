import { ViewMode } from "./view-mode.js";
import { CalendarRepository } from '../models/mvc/calendar-repository.js';
import { CreateCalendar } from '../views/pop-ups/create-calendar.js'
import { Modal } from "./pop-ups/modal.js";
import { ShareCalendar } from './pop-ups/share-calendar.js';
import { PopUp } from "./pop-ups/pop-up.js";
import { FetchContent } from "../models/mvc/fetch-content.js";

export let CalendarList = {
  data: {
    cache: {
      tooltips: []
    },

    selectors: {
      s_calendarList: '#calendar-list',
      s_calendars: '.calendar-list',
      s_calendar: '.calendar',
      s_calendarActions: '.calendar-actions',
      s_displayCalendar: '.calendar-events-checkbox',
      s_unsubscribeCalendar: '.unsubscribe-calendar',
      s_deleteCalendar: '.delete-calendar',      
      s_calendarFormTarget: '#create-calendar-target',
      s_calendarFormContainer: '#create-calendar',    
      s_calendarFormContent: '.create-calendar', 
      s_shareCalendarTrigger: '.share-calendar'
    },

    url: {
      u_deleteView: '/PopUp/DeleteCalendarMessage',
      u_loadList: '/CalendarView/GetList',
      u_calendarView: '/CalendarView/GetCalendarView',
      u_loadCalendarForm: 'CalendarView/GetCreateCalendarForm'
    }
  },

  run(callback) {
    this.renderCalendarList(callback);
  },

  setUpListeners() {
    let s = this.data.selectors;

    $(s.s_calendarFormTarget).click(() => this.onOpenCreationMenu())
    $(s.s_displayCalendar).change(e => this.onShowCalendarEvents(e));
    $(s.s_deleteCalendar).click(e => this.onDeleteCalendar(e));
    $(s.s_unsubscribeCalendar).click(e => this.onUnsubscribeCalendar(e));
    $(s.s_shareCalendarTrigger).click(e => this.onShareMenu(e));    

    this.data.cache.tooltips = $('.tooltipped').tooltip({
      inDuration: 0, 
      outDuration: 0,  
      enterDelay: 300,
      margin: 0,
      transitionMovement: 5
    });
  },

  renderCalendarList(callback) {
    let url = this.data.url.u_loadList;
    let container = this.data.selectors.s_calendarList;

    FetchContent.get(url, content => {
      $(container).html(content);
      this.setUpListeners();
      callback();      
    });
  },

  async addCalendar(name, colorId) {
    if (!name || name.trim() === '') {
      name = '(No name)';
    }

    let id = await new CalendarRepository().insert(name, colorId);
    let url = this.data.url.u_calendarView + `?id=${id}`;
    let container = this.data.selectors.s_calendars;

    FetchContent.get(url, content => {
      $(container).append(content);  
      this.setUpListeners();
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

  onOpenCreationMenu() {    
    CreateCalendar.open();
  },

  onDeleteCalendar(e) {
    let root = e.currentTarget.parentElement.parentElement;
    let id = $(root).find('input[name="calendarId"]').val();
    
    this.loadDeleteMessage(id, (content) => {
      PopUp.open(content, (result) => {
        let response = PopUp.data.response;

        if (result == response.SUBMIT) {
          ViewMode.hideEventsByCalendarId(id);
          new CalendarRepository().delete(id);
          this.hideToolTips();   
          $(root).remove();
        }
      });
    });
  },

  onUnsubscribeCalendar(e) {
    let root = e.currentTarget.parentElement.parentElement;
    let id = $(root).find('input[name="calendarId"]').val();
    
    this.loadDeleteMessage(id, content => {
      PopUp.open(content, result => {
        let response = PopUp.data.response;

        if (result == response.SUBMIT) {
          ViewMode.hideEventsByCalendarId(id);
          new CalendarRepository().unsubscribe(id);
          this.hideToolTips();   
          $(root).remove();
        }
      });
    });
  },

  onShowAction(e) {
    let s = this.data.selectors;
    let target = e.currentTarget;
    let actions = $(target).find(s.s_calendarActions);
    
    $(actions).css('display', 'flex');
  },

  onHideAction(e) {
    let s = this.data.selectors;
    let target = e.currentTarget;
    let actions = $(target).find(s.s_calendarActions);
    
    $(actions).css('display', 'none');
  },

  onShareMenu(e) {    
    let root = e.currentTarget.parentElement.parentElement;    
    let id = $(root).find('input[name="calendarId"]').val();   
    ShareCalendar.openForm(id, { x: e.pageX, y: e.pageY }, root);
  },

  loadDeleteMessage(id, callback) {
    let url = this.data.url.u_deleteView + `?id=${id}`;
    FetchContent.get(url, callback);
  },

  hideToolTips() {
    for (let tooltip of this.data.cache.tooltips) {
      var instance = M.Tooltip.getInstance(tooltip);
      instance.close();
    }
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

  getDefaultCalendarColor() {
    let calendar = $(this.data.selectors.s_calendar)[0];    
    let checkbox = $(calendar).find('span')[0];
    let style = window.getComputedStyle(checkbox);
    let color = style.getPropertyValue('--checkbox-color')

    return color;
  },

  findRootById(calendarId) {
    let calendars = $(this.data.selectors.s_calendar);

    for (let calendar of calendars) {
      let id = $(calendar).find('input[name="calendarId"]').val();
      if (id == calendarId) {
        return calendar;
      }
    }

    return null;
  }
};