import { Header } from './views/Header.js';
import { SideBar } from './views/SideBar.js';
import { ViewMode } from './views/ViewMode.js';
import { EventRepository } from './models/mvc/EventRepository.js';
import { ViewLoader } from './models/share/ViewLoader.js';

$(function() {
  let app = {
    async run() {      
      sessionStorage.setItem('currentDate', new Date());
      
      Header.run();
      SideBar.run();
      ViewMode.run();   
    }
  };

  app.run();
});