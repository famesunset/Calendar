import { EventForm } from '../event-form.js';
import { GUID } from '../../models/share/GUID.js';
import { EventRepository } from '../../models/mvc/event-repository.js';
import { DeleteEvent } from '../pop-ups/delete-event.js';
import { CalendarRepository } from '../../models/mvc/calendar-repository.js';
import { CalendarList } from '../calendar-list.js';
import { EventInfo } from '../pop-ups/event-info.js';

export let Daily = {
  data: {
    cache: {
      cachedEvent: '',
      cachedColor: '',      
      c_targetCellShift: {},
      timeStart: {},
      timeFinish: {},
      state: '',
      rollback: {
        id: 0,
        color: null,
        title: null,
        start: null,
        finish: null,
        isAllDay: null
      } 
    },

    selectors: {
      s_table: '#table-events',
      s_allDayEvents: '#all-day-events',
      s_dayOfWeek: '.date .day-of-week',
      s_dailyEvent: '.daily-event',
      s_allDayEvent: '.all-day-event',
      s_createAllDayTarget: '#day-view-mode .date',
      s_loadDeleteEvent: '.load-delete-event',
      s_eventContentWrapper: '.event-content-wrapper',
      s_eventWrapperTiny: '.daily-event-tiny',
      s_day: '.date .day span', 
      s_cell: '.cell'     
    },

    css: {
      s_eventContentWrapper: 'event-content-wrapper',
      s_eventWrapperTiny: 'daily-event-tiny',
      c_loadDeleteEvent: 'load-delete-event',
      c_currentDate: 'current-date',
      c_currentDateDayOfWeek: 'current-date-day-of-week',
      с_eventDrag: 'event-drag'
    },

    ux: {
      cellHeight: 50,
      eventHeight: 46,
      pos_mouseStart: 0,
      animateDuration: 150,
      eventDraggableStep: 15,
      leftMouseBtn: 1,
      rightMouseBtn: 3
    }
  },

  run() {    
    let calendars = CalendarList.getSelectedCalendars();    
    let date = new Date(sessionStorage.getItem('currentDate'));

    new EventRepository()
    .getList(moment(date).startOf('day').toDate(), calendars,
    events => {
      this.renderDate(date);
      this.renderTable();
      this.renderEvents(events);
      this.setUpListeners();  
      this.currentTimeListener();
    });
  },

  setUpListeners() {
    let s = this.data.selectors;    

    $(s.s_cell).mousedown(e => {
      this.onCellMouseDown(e);        
      $(document).mousemove(async (e) => this.onStretchEvent(e));      
      $(document).mouseup((e) => this.onOpenCreateForm(e));      
    });        
  },  

  clear() {
    let s = this.data.selectors;

    $(s.s_dailyEvent).remove();
    $(s.s_allDayEvent).remove();
  },  

  createDefaultEvent(container) {    
    let selector = GUID();
    let id = 0;

    let hour = $(container).find('input').val();
    let start = new Date(sessionStorage.getItem('currentDate'));
        start.setHours(hour, 0);
          
    let finish = new Date(start);
        finish.setHours(start.getHours() + 1);

    EventForm.openCreate(start, finish);       

    let color = CalendarList.getDefaultCalendarColor();
    this.renderEvent(container, selector, id, '(No title)', start, finish, color);
    this.cacheEvent(selector);
    this.cacheColor(color);    
  },

  onShowEventInfo(e) {  
    e.stopPropagation();
    if (e.which != this.data.ux.leftMouseBtn)
      return;
          
    let target = e.currentTarget;
    let id = $(target).find('input[name="id"]').val();
    
    if (id != 0 && EventForm.formCanOpen()) {
      let color = $(target).css('background-color');
      EventInfo.open(id);
      this.cacheEvent(target.id);      
      this.cacheColor(color);
    }        
  },

  onDeleteEvent(e) {    
    e.stopPropagation();
    if (e.which != this.data.ux.rightMouseBtn)
      return;
    
    let $target = $(e.currentTarget);
    let eventId = $target.find('input[name="id"]').val();    
    let pos = { x: e.pageX, y: e.clientY };

    this.cacheEvent(e.currentTarget.id);    
    DeleteEvent.open(eventId, pos);
  },

  // Stretch mouse down
  onCellMouseDown(e) {       
    if (e.which != this.data.ux.leftMouseBtn) 
      return;    
    $(this.data.selectors.s_cell).unbind('mousedown');    

    let container = e.target;
    let selector = GUID();
    let eventId = GUID();        

    let hour = $(container).find('input').val();

    let timeStart = moment(new Date(sessionStorage.getItem('currentDate')))
                    .hour(+hour)
                    .startOf('hour')
                    .toDate();
          
    let timeFinish = moment(timeStart).add(1, 'hours').toDate();

    let color = CalendarList.getCachedCalendarColor();
    this.renderEvent(container, selector, eventId, '(No title)', timeStart, timeFinish, color);

    let targetCoords = this.getCoords(container);
    this.data.cache.c_targetCellShift = Math.abs(e.clientY - targetCoords.y);
    this.data.ux.pos_mouseStart = e.clientY;
    this.data.cache.timeStart = timeStart;
    this.data.cache.timeFinish = timeFinish;
    this.data.cache.state = 'create';
    this.data.cache.cachedColor = color;
    $(this.data.selectors.s_table).addClass(this.data.css.с_eventDrag);    
  },

  // Stretch mouse up
  onOpenCreateForm(event, allDay = false) {  
    if (event.which != this.data.ux.leftMouseBtn) 
      return;

    let s = this.data.selectors;
    let target = this.getCachedEvent();;

    EventForm.openCreate(      
      this.data.cache.timeStart,
      this.data.cache.timeFinish,
      allDay,
      () => this.setUpListeners(),
      () => $(target).mouseup((e) => this.onShowEventInfo(e))
    );
        
    $(document).unbind('mouseup');
    $(document).unbind('mousemove');    
    $(s.s_table).removeClass(this.data.css.с_eventDrag);    

    this.data.cache.state = '';
  },

  onStretchEvent(e) {
    if (e.which != this.data.ux.leftMouseBtn) 
      return;

    let cache = this.data.cache;
    
    let mouseStart = this.data.ux.pos_mouseStart;
    let mouseEnd = e.clientY;
    let mouseOffset = mouseEnd - mouseStart + cache.c_targetCellShift;

    if (mouseOffset >= 25) {
      let step = this.data.ux.eventDraggableStep;
      let minutesOffset = Math.trunc((mouseOffset * 60) / this.data.ux.cellHeight);                  
           
      let timeStart = cache.timeStart;
      let timeFinish = new Date(timeStart);        
          timeFinish.setMinutes(this.roundUpToAny(minutesOffset, step));

      this.setEventTime(
        moment(timeStart).format('LT'),
        moment(timeFinish).format('LT'),
        cache.cachedEvent
      );

      this.calcEventPosition(cache.cachedEvent, timeStart, timeFinish);

      this.data.cache.timeStart = timeStart;
      this.data.cache.timeFinish = timeFinish;      
    }
  },

  roundUpToAny(n, x) {
      return Math.floor((n + x / 2) / x) * x;
  },

  getCoords(elem) {
    var box = elem.getBoundingClientRect();
    
    return {
      x: box.x,
      y: box.y     
    };
  },

  findCellByTime(date) {
    return $(`#cell-${date.getHours()}`);
  },

  getDefaultCell() {
    let date = new Date();

    return $(`#cell-${date.getHours()}`);
  },

  renderTable() {  
    let el =  `<div class="cell" id="{{cell-id}}">
                <input type="hidden" value="{{time}}">
              </div>`;

    let timeIterator = 0
    let hour = 0;
    let ampm = 'AM';    

    for (let i = 0; i < 24; ++i) {            
      ampm = hour < 12 ? 'AM' : 'PM';
      hour = hour > 12 ? hour - 12 : hour;      

      let dataContent = `${hour} ${ampm}`;
      let dataTime = dataContent;
      this.renderCell(el, timeIterator, dataContent, dataTime);
          
      hour = ++timeIterator;
    }
  },

  renderCell(el, hour, dataContent, dataTime) {
    let s = this.data.selectors;    
    
    let html = el.replace('{{time}}', hour);
    html = html.replace('{{cell-id}}', `cell-${hour}`);    
    html = $(html).attr('data-time', dataTime);
    if (hour != 0) {
      html = $(html).attr('data-content', dataContent);
    }    

    $(s.s_table).append(html);
  },

  renderDate(date) {
    let s = this.data.selectors;  
    let cssCurrentDate = this.data.css.c_currentDate;  
    let cssCurrentDateDayOfWeek = this.data.css.c_currentDateDayOfWeek;
    let m_date = moment(date);

    $(s.s_dayOfWeek).text(m_date.format('ddd'));    

    let $day = $(s.s_day);
    let dayWrapper = $day[0].parentElement;

    if (moment().isSame(m_date, 'day')) {
      $(dayWrapper).addClass(cssCurrentDate);
      $(s.s_dayOfWeek).addClass(cssCurrentDateDayOfWeek);
    }
    else  {
      $(s.s_dayOfWeek).removeClass(cssCurrentDateDayOfWeek);
      $(dayWrapper).removeClass(cssCurrentDate);
    }      

    $day.text(m_date.format('D'));    
  },

  renderEvents(events) {           
    events.forEach(event => {   
      if (this.eventExists(event.id))
        return;      

      let start = new Date(event.start);
      let finish = new Date(event.finish);
      let selector = GUID();     
      let container = $(`#cell-${start.getHours()}`);

      if (event.isAllDay) {
        this.renderAllDayEvent(selector, event.id, event.title, event.color);
      } else {
        this.renderEvent(container, selector, event.id, event.title, start, finish, event.color);
      }      
    });
  },

  renderEvent(container, selector, id, title, start, finish, color) {
    if (!title || title.trim() === '') {
      title = '(No title)';
    }
    
    var el = 
    `<div class="daily-event" id="${selector}">
      <div class="${this.data.css.s_eventContentWrapper}">
        <input type="hidden" name="id" value="${id}">
        <h6 class="title">${title}</h4>
        <div class="time">
          <span class="start">${moment(start).format('LT')}</span>
          <span> &mdash; </span>
          <span class="end">${moment(finish).format('LT')}</span>
        </div>
      </div>
      <div class="${this.data.css.c_loadDeleteEvent}"></div>
    </div>`;      
    
    $(container).append(el);

    this.calcEventPosition(selector, start, finish);   
    this.cacheEvent(selector); 
    $(`#${selector}`).css('background-color', color);
    $(`#${selector}`).mousedown(e => this.onDeleteEvent(e));
    $(`#${selector}`).mouseup(e => {
      if (this.data.cache.state == 'create')
        this.onOpenCreateForm(e);
      else 
        this.onShowEventInfo(e);
    });
  },

  renderAllDayEvent(selector, id, title, color) {
    let s = this.data.selectors;    
    
    if (!title || title.trim() === '') {
      title = '(No title)';
    }

    let el = 
    `<div class="all-day-event" id="${selector}">
      <input type="hidden" name="id" value="${id}">
      <span class="title">${title}</span>
      <div class="${this.data.css.c_loadDeleteEvent}"></div>
    </div>`;
    
    $(s.s_allDayEvents).append(el);

    this.cacheEvent(selector); 
    $(`#${selector}`).css('background-color', color);
    $(`#${selector}`).mousedown(e => this.onDeleteEvent(e));
    $(`#${selector}`).mouseup(e => this.onShowEventInfo(e));
  },

  hideEventsByCalendarId(calendarId, callback) {
    let date = new Date(sessionStorage.getItem('currentDate'));
    new EventRepository()
    .getList(date, [calendarId],

    events => {
      events.forEach(event => {                    
        let root = this.findRootByEventId(event.id);
        if (root != null) $(root).remove();      
      });

      callback();
    });      
  },

  showEventsByCalendarId(calendarId) {    
    let date = new Date(sessionStorage.getItem('currentDate'));
    
    new EventRepository()
    .getList(date, [calendarId],
    events => this.renderEvents(events));         
  },

  findRootByEventId(id) {
    let daily = this.data.selectors.s_dailyEvent;
    let allDay = this.data.selectors.s_allDayEvent;

    let events = [...$(daily), ...$(allDay)];

    if (events.length != 0) {
      for(let event of events) {
        let _id = $(event).find('input[name="id"]').val();
        if (_id == id) return event;
      }
    }

    return null;
  },

  currentTimeListener() {        
    let table = $(this.data.selectors.s_table)[0];    
    let cellHeight = this.data.ux.cellHeight;
    let timeout = 1000 * 60;

    this.setCurrentTime(table, cellHeight);
    setInterval(() => {
      this.setCurrentTime(table, cellHeight);
    }, timeout);  
  },

  setCurrentTime(table, height) {
    let _moment = moment();
    let minutes = (_moment.hour() * 60) + _moment.minute();
    let offset = (height * minutes) / 60; 
      
    table.style.setProperty('--time-offset', `${offset}px`); 
  },

  calcEventPosition(selector, start, finish) {
    let s = this.data.selectors;
    let c = this.data.css;    

    let $event = $(`#${selector}`);     
    let $wrapper = $(`#${selector} ${s.s_eventContentWrapper}`);  

    // Convert start and finish to the same date
    finish = moment(start).set({hour: finish.getHours(), minute: finish.getMinutes()}).toDate();
    if (finish.getHours() == 0 &&
        finish.getMinutes() == 0) {
      finish = moment(finish).add(1, 'days').toDate();
    }

    let minutesDiff = Math.abs(start.getTime() - finish.getTime()) / 1000.0 / 60.0;
    let factor = (this.data.ux.cellHeight * minutesDiff) / 60.0 - this.data.ux.cellHeight;
    let margin = (this.data.ux.cellHeight * start.getMinutes()) / 60.0;
    
    let height = this.data.ux.eventHeight + factor;

    if (height < this.data.ux.eventHeight) {
      $wrapper.addClass(c.s_eventWrapperTiny);
    } else {
      $wrapper.removeClass(c.s_eventWrapperTiny);
    }
    
    $event.css('margin-top', `${margin}px`);    
    $event.css('height', `${height}px`);   
  },

  changeEventPosition(selector, title, start, finish) {
    let id = $(`#${selector}`).find('input[name="id"]').val();
    let cell = this.data.selectors.s_cell;
    let hours = start.getHours();

    let dataTime = hours > 12 ? +hours - 12 : +hours; 
        dataTime += hours < 12 ? ' AM' : ' PM';

    let container = $(`${cell}[data-time='${dataTime}']`)[0];
    
    $(`#${selector}`).remove();
    let color = this.getCachedColor();
    this.renderEvent(container, selector, id, title, start, finish, color);
  },

  changeEventCalendar(id) {
    new CalendarRepository().get(id, calendar => {
      let event = this.getCachedEvent();

      if (event != '')
        $(`#${event}`).css('background-color', calendar.color.hex); 
    });     
  },

  eventRollback() {
    let settings = this.data.cache.rollback;
    let target = this.getCachedEvent();
    $('#' + target).remove();

    let container = this.findCellByTime(settings.start);    

    if (settings.isAllDay) {
      this.renderAllDayEvent(
        target,
        settings.id,
        settings.title,
        settings.color
      )
    } else {
      this.renderEvent(
        container, 
        target, 
        settings.id, 
        settings.title,
        settings.start,
        settings.finish,
        settings.color);
    }
  },

  eventExists(id) {
    let daily = this.data.selectors.s_dailyEvent;
    let allDay = this.data.selectors.s_allDayEvent;

    let events = [...$(daily), ...$(allDay)];

    if (events.length != 0) {
      for(let event of events) {
        let _id = $(event).find('input[name="id"]').val();
        if (_id == id) return true;
      }
    }

    return false;
  },

  setEventTitle(title, id) {
    if (id == undefined)
      return;

    if (!title || title.trim() === "") 
      title = "(No title)";
    
    let selector = `#${id} .title`;
    $(selector).text(title);
  },

  setEventTime(start, finish, selector) {
    let s_start = `#${selector} .start`;
    let s_finish = `#${selector} .end`;

    $(s_start).text(start);
    $(s_finish).text(finish);
  },
  
  cacheEvent(selector) {
    this.data.cache.cachedEvent = selector;
  },

  cacheColor(color) {
    this.data.cache.cachedColor = color;
  },

  cacheRollbackSettings(settings) {
    this.data.cache.rollback = settings;
  },

  getCachedColor() {
    return this.data.cache.cachedColor;
  },

  getCachedEvent() {
    return this.data.cache.cachedEvent;
  }  
}; 