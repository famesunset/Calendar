import { MainCalendar } from './main-calendar.js';


export let SideBar = {
  data: {
    selectors: {
      s_createEventBtn: '.create-event-btn'      
    }
  },

  run() {
    MainCalendar.run();    
  }
};