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
    let startTime = moment(new Date(sessionStorage.getItem('currentDate')))
                    .startOf('hour')
                    .toDate();
    
    let finishTime = moment(startTime).add(1, 'hours').toDate();

    ViewMode.renderEvent();
    EventForm.open(startTime, finishTime);
  }
};

export { SideBar };