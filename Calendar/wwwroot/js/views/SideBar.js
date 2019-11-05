import { MainCalendar } from './MainCalendar.js';
import { EventForm } from './EventForm.js';
import { ViewMode } from './ViewMode.js';

let SideBar = {
  data: {
    selectors: {
      s_createEventBtn: '.create-event-btn'      
    }
  },

  run() {
    MainCalendar.run();
    this.setUpListeners();
  },  

  setUpListeners() {
    let s = this.data.selectors;

    $(s.s_createEventBtn).click(() => this.onCreateEvent());
  },
  
  onCreateEvent() {
    let date = new Date(sessionStorage.getItem('currentDate'));
    date.setMinutes(0);

    ViewMode.renderEvent();
    EventForm.open(date);
  }
};

export { SideBar };