import { Daily } from './view-modes/Daily.js';

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

  renderAllDayEvent(selector, id, title) {
    this.data.current.renderAllDayEvent(selector, id, title);
  },

  renderDefaultEvent(selector, id, title, start, finish) {    
    let container = this.data.current.findCellByTime(start); 

    this.data.current.renderEvent(container, selector, id, title, start, finish);    
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

  getCachedEvent() {
    return this.data.current.getCachedEvent();
  },

  deleteEvent(selector) {    
    let el = document.getElementById(selector);

    setTimeout(() => {
      $(el).remove();
    }, 150);    
  }
};