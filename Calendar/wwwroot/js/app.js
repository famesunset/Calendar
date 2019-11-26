import { Header } from './views/header.js';
import { SideBar } from './views/side-bar.js';
import { ViewMode } from './views/view-mode.js';
import { EventRepository } from './models/mvc/event-repository.js';
import { CalendarList } from './views/calendar-list.js';

$(function() {
  let app = {
    async run() {    
      let sessionDate = sessionStorage.getItem('currentDate');          
      let date = sessionDate != null ? new Date(sessionDate) : new Date();

      sessionStorage.setItem('currentDate', 
        moment(date).startOf('day').toDate());

      Header.run();
      SideBar.run();
      CalendarList.run(() => {
        ViewMode.run();         
      });      
    }
  };

  app.run();
});