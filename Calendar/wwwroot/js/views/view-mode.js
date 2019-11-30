import { Daily } from './view-modes/daily.js';
import { EventForm } from './event-form.js';
import { EventInfo } from './pop-ups/event-info.js';

export let ViewMode = {
  data: {
    current: null,
    selectors: {
      s_createEventBtn: '.create-event-btn'      
    }
  },

  run() {
    Daily.run();
    this.data.current = Daily;
    this.setUpListeners();
    this.openSharedEvent();
  },

  setUpListeners() {
    let s = this.data.selectors;

    $(s.s_createEventBtn).click(() => this.onCreateEvent());
  },

  openSharedEvent() {
    var urlString = decodeURIComponent(window.location.href);        
    let find = "event=";
    let idx = urlString.indexOf(find)

    if (idx != -1) {
      let event = urlString.substring(idx + find.length);  

      EventForm.openShared(JSON.parse(event));
      window.history.pushState({}, document.title, "/");
    }
  },
  
  onCreateEvent() {
    let startTime = moment(new Date(sessionStorage.getItem('currentDate')))
                    .startOf('hour')
                    .toDate();
    
    let finishTime = moment(startTime).add(1, 'hours').toDate();

    this.createEvent();
    EventForm.openCreate(startTime, finishTime);
  },

  showEventInfo(id) {
    EventInfo.open(id);
  },

  clear() {
    this.data.current.clear();
  },

  renderDate(date) {    
    this.data.current.renderDate(date);
  },  

  renderAllDayEvent(selector, id, title, color) {
    this.data.current.renderAllDayEvent(selector, id, title, color);
  },

  renderEvent(selector, id, title, start, finish, color) {    
    let container = this.data.current.findCellByTime(start); 

    this.data.current.renderEvent(container, selector, id, title, start, finish, color);    
  },

  changeEventCalendar(id) {
    this.data.current.changeEventCalendar(id);
  },

  showEventsByCalendarId(calendarId) {
    this.data.current.showEventsByCalendarId(calendarId);
  },

  hideEventsByCalendarId(calendarId) {
    this.data.current.hideEventsByCalendarId(calendarId);
  },

  createEvent(hour = new Date().getHours()) {    
    let container = document.getElementById(`cell-${hour}`);

    this.data.current.createDefaultEvent(container);
  },

  renderEvents(events) {
    this.data.current.renderEvents(events);
  },

  changeEventPosition(selector, title, start, finish) {    
    this.data.current.changeEventPosition(selector, title, start, finish);
  },

  setEventTitle(title, id) {
    this.data.current.setEventTitle(title, id);
  },

  setEventTime(start, finish, id) {
    this.data.current.setEventTime(start, finish, id);
  },
  
  cacheEvent(selector) {
    this.data.current.cacheEvent(selector);
  },

  cacheColor(color) {
    this.data.current.cacheColor(color);
  },

  getCachedEvent() {
    return this.data.current.getCachedEvent();
  },

  getCachedColor() {    
    return this.data.current.getCachedColor();
  },

  eventRollback() {
    this.data.current.eventRollback();
  },

  cacheRollback(settings) {
    this.data.current.cacheRollbackSettings(settings);
  },

  deleteEvent(selector) {    
    let el = document.getElementById(selector);

    setTimeout(() => {
      $(el).remove();
    }, 150);    
  }
};