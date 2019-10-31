import { DateParse } from '../../models/share/DateParse.js';
import { DatePicker } from '../../models/DatePicker.js';

export let dailyViewMode = {
  data: {
    calendar: null,
    
    selectors: {
      s_calendarWrapper: '.mini-calendar',
      s_calendar: '#mini-calendar',
      s_headerDate: '.header-date',
      s_dayOfWeek: '.date .day-of-week',
      s_day: '.date .day',
      s_table: '#table-events'
    }
  },

  run: function() {
    this.intializeCalendar();
    this.renderTable();
    this.updateViewDate(new Date(sessionStorage.getItem('currentDate')));
  },

  // --- Listeners ---- //

  onNext: function() {
    let currentDate = new Date(sessionStorage.getItem('currentDate'));
    let nextDay = new Date(currentDate);
    nextDay.setDate(currentDate.getDate() + 1);

    this.data.calendar.setDate(nextDay);
  },

  onPrev: function() {
    let currentDate = new Date(sessionStorage.getItem('currentDate'));
    let prevDay = new Date(currentDate);
    prevDay.setDate(currentDate.getDate() - 1);

    this.data.calendar.setDate(prevDay);
  },

  onToday: function() {
    this.data.calendar.setDate(new Date());
  },

  onUpdateDate: function(date) {
    this.updateHeaderDate(date);
    this.updateViewDate(date);

    sessionStorage.setItem('currentDate', date);
  },

  updateHeaderDate: function(date) {
    let s = this.data.selectors;
    var dateParse = new DateParse(date);

    let year = date.getYear() + 1900;
    let month = dateParse.getMonthOfYear();

    $(s.s_headerDate).text(`${month} ${year}`);
  },

  updateViewDate: function(date) {
    let s = this.data.selectors;
    var dateParse = new DateParse(date);

    $(s.s_dayOfWeek).text(dateParse.getDayOfWeek());
    $(s.s_day).text(dateParse.date.getDate());
  },


  // --- Initializers ---- //
  intializeCalendar: function() {    
    let _this = this;
    let s = this.data.selectors;

    this.data.calendar = new DatePicker(s.s_calendar, {
        container: s.s_calendarWrapper,
        setDefaultDate: true,
        defaultDate: new Date(),
        firstDay: 1,
        animation: false,
        onSelect: (date) => _this.onUpdateDate(new Date(date))
    });
    
    this.data.calendar.runDatePicker();  

    var instance = this.data.calendar.getInstance();
    instance.open();
  },

  renderTable: function() {  
    let time = new Date();
    time.setHours(0);

    let el =  '<div class="cell">' +
                `<input type="hidden" value="{{time}}">`
              '</div>';

    let timeZone = new DateParse(new Date()).getTimeZone();
    this.renderCell(el, time, timeZone);

    let hour = 0;
    let ampm = 'AM';
    for (let i = 0; i < 23; ++i) {
      if (hour > 12) {
        hour = 1;
        ampm = 'PM';
      }

      time.setHours(time.getHours() + 1);
      hour++;

      this.renderCell(el, time, `${hour} ${ampm}`);
    }
  },

  renderCell: function(el, time, dataContent) {
    let s = this.data.selectors;

    let html = el.replace('{{time}}', time.getHours());
    html = $(html).attr('data-content', dataContent);

    $(s.s_table).append(html);
  }
};