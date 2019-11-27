import { Daily } from './view-modes/daily.js';

export let ViewMode = {
  data: {
    current: null,
  },

  run() {
    Daily.run();
    this.data.current = Daily;
  },
  
  close() {
    this.data.current.close();
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