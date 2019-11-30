import { Modal } from "./modal.js";
import { UserRepository } from '../../models/mvc/user-repository.js';
import { PopUp } from "./pop-up.js";
import { CalendarRepository } from "../../models/mvc/calendar-repository.js";
import { FetchContent } from "../../models/mvc/fetch-content.js";

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
      s_shareCalendarSubmit: '#share-calendar-submit',      
      s_userId: '.confirm-wrapper input[name="userId"]',
      s_calendarId: '.confirm-wrapper input[name="calendarId"]',
    },

    url: {
      s_loadForm: '/CalendarView/ShareCalendarForm',
      s_loadConfirm: '/PopUp/ShareCalendarConfirm'
    }
  },

  setUpListeners() {
    let s = this.data.selectors;

    $(s.s_shareCalendarSubmit).click(() => this.onShareCalendar())
  },

  openForm(id, point, root) {
    let s = this.data.selectors;
    let u = this.data.url;

    let url = u.s_loadForm + `?id=${id}`;
    let container = s.s_calendarList;

    FetchContent.get(url, content => {
      $(container).prepend(content);      
      Modal.open(this.closeForm);
      this.openState(point, root);
      this.setUpListeners();      
      this.data.cache.calendar = root;
    });
  },

  openConfirm(email, calendarId) {
    let url = this.data.url.s_loadConfirm + `?email=${email}&calendarId=${calendarId}`;
    
    FetchContent.get(url, content => {
      PopUp.open(content, result => {
        let response = PopUp.data.response;

        if (result == response.SUBMIT) {
          new CalendarRepository().subscribe(email, calendarId);
        }
      });
    });
  },

  closeForm() {
    let _this = ShareCalendar;
    let calendar = _this.data.cache.calendar;
    let form = _this.data.selectors.s_calendarForm;
    $(form).remove();
    $(calendar).css('background-color', 'transparent');
    Modal.close();
  },

  async onShareCalendar() {    
    let s = this.data.selectors;
    let email = $(s.s_shareCalendarEmail).val();
    let calendarId = $(s.s_shareCalendarId).val();

    let isValid = await new UserRepository().validEmail(email);

    if (isValid) {
      this.closeForm();
      this.openConfirm(email, calendarId);
    }
  },

  openState(point, root) {
    let s = this.data.selectors;
    let form = $(s.s_calendarForm);
    form.css({ 'left': point.x + 'px', 'top': (point.y - 35) + 'px' });
    $(root).css('background-color', '#F0F0F0');
  }
}