import { Header } from './views/header.js';
import { SideBar } from './views/side-bar.js';
import { ViewMode } from './views/view-mode.js';
import { CalendarList } from './views/calendar-list.js';
import { UserMenu } from './views/user-menu.js';

$(function() {
  let app = {
    run() {    
      let sessionDate = sessionStorage.getItem('currentDate');          
      let date = sessionDate != null ? new Date(sessionDate) : new Date();            

      sessionStorage.setItem('currentDate', 
        moment(date).startOf('day').toDate());      

      Header.run();
      SideBar.run();
      CalendarList.run(() => {        
        ViewMode.run();   
      });          
      UserMenu.run();            
    },
  };

  app.run();
});