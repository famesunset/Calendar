import { DatePicker } from '../models/date-picker.js';
import { TimePicker } from '../models/time-picker.js';
import { Dropdown } from '../models/dropdown.js';
import { ViewMode } from './view-mode.js';
import { EventRepository } from '../models/mvc/event-repository.js';
import { Modal } from '../views/pop-ups/modal.js';
import { GUID } from '../models/share/GUID.js'
import { FetchContent } from '../models/mvc/fetch-content.js';
import { CalendarList } from './calendar-list.js';

export let EventForm = {
  data: {
    form: {
      datePickers: [],
      timePickers: [],
      dropdown: null,
      userCalendars: null,
      state: 'close',
      states: {
        share: 'share',
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
      s_repeatInterval: '#repeat-dropdown-trigger',
      s_repeatIntervalItem: '#repeat-dropdown-content li',
      s_userCaledars: '#user-calendars-trigger',
      s_seletedCalendar: '#user-calendars-trigger',
      s_userCalendar: '.user-calendar',
      s_userCalendarItem: '#user-calendar-list .user-calendar',
      s_calendarColor: '.calendar-color',
      s_notifyTimeValue: '.notify input[name="timeValue"]',
      s_notifyTimeUnitMenu: '#notify-time-unit',     
      s_notifyTimeUnitText: '#notify-time-unit span' ,
      s_notifyTimeUnitValue: '#notify-time-unit input[name="timeUnit"]',
      s_notifyTimeUnitListItem: "#time-unit-list li",
      s_notifyToggle: '.notify-toggle',
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
      editEventForm: '/EventView/EditEventForm',
      sharedEventForm: '/EventView/OpenSharedEventForm'
    },

    cache: {
      formCallback: null,
      eventId: null,
      eventColor: null,
      lastCalendar: null
    }
  },

  setUpListeners() {
    let s = this.data.selectors;

    $(s.s_userCalendarItem).click((e) => this.onCalendarChanged(e));
    $(s.s_closeTrigger).click(() => this.onCancelCreation());  
    $(s.s_optionsTrigger).click(() => this.onOptionsOpen(100));
    $(s.s_notifyToggle).click(e => this.onNotifyMenu(e));
    $(s.s_notifyTimeUnitListItem).click(e => this.onChangeNotifyTimeUnit(e));
    $(s.s_repeatIntervalItem).click(e => this.onRepeatIntervalChanged(e));
    $(s.s_submitCreate).click(() => this.onCreate());
    $(s.s_submitEdit).click(() => this.onEdit());
    $(s.s_title).focusout(e => this.onTitleFocusOut(e));
    $(s.s_isAllDay).change(e => this.onAllDayChanged(e));
    $(s.s_timeStart).on('input', e => this.onTimeChanged(e));
    $(s.s_timeFinish).on('input', e => this.onTimeChanged(e));
    $(s.s_timeStart).change(e => this.onTimeChanged(e));
    $(s.s_timeFinish).change(e => this.onTimeChanged(e));
  },

  openCreate(start, finish, allDay = false, callback = null) {        
    let event = {
      start,
      finish,
      allDay
    }

    let date = new Date(sessionStorage.getItem('currentDate'));    
    let url = this.data.url.createEventForm + `?date=${date.toISOString()}`;    

    FetchContent.get(url, 
      content => this.renderCreateForm(content, event, callback));
  },

  openShared(event) {
    if (!this.formCanOpen())
      return;

    let url = this.data.url.sharedEventForm;
    let _data = JSON.stringify(event);

    FetchContent.post(_data, url, 
      content => this.renderSharedForm(content, event)); 
  },

  openEdit(id) {
    if (isNaN(id))
      return;  

    let url = this.data.url.editEventForm + `?id=${id}&timeOffset=${new Date().getTimezoneOffset()}`;
    FetchContent.get(url, 
      content => this.renderEditForm(content, id));
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
    
    let state = _this.getFormState();
    let states = _this.data.form.states;

    if (state == states.create) {
      ViewMode.deleteEvent(selector);      
    } else if (state == states.edit) {
      ViewMode.eventRollback();
    }    
    _this.close();
  },

  onCreate() {    
    let event = this.getEvent();
    
    new EventRepository()
    .insert(event, 
    id => {
      if (id != -1) {
        let selector = ViewMode.getCachedEvent();
        $(`#${selector}`).find('input[name="id"]').val(id);
    
        this.close();
        this.execFormCallback();    
        M.toast({html: 'Event added'});
      }
    });    
  },

  onEdit() {
    let event = this.getEvent();
    
    new EventRepository().update(event, 
    () => {
      M.toast({html: 'Event changed'});
      this.close();
    });    
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
      
      ViewMode.renderEvent(selector, id, title, start, finish, color);
    }    
  },

  onOptionsOpen(duration = 100) {              
    let s = this.data.selectors;      
    if ($(s.s_options).hasClass('open'))    
      return;

    this.data.form.dropdown = new Dropdown(s.s_dropdownTrigger, { constrainWidth: false });    
    this.data.form.dropdown.runDropdown();

    let timeUnitDropdown = new Dropdown(s.s_notifyTimeUnitMenu, { constrainWidth: false });
    timeUnitDropdown.runDropdown();
      
    this.openOptionsAnimation(duration);
  },

  onRepeatIntervalChanged(e) {
    let target = e.currentTarget;    
    let repeatInterval = this.data.selectors.s_repeatInterval;
    let text = $(target).find('a').text();
    let interval = $(target).find('input[name="interval"]').val();

    $(repeatInterval).find('span').text(text);
    $(repeatInterval).find('input[name="interval"]').val(interval);    
  },

  onNotifyMenu(e) {     
    let target = e.currentTarget;   
    let notifyMenu = '#' + $(target).attr('data-target');
    let $menu = $(notifyMenu);
    
    if ($menu.hasClass('state-close')) {
      this.notifyOpen(target, notifyMenu);
    } else {
      this.notifyClose(target, notifyMenu);
    }
  },

  renderCreateForm(content, event, callback) {
    if (!this.formCanOpen())
      return;

    let s = this.data.selectors;
    let container = s.s_formLoad;

    $(container).html(content);   
    this.runUserCalendars();      
    this.renderDatePickers(event.start, event.finish);
    this.renderTimePickers(event.start, event.finish);            
    this.formState('create');
    this.setUpListeners();
    this.cacheFromCallback(callback);
    
    Modal.open(this.onCancelCreation);
  },

  renderSharedForm(content, event) {
    if (!this.formCanOpen())
      return;

    let s = this.data.selectors;
    let container = s.s_formLoad;

    $(container).html(content);      
    let selector = GUID();        
    let start = new Date(event.Start);
    let finish = new Date(event.Finish);
    
    this.runUserCalendars();
    this.renderDatePickers(start, finish);
    this.renderTimePickers(start, finish);                  
    this.formState('create');
    this.setUpListeners();
    this.onOptionsOpen(0);

    let color = CalendarList.getDefaultCalendarColor();
    if (event.IsAllDay) {
      ViewMode.renderAllDayEvent(selector, 0, event.Title, color);
    } else {
      ViewMode.renderEvent(selector, 0, event.Title, start, finish, color);
    }

    Modal.open(this.onCancelCreation);
  },  

  renderEditForm(content, id) {
    if (!this.formCanOpen())
      return;

    let s = this.data.selectors;
    let container = s.s_formLoad;

    $(container).html(content);      
    let start = new Date($(s.s_dateStart).val());
    let finish = new Date($(s.s_dateFinish).val());
    
    this.runUserCalendars();
    this.renderDatePickers(start, finish);
    this.renderTimePickers(start, finish);             
    this.formState('edit');
    this.setUpListeners();
    this.onOptionsOpen(0);

    Modal.open(this.onCancelCreation);

    let title = $(s.s_title).val();
    let isAllDay = $(s.s_isAllDay).is(":checked");
    let color = ViewMode.getCachedColor();

    let rollback = {
      id,
      start,
      finish,
      title,
      isAllDay,
      color
    };

    ViewMode.cacheRollback(rollback);
  },

  notifyOpen(target, menu) {
    let s = this.data.selectors;
    let $target = $(target);    
    let $menu = $(menu);

    $menu.removeClass('state-close');
    $target.removeClass('notify-toggle-add');            
    $target.addClass('notify-toggle-close');      
    $(s.s_notifyTimeUnitValue).val('Min');
    $(s.s_notifyTimeUnitText).text('Min');
  },

  notifyClose(target, menu) {
    let s = this.data.selectors;
    let $target = $(target);    
    let $menu = $(menu);

    $menu.addClass('state-close');      
    $target.removeClass('notify-toggle-close');            
    $target.addClass('notify-toggle-add'); 
    $(s.s_notifyTimeUnitValue).val('NoNotify');
    $(s.s_notifyTimeUnitText).text('Min');
  },

  onChangeNotifyTimeUnit(e) {
    let s = this.data.selectors;
    let $target = $(e.currentTarget);

    let text = $target.find('a').text();
    let value = $target.find('input[name="timeUnit"]').val();

    $(s.s_notifyTimeUnitText).text(text);
    $(s.s_notifyTimeUnitValue).val(value);
  },

  onTitleFocusOut(e) {
    let title = $(e.target).val();
    let id = ViewMode.getCachedEvent();

    ViewMode.setEventTitle(title, id);
  },

  onTimeChanged(e) {       
    let event = '#' + ViewMode.getCachedEvent();
    if ($(event)[0] == undefined ||
        $(event).hasClass('all-day-event'))
      return;
    
    let s = this.data.selectors;

    let timeStart = $(s.s_timeStart).val();
    let timeFinish = $(s.s_timeFinish).val();
    
    let time = this.validateTime(      
      moment(timeStart, ['h:m a', 'H:m']).toDate(),
      moment(timeFinish, ['h:m a', 'H:m']).toDate(),
      e.target.id
    );
    
    if (time != null) {
      let id = ViewMode.getCachedEvent();    
      let title = $(s.s_title).val();
    
      ViewMode.changeEventPosition(id, title, time.start, time.finish);
    }
  },

  onDateChanged(date) {
    let current = new Date(sessionStorage.getItem('currentDate'));
    let start = this.data.form.datePickers['date-start'].getDate();
    let finish = this.data.form.datePickers['date-finish'].getDate();

    this.validateDate(start, finish);
    
    let m_current = moment(current);
    let validRange = m_current.isSameOrAfter(start) && m_current.isSameOrBefore(finish);    

    if (!validRange) {
      this.inValidDateRange();
    } else {
      this.validDateRange();
    }
  },

  inValidDateRange() {
    let el = '#' + ViewMode.getCachedEvent();   

    if ($(el)[0] != undefined) {
      let id = $(el).find('input[name="id"]').val();      
      this.data.cache.eventId = id;

      $(el).remove();    
    }      
  },

  validDateRange() {    
    let el = '#' + ViewMode.getCachedEvent();    
    if ($(el)[0] != undefined)
      return;

    let event = this.getEvent().event;
    let color = ViewMode.getCachedColor();
    let eventSelector = ViewMode.getCachedEvent();

    if (event.isAllDay)  {
      ViewMode.renderAllDayEvent(
        eventSelector,
        event.id,
        event.title,
        color
      );
    } else {
      ViewMode.renderEvent(
        eventSelector,
        event.id,
        event.title,
        event.start,
        event.finish,
        color
      );
    }     
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
    let currentDate = new Date(sessionStorage.getItem('currentDate'));

    let datePickers = this.data.form.datePickers;
    let timePickers = this.data.form.timePickers;

    let id = $(s.s_form).find('input[name="eventId"]').val();
    let calendarId = $(s.s_seletedCalendar).find('input[name="calendarId"]').val();
    let title = $(s.s_title).val();
    let description = $(s.s_description).val();
    let isAllDay = $(s.s_isAllDay).is(":checked");

    let notify = {
      timeUnit: $(s.s_notifyTimeUnitValue).val(),
      value: $(s.s_notifyTimeValue).val(),
    }
    let repeat = $(s.s_repeatInterval).find('input[name="interval"]').val();
    let start = datePickers['date-start'].getDate();
    let finish = datePickers['date-finish'].getDate();

    let offset = new Date().getTimezoneOffset();
    let timeStart = timePickers['time-start'].getDate();
    let timeFinish = timePickers['time-finish'].getDate();

    start.setHours(timeStart.getHours());
    start.setMinutes(timeStart.getMinutes());

    finish.setHours(timeFinish.getHours());
    finish.setMinutes(timeFinish.getMinutes());

    return {
      event: {
        id,
        calendarId,
        title,
        description,
        start: moment(start).toISOString(true),
        finish: moment(finish).toISOString(true),
        isAllDay,   
        repeat,
        notify
      },
      offset
    };
  },

  validateTime(start, finish, target) {
    if (!moment(start).isValid() ||
        !moment(finish).isValid()) return null;
        
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
        onSelect: (date) => _this.onDateChanged(date)
      }, s.s_dateStart),

      "date-finish": new DatePicker({
        setDefaultDate: true,
        defaultDate: dateFinish,
        firstDay: 1,
        onSelect: (date) => _this.onDateChanged(date)
      }, s.s_dateFinish)
    };

    for (let key in this.data.form.datePickers) {
      this.data.form.datePickers[key].runDatePicker();
    }
  },

  renderTimePickers(dateStart, dateFinish) {  
    const HOUR = 1000 * 60 * 60; // hour in ms
    let _this = this;
    let s = this.data.selectors;
    let c = this.data.css;

    let timeStart = moment(dateStart).format('h:mm');
    let timeFinish = moment(dateStart).format('h:mm');

    this.data.form.timePickers = {
      'time-start': new TimePicker(dateStart, {            
        defaultTime: timeStart,        
      }, s.s_timeStart),

      'time-finish': new TimePicker(dateFinish, {
        defaultTime: timeFinish,
        fromNow: HOUR,        
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

  openOptionsAnimation(duration) {
    let s = this.data.selectors;

    var $trigger = $(s.s_optionsTrigger);
    var $wrapper = $(s.s_optionsLoad);
    var $content = $(s.s_options);

    $wrapper.animate({
      height: `+=${$content.height()}`
    }, duration, () => {
      $content.css('display', 'block');
      $content.animate({
        opacity: 1
      }, duration);
    });

    $content.addClass('open');
    $trigger.addClass('hide');
  }
};