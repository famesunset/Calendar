import { Daily } from './view-modes/Daily.js';

let ViewMode = {
  data: {
    current: null,
  },

  run() {
    Daily.run();
    this.data.current = Daily;
  },

  openAnimation() {
    this.data.current.tableOpenAnimation();
  },
  
  closeAnimation() {
    this.data.current.tableCloseAnimation();
  },

  renderDate(date) {    
    this.data.current.renderDate(date);
  },

  renderEvent(hour = new Date().getHours()) {    
    let container = document.getElementById(`cell-${hour}`);

    this.data.current.onCreateEvent(container);
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
  
  getLastEventId() {
    return this.data.current.lastEventId();
  },

  removeLastEvent() {
    let id = this.data.current.lastEventId();
    let el = document.getElementById(id);

    $(el).remove();
  }
};

export { ViewMode };