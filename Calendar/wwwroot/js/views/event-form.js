import { DatePicker } from '../models/date-picker.js';
import { TimePicker } from '../models/time-picker.js';
import { Dropdown } from '../models/dropdown.js';
import { ViewMode } from './view-mode.js';
import { EventRepository } from '../models/mvc/event-repository.js';
import { Modal } from '../views/pop-ups/modal.js';

export let EventForm = {
  data: {
    form: {
      datePickers: [],
      timePickers: [],
      dropdown: null,
      userCalendars: null,
      state: 'close',
      states: {
        create: 'create',
        edit: 'edit',
        close: 'close'
      }
    },

    selectors: {
      s_formLoad: '#load-create-event-form',
      s_formWrapper: '.create-event-wrapper',
      s_form: '.create-event-form',       
      s_title: '.create-event-form #title',
      s_description: '.create-event-form #description',
      s_timeStart: '#time-start',
      s_timeFinish: '#time-finish',
      s_dateStart: '#date-start',
      s_dateFinish: '#date-finish',
      s_isAllDay: '.create-event-form #all-day',     
      s_userCaledars: '#user-calendars-trigger',
      s_seletedCalendar: '#user-calendars-trigger',
      s_userCalendar: '.user-calendar',
      s_userCalendarItem: '#user-calendar-list .user-calendar',
      s_calendarColor: '.calendar-color',
      s_optionsLoad: '#more-options',
      s_options: '.options',
      s_submitBtn: '#event-submit-btn',
      s_submitCreate: '.btn-submit-create',
      s_submitEdit: '.btn-submit-edit',
      s_closeTrigger: '.create-event-form .close',
      s_optionsTrigger: '.more-options-btn',
      s_dropdownTrigger: '#repeat-dropdown-trigger', 
    },

    css: {
      c_timeStart: 'time-start',
      c_timeFinish: 'time-finish',
      c_submitCreate: 'btn-submit-create',
      c_submitEdit: 'btn-submit-edit',
    },

    url: {
      createEventForm: '/EventView/CreateEventForm',
      editEventForm: '/EventView/EditEventForm'
    },

    cache: {
      formCallback: null
    }
  },

  setUpListeners() {
    let s = this.data.selectors;

    $(s.s_userCalendarItem).click((e) => this.onCalendarChanged(e));
    $(s.s_closeTrigger).click(() => this.onCancelCreation());  
    $(s.s_optionsTrigger).click(() => this.onOptionsOpen());
    $(s.s_submitCreate).click(() => this.onCreate());
    $(s.s_submitEdit).click(() => this.onEdit());
    $(s.s_title).focusout(e => this.onTitleFocusOut(e));
    $(s.s_isAllDay).change(e => this.onAllDayChanged(e));
    $(s.s_timeStart).focusout(e => this.onTimeFocusOut(e));
    $(s.s_timeFinish).focusout(e => this.onTimeFocusOut(e));
  },

  openCreate(start, finish, allDay = false, callback = null) {    
    let s = this.data.selectors;
    let container = s.s_formLoad;
    let url = this.data.url.createEventForm;    

    $.get(url, (content) => {
      if (!this.formCanOpen())
        return;

      $(container).html(content);   
      this.runUserCalendars();
      this.renderDatePickers(start, finish);
      this.renderTimePickers(start, finish);            
      this.formState('create');
      this.setUpListeners();
      this.cacheFromCallback(callback);

      if (allDay) $(s.s_isAllDay).show();
      Modal.open(this.onCancelCreation);
    });

  },

  openEdit(id) {
    if (!this.formCanOpen())
      return;

    let s = this.data.selectors;
    let container = s.s_formLoad;
    let url = this.data.url.editEventForm + `?id=${id}`;

    $.get(url, (content) => {
      $(container).html(content);      
      let start = new Date($(s.s_dateStart).val());
      let finish = new Date($(s.s_dateFinish).val());
      
      this.runUserCalendars();
      this.renderDatePickers(start, finish);
      this.renderTimePickers(start, finish);             
      this.formState('edit');
      this.setUpListeners();

      Modal.open(this.onCancelCreation);

      let title = $(s.s_title).val();
      let color = ViewMode.getCachedColor();

      let rollback = {
        id,
        start,
        finish,
        title,
        color
      };

      ViewMode.cacheRollback(rollback);
    });
  },

  close() {
    let s = this.data.selectors;
    
    let el = s.s_formWrapper;    
            
    $(el).remove();     
    this.formState('close');      
    ViewMode.cacheEvent('');  
    Modal.close();
  },

  onCancelCreation() {  
    let _this = EventForm;
    let selector = ViewMode.getCachedEvent();

    if (_this.getFormState() == _this.data.form.states.create) {
      ViewMode.deleteEvent(selector);      
    } else {
      ViewMode.eventRollback();
    }    
    _this.close();
  },

  async onCreate() {    
    let event = this.getEvent();
    let id = await new EventRepository().insert(event);    
    
    let selector = ViewMode.getCachedEvent();
    $(`#${selector}`).find('input[name="id"]').val(id);

    this.close();
    this.execFormCallback();
  },

  onEdit() {
    let event = this.getEvent();
    new EventRepository().update(event);
    this.close();
  },

  onCalendarChanged(e) {
    let s = this.data.selectors;
    let target = e.currentTarget;
    let selectedCalendar = s.s_seletedCalendar;    

    let name = $(target).find('span')[0].innerText;
    let colorContainer = $(target).find(s.s_calendarColor)[0];
    let color = $(colorContainer).css('background-color');
    let id = $(target).find('input[name="calendarId"]').val();

    $(selectedCalendar).find('span')[0].innerText = name;
    $(selectedCalendar).find('input[name="calendarId"]').val(id);
    let selectedCalendarColor = $(selectedCalendar).find(s.s_calendarColor)[0];
    $(selectedCalendarColor).css('background-color', color);

    ViewMode.changeEventCalendar(+id);
    ViewMode.cacheColor(color);
  },

  onAllDayChanged(e) {
    let s = this.data.selectors;
    let checked = e.currentTarget.checked;    

    let selector = ViewMode.getCachedEvent();    
    let title = $(s.s_title).val();
    let id = $(s.s_form).find('input[name="eventId"]').val();
    let color = ViewMode.getCachedColor();

    $(`#${selector}`).remove();    

    if (checked) {            
      ViewMode.renderAllDayEvent(selector, id, title, color);
    } else {      
      let start = moment($(s.s_timeStart).val(), ['h:m a', 'H:m']).toDate();
      let finish = moment($(s.s_timeFinish).val(), ['h:m a', 'H:m']).toDate();
      
      ViewMode.renderDefaultEvent(selector, id, title, start, finish, color);
    }    
  },

  onOptionsOpen() {              
    let s = this.data.selectors;      
    if ($(s.s_options).hasClass('open'))    
      return;

    this.data.form.dropdown = new Dropdown(s.s_dropdownTrigger, { constrainWidth: false });
    this.data.form.dropdown.runDropdown();

    $('#repeat-dropdown-content a').click(event => 
      this.data.form.dropdown.clickHandler(event));
      
    this.openOptionsAnimation();
  },

  onTitleFocusOut(e) {
    let title = $(e.target).val();
    let id = ViewMode.getCachedEvent();

    ViewMode.setEventTitle(title, id);
  },

  onTimeFocusOut(e) {   
    let event = ViewMode.getCachedEvent();
    if ($(`#${event}`).hasClass('all-day-event'))
      return;
    
    let s = this.data.selectors;

    let timeStart = $(s.s_timeStart).val();
    let timeFinish = $(s.s_timeFinish).val();

    let id = ViewMode.getCachedEvent();    
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

  formState(value) {
    let s = this.data.selectors;
    let c = this.data.css;
    let m = this.data.form.states;

    $(s.s_submitBtn).addClass(
      value === m.create ?
      c.c_submitCreate : c.c_submitEdit
    );

    this.data.form.state = value;
  },

  getFormState() {
    return this.data.form.state;
  },

  getEvent() {
    let s = this.data.selectors;

    let datePickers = this.data.form.datePickers;
    let timePickers = this.data.form.timePickers;

    let id = $(s.s_form).find('input[name="eventId"]').val();
    let calendarId = $(s.s_seletedCalendar).find('input[name="calendarId"]').val();
    let title = $(s.s_title).val();
    let description = $(s.s_description).val();
    let isAllDay = $(s.s_isAllDay).is(":checked");
    let start = datePickers['date-start'].getDate();
    let finish = datePickers['date-finish'].getDate();
    let timeStart = timePickers['time-start'].getDate();
    let timeFinish = timePickers['time-finish'].getDate();

    start.setHours(timeStart.getHours());
    start.setMinutes(timeStart.getMinutes());

    finish.setHours(timeFinish.getHours());
    finish.setMinutes(timeFinish.getMinutes());

    return {
      id,
      calendarId,
      title,
      description,
      start,
      finish,      
      isAllDay,      
    };
  },

  validateTime(start, finish, target) {      
    let startDuration = start.getTime() / 1000;
    let finishDuration = finish.getTime() / 1000;

    if (startDuration < finishDuration) {
      return { start, finish };
    }
    
    if (target === this.data.css.c_timeStart) {
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

  formCanOpen() {
    return this.getFormState() === this.data.form.states.close;
  },

  setStartFinishTime(start, finish) {
    let s = this.data.selectors;   

    $(s.s_timeStart).val(start);
    $(s.s_timeFinish).val(finish);
  },

  runUserCalendars() {
    let s = this.data.selectors;

    this.data.form.userCalendars = new Dropdown(s.s_userCaledars, { constrainWidth: false });
    this.data.form.userCalendars.runDropdown();    
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

  cacheFromCallback(callback) {
    this.data.cache.formCallback = callback;
  },

  execFormCallback() {
    let callback = this.data.cache.formCallback;

    if (callback != null)
      callback();
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

    $content.addClass('open');
  }
};