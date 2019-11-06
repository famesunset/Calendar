import { DateParse } from '../models/share/DateParse.js';
import { DatePicker } from '../models/DatePicker.js';
import { TimePicker } from '../models/TimePicker.js';
import { Dropdown } from '../models/Dropdown.js';
import { Event } from '../models/Event.js';
import { ViewMode } from './ViewMode.js';
import { TimeParse } from '../models/share/TimeParse.js';

var EventForm = {
  data: {
    form: {
      datePickers: [],
      timePickers: [],
      dropdown: null
    },

    selectors: {
      s_formLoad: '#load-create-event-form',
      s_formWrapper: '.create-event-wrapper',
      s_form: '.create-event-form', 
      s_title: '.create-event-form #title',
      s_timeStart: '#time-start',
      s_timeEnd: '#time-finish',
      s_modal: '#event-form-modal',
      s_optionsLoad: '#more-options',
      s_options: '.options',
      s_saveBtn: '#btn-save',
      s_closeTrigger: '.create-event-form .close',
      s_optionsTrigger: '.more-options-btn',
      s_dropdownTrigger: '#repeat-dropdown-trigger', 
    },

    css: {
      s_timeStart: 'time-start',
      s_timeFinish: 'time-finish',
    },

    url: {
      u_formLoad: 'LoadView/CreateEventForm',
      u_optionsLoad: 'LoadView/EventMoreOptions',
      u_createEvent: 'Home/CreateEvent'
    } 
  },

  setUpListeners() {
    let s = this.data.selectors;

    $(s.s_closeTrigger).click(() => this.onCloseForm());     
    $(s.s_modal).click(() => this.onCloseForm());
    $(s.s_optionsTrigger).click(() => this.onLoadOptionsTrigger());
    $(s.s_saveBtn).click(() => this.onSave());
    $(s.s_title).focusout(e => this.onTitleFocusOut(e));
    $(s.s_timeStart).focusout(e => this.onTimeFocusOut(e));
    $(s.s_timeEnd).focusout(e => this.onTimeFocusOut(e));
  },

  open(start, end) {    
    let container = this.data.selectors.s_formLoad;
    let url = this.data.url.u_formLoad;

    $(container).load(url, () => {
      this.renderDatePickers(start, end);
      this.renderTimePickers(start, end);
      this.renderModal();
      this.openAnimation();

      this.setUpListeners();
    });
  },

  close() {
    let s = this.data.selectors;
    
    let el = s.s_formWrapper;
    let modal = s.s_modal;

    this.closeAnimation();
    $(modal).remove();
    $(el).remove();     
  },

  onCloseForm() {
    this.close();
    ViewMode.removeLastEvent();
  },

  onSave() {
    let url = this.data.url.u_createEvent;
    let datePickers = this.data.form.datePickers;
    let timePickers = this.data.form.timePickers;

    let title = $('#title').val();
    let description = $('#description').val();
    let isAllDay = $('#all-day').is(":checked");
    let start = datePickers['date-start'].getDate();
    let finish = datePickers['date-finish'].getDate();
    let timeStart = timePickers['time-start'].getDate();
    let timeFinish = timePickers['time-finish'].getDate();

    start.setHours(timeStart.getHours());
    start.setMinutes(timeStart.getMinutes());

    finish.setHours(timeFinish.getHours());
    finish.setMinutes(timeFinish.getMinutes());
      
    new Event(
    title,
    description,
    start,
    finish,
    null,
    isAllDay,
    null
    ).sendToMVC(url); 
    
    this.close();
  },

  onLoadOptionsTrigger() {    
    // Options are already open
    if (document.getElementById("options") != null) 
      return;     
      
    let container = this.data.selectors.s_optionsLoad;
    let url = this.data.url.u_optionsLoad;

    // Load more options to container
    $(container).load(url, () => {    
      let s = this.data.selectors;          

      this.data.form.dropdown = new Dropdown(s.s_dropdownTrigger, { constrainWidth: false });
      this.data.form.dropdown.runDropdown();

      $('#repeat-dropdown-content a').click(event => 
        this.data.form.dropdown.clickHandler(event));
      
      this.openOptionsAnimation();
    });
  },

  onTitleFocusOut(e) {
    let title = $(e.target).val();
    let id = ViewMode.getLastEventId();

    ViewMode.setEventTitle(title, id);
  },

  onTimeFocusOut(e) {       
    let s = this.data.selectors;

    let _start = $(s.s_timeStart).val();
    let _end = $(s.s_timeEnd).val();

    let id = ViewMode.getLastEventId();    
    let title = $(s.s_title).val();
    
    let time = this.validateTime(
      TimeParse.parse(_start),
      TimeParse.parse(_end),
      e.target.id
    );
  
    ViewMode.changeEventPosition(id, title, time.start, time.end);
  },

  validateTime(start, end, target) {      
    let _start = start.getTime() / 1000;
    let _end = end.getTime() / 1000;

    if (_start < _end) {
      return { start, end };
    }
    
    if (target === this.data.css.s_timeStart) {
      end.setHours(start.getHours() + 1);
    } else {
      start.setHours(end.getHours() - 1);
    }      
    
    this.setStartEndTime(
      new TimeParse(start).getTime(),
      new TimeParse(end).getTime()
    );

    return { start, end };
  },

  setStartEndTime(start, end) {
    let s = this.data.selectors;   

    $(s.s_timeStart).val(start);
    $(s.s_timeEnd).val(end);
  },

  renderModal() {
    let s = this.data.selectors;
    if ($(s.s_modal)[0] != undefined)
      return;

    let modal = `<div id="event-form-modal"></div>`;
    $('body').prepend(modal);
  },

  renderDatePickers(dateStart, dateEnd) {
    this.data.form.datePickers = {
      "date-start": new DatePicker('#date-start', {
        setDefaultDate: true, 
        defaultDate: dateStart,
        firstDay: 1
      }),

      "date-finish": new DatePicker('#date-finish', {
        setDefaultDate: true,
        defaultDate: dateEnd,
        firstDay: 1
      })
    };

    for (let key in this.data.form.datePickers) {
      this.data.form.datePickers[key].runDatePicker();
    }
  },

  renderTimePickers(dateStart, dateEnd) {  
    const HOUR = 1000 * 60 * 60; // hour in ms

    let timeStart = `${dateStart.getHours()}:${dateStart.getMinutes()}`;
    let timeEnd = `${dateEnd.getHours()}:${dateEnd.getMinutes()}`;

    this.data.form.timePickers = {
      'time-start': new TimePicker(dateStart, {            
        defaultTime: timeStart,
      }, '#time-start'),

      'time-finish': new TimePicker(dateEnd, {
        defaultTime: timeEnd,
        fromNow: HOUR
      }, '#time-finish')
    };

    for (let key in this.data.form.timePickers) {
      this.data.form.timePickers[key].setDefaultInputValue();
      this.data.form.timePickers[key].runTimePicker();
    }
  },

  openAnimation() {
    var s = this.data.selectors;
    var $content = $(s.s_form);
        
    $content.css('display', 'block');
    $content.animate({
        opacity: 1
    }, 50);
  },

  closeAnimation() {
    var s = this.data.selectors;
    var $content = $(s.s_form);

    $content.animate({
      opacity: 0
    }, 50, () => $content.css('display', 'none'));
  },

  openOptionsAnimation() {
    var $wrapper = $(this.data.selectors.s_optionsLoad);
    var $content = $(this.data.selectors.s_options);

    $wrapper.animate({
      height: `+=${$content.height()}`
    }, 100, () => {
      $content.css('display', 'block');
      $content.animate({
        opacity: 1
      }, 150);
    });
  }
};

export { EventForm };