import { DatePicker } from '../models/DatePicker.js';
import { ViewMode } from './ViewMode.js';
import { Header } from './Header.js';
import { EventRepository } from '../models/mvc/EventRepository.js';

let MainCalendar = {
  data: {
    calendar: null,

    selectors: {
      s_calendarWrapper: '.mini-calendar',
      s_input: '#mini-calendar',
    }    
  },

  run() {    
    this.render();
  },

  async onSelect(date) {
    ViewMode.closeAnimation();     

    Header.renderDate(date);
    ViewMode.renderDate(date);    

    let events = await  new EventRepository().getList(date);
    ViewMode.renderEvents(events);
    
    ViewMode.openAnimation();

    let currentDate = new Date(date);        
    sessionStorage.setItem('currentDate', currentDate);
  },

  render() {
    let _this = this;
    let container = this.data.selectors.s_calendarWrapper;
    let input = this.data.selectors.s_input;

    this.data.calendar = new DatePicker({
        container: container,
        setDefaultDate: true,
        defaultDate: new Date(),
        firstDay: 1,
        animation: false,
        onSelect: (date) => _this.onSelect(date)
    }, input);
    
    this.data.calendar.runDatePicker();  
    this.data.calendar.open();
  },

  setDate(date) {
    this.data.calendar.setDate(date);
  }
};

export { MainCalendar };