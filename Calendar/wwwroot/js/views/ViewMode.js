import { Daily } from './view-modes/Daily.js';

let ViewMode = {
  data: {
    current: null,
  },

  run() {
    Daily.run();
    this.data.current = Daily;
  },

  renderDate(date) {    
    this.data.current.renderDate(date);
  },

  renderEvent(hour = new Date().getHours()) {    
    let container = document.getElementById(`cell-${hour}`);

    this.data.current.onCreateEvent(container);
  },

  changeEventPosition(guid, title, start, end) {    
    this.data.current.changeEventPosition(guid, title, start, end);
  },

  setEventTitle(title, id) {
    this.data.current.setEventTitle(title, id);
  },

  setEventTime(start, end, id) {
    this.data.current.setEventTime(start, end, id);
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