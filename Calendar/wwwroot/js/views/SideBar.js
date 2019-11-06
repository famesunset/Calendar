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
    let start = new Date(sessionStorage.getItem('currentDate'));
        start.setMinutes(0);
    
    let end = new Date(start);
        end.setHours(start.getHours() + 1);

    ViewMode.renderEvent();
    EventForm.open(start, end);
  }
};

export { SideBar };