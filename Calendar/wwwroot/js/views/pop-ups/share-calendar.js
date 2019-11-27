import { Modal } from "./modal.js";

export let ShareCalendar = {
  data: {
    cache: {
      calendar: null
    },

    selectors: {
      s_calendarList: '#calendar-list',
      s_calendarForm: '.share-calendar-form',
      s_shareCalendarEmail: '.share-calendar-form input[name="userEmail"]',
      s_shareCalendarId: '.share-calendar-form input[name="calendarId"]',
      s_shareCalendarSubmit: '#share-calendar-submit'
    },

    url: {
      s_loadForm: '/CalendarView/ShareCalendarForm'
    }
  },

  open(id, point, root) {
    let s = this.data.selectors;
    let u = this.data.url;

    let url = u.s_loadForm + `?id=${id}`;
    let container = s.s_calendarList;

    $.get(url, (content) => {      
      $(container).prepend(content);
      this.openState(point, root);
      Modal.open(this.close);
    });

    this.data.cache.calendar = root;
  },

  close() {
    let _this = ShareCalendar;
    let calendar = _this.data.cache.calendar;
    let form = _this.data.selectors.s_calendarForm;
    $(form).remove();
    $(calendar).css('background-color', 'transparent');
  },

  setUpListeners() {
    let s = this.data.selectors;

    $(s.s_shareCalendarSubmit).click(() => this.onShareCalendar())
  },

  onShareCalendar() {

  },

  openState(point, root) {
    let s = this.data.selectors;
    let form = $(s.s_calendarForm);
    form.css({ 'left': point.x + 'px', 'top': (point.y - 35) + 'px' });
    $(root).css('background-color', '#F0F0F0');
  }
}