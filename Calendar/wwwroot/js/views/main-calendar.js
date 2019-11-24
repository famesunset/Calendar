import { DatePicker } from '../models/date-picker.js';
import { ViewMode } from './view-mode.js';
import { Header } from './header.js';
import { EventRepository } from '../models/mvc/event-repository.js';
import { CalendarList } from './calendar-list.js';

export let MainCalendar = {
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
    if (!this.validDate(date)) 
      return;

    ViewMode.close();     

    Header.renderDate(date);
    ViewMode.renderDate(date);    

    let calendars = CalendarList.getSelectedCalendars();
    let events = await  new EventRepository().getList(date, calendars);
    ViewMode.renderEvents(events);

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
  },

  validDate(date) {
    let userSelectedDate = new Date(sessionStorage.getItem('currentDate'));
    let mUserSelectedDate = moment(userSelectedDate).startOf('day');
    let mNewDate = moment(date).startOf('day');

    return !mUserSelectedDate.isSame(mNewDate);
  }
};