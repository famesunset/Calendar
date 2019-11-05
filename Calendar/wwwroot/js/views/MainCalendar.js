import { DatePicker } from '../models/DatePicker.js';
import { ViewMode } from './ViewMode.js';
import { Header } from './Header.js';

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

  onSelect(date) {
    let currentDate = new Date(sessionStorage.getItem('currentDate'));    
    date.setHours(
      currentDate.getHours(),
      currentDate.getMinutes()
    );

    Header.renderDate(date);
    ViewMode.renderDate(date);

    sessionStorage.setItem('currentDate', date);
  },

  render() {
    let _this = this;
    let container = this.data.selectors.s_calendarWrapper;
    let input = this.data.selectors.s_input;

    this.data.calendar = new DatePicker(input, {
        container: container,
        setDefaultDate: true,
        defaultDate: new Date(),
        firstDay: 1,
        animation: false,
        onSelect: (date) => _this.onSelect(date)
    });
    
    this.data.calendar.runDatePicker();  
    this.data.calendar.open();
  },

  setDate(date) {
    this.data.calendar.setDate(date);
  }
};

export { MainCalendar };