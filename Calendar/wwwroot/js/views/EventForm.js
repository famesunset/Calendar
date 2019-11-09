import { DatePicker } from '../models/DatePicker.js';
import { TimePicker } from '../models/TimePicker.js';
import { Dropdown } from '../models/Dropdown.js';
import { Event } from '../models/Event.js';
import { ViewMode } from './ViewMode.js';

var EventForm = {
  data: {
    targetEvent: new Event(),

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
      s_timeFinish: '#time-finish',
      s_dateStart: '#date-start',
      s_dateFinish: '#date-finish',
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
    $(s.s_timeFinish).focusout(e => this.onTimeFocusOut(e));
  },

  open(start, finish) {    
    let container = this.data.selectors.s_formLoad;
    let url = this.data.url.u_formLoad;

    this.initTargetEvent(new Event(
      "(No title)",
      "",
      start,
      finish, 
      "#9E69AF",
      false,
      null
    ));

    $(container).load(url, () => {
      this.renderDatePickers(start, finish);
      this.renderTimePickers(start, finish);
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

  initTargetEvent(event) {
    let data = event.data;

    this.data.targetEvent.data = { ...data };
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

    let timeStart = $(s.s_timeStart).val();
    let timeFinish = $(s.s_timeFinish).val();

    let id = ViewMode.getLastEventId();    
    let title = $(s.s_title).val();
    
    let time = this.validateTime(      
      moment(timeStart, ['h:m a', 'H:m']).toDate(),
      moment(timeFinish, ['h:m a', 'H:m']).toDate(),
      e.target.id
    );
  
    ViewMode.changeEventPosition(id, title, time.start, time.finish);
  },

  onDateSelect(date) {
    this.validateDate(
      this.data.form.datePickers['date-start'].getDate(),
      this.data.form.datePickers['date-finish'].getDate()      
    )
  },

  validateTime(start, finish, target) {      
    let startDuration = start.getTime() / 1000;
    let finishDuration = finish.getTime() / 1000;

    if (startDuration < finishDuration) {
      return { start, finish };
    }
    
    if (target === this.data.css.s_timeStart) {
      finish = moment(start).add(1, 'hours').toDate();
    } else {
      start = moment(finish).add(-1, 'hours').toDate();
    }       
    
    this.setStartFinishTime(
      moment(start).format('LT'),
      moment(finish).format('LT')
    );

    return { start, finish };
  },

  validateDate(start, finish) {
    let s = this.data.selectors;
    let m_start = moment(start);
    let m_finish = moment(finish);

    let min = moment.min(m_start, m_finish);

    if (min != m_start) {
      let m_dateFinish = moment(start).add(1, 'days');      

      this.data.form.datePickers['date-finish']
        .setDate(m_dateFinish.toDate());
      
      $(s.s_dateFinish).val(m_dateFinish.format('MMM DD, YYYY'));
    }
  },

  setStartFinishTime(start, finish) {
    let s = this.data.selectors;   

    $(s.s_timeStart).val(start);
    $(s.s_timeFinish).val(finish);
  },

  renderModal() {
    let s = this.data.selectors;
    if ($(s.s_modal)[0] != undefined)
      return;

    let modal = `<div id="event-form-modal"></div>`;
    $('body').prepend(modal);
  },

  renderDatePickers(dateStart, dateFinish) {
    let s = this.data.selectors;
    let _this = this;

    this.data.form.datePickers = {
      "date-start": new DatePicker({
        setDefaultDate: true, 
        defaultDate: dateStart,
        firstDay: 1,
        onSelect: (date) => _this.onDateSelect(date)
      }, s.s_dateStart),

      "date-finish": new DatePicker({
        setDefaultDate: true,
        defaultDate: dateFinish,
        firstDay: 1,
        onSelect: (date) => _this.onDateSelect(date)
      }, s.s_dateFinish)
    };

    for (let key in this.data.form.datePickers) {
      this.data.form.datePickers[key].runDatePicker();
    }
  },

  renderTimePickers(dateStart, dateFinish) {  
    const HOUR = 1000 * 60 * 60; // hour in ms
    let s = this.data.selectors;

    let timeStart = moment(dateStart).format('h:mm');
    let timeFinish = moment(dateStart).format('h:mm');

    this.data.form.timePickers = {
      'time-start': new TimePicker(dateStart, {            
        defaultTime: timeStart,
      }, s.s_timeStart),

      'time-finish': new TimePicker(dateFinish, {
        defaultTime: timeFinish,
        fromNow: HOUR
      }, s.s_timeFinish)
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