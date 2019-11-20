import { Header } from './views/Header.js';
import { SideBar } from './views/SideBar.js';
import { ViewMode } from './views/ViewMode.js';
import { EventRepository } from './models/mvc/EventRepository.js';
import { CalendarList } from './views/CalendarList.js';

$(function() {
  let app = {
    async run() {      
      let date = new Date();
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