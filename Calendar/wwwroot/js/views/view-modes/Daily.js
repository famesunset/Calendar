import { EventForm } from '../EventForm.js';
import { GUID } from '../../models/share/GUID.js';
import { EventRepository } from '../../models/mvc/EventRepository.js';
import { DeleteEvent } from '../pop-ups/DeleteEvent.js';

export let Daily = {
  data: {
    cache: {
      cachedEvent: '',
      c_targetCellShift: {},
      timeStart: {},
      timeFinish: {},
      state: ''      
    },

    selectors: {
      s_table: '#table-events',
      s_allDayEvents: '#all-day-events',
      s_dayOfWeek: '.date .day-of-week',
      s_dailyEvent: '.daily-event',
      s_allDayEvent: '.all-day-event',
      s_loadDeleteEvent: '.load-delete-event',
      s_eventContentWrapper: '.event-content-wrapper',
      s_eventWrapperTiny: '.daily-event-tiny',
      s_day: '.date .day', 
      s_cell: '.cell'     
    },

    css: {
      s_eventContentWrapper: 'event-content-wrapper',
      s_eventWrapperTiny: 'daily-event-tiny',
      c_loadDeleteEvent: 'load-delete-event'
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

  async run() {
    let repo = new EventRepository(); 
    let events = await repo.getList(new Date());

    this.renderDate(new Date(sessionStorage.getItem('currentDate')));
    this.renderTable();
    this.renderEvents(events);
    this.setUpListeners();    
  },

  setUpListeners() {
    let s = this.data.selectors;    
    
    $(s.s_cell).mousedown(e => {
      this.onCellMouseDown(e);
      $(s.s_table).mousemove(async (e) => this.onStretchEvent(e));
      $(s.s_table).mouseup((e) => this.onOpenCreateForm(e));      
    });        
  },  

  close() {
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

    this.renderEvent(container, selector, id, '(No title)', start, finish);
    this.cacheEvent(selector);
  },

  onEditEvent(e) {  
    e.stopPropagation();
    if (e.which != this.data.ux.leftMouseBtn)
      return;
          
    let id = $(e.currentTarget).find('input[name="id"]').val();
    
    if (id != 0 && EventForm.formCanOpen()) {
      EventForm.openEdit(id);    
      this.cacheEvent(e.currentTarget.id);
    }        
  },

  onDeleteEvent(e) {    
    e.stopPropagation();
    if (e.which != this.data.ux.rightMouseBtn)
      return;
    
    let $target = $(e.currentTarget);
    let eventId = $target.find('input[name="id"]').val();    
    let pos = { x: e.pageX, y: e.pageY };

    this.cacheEvent(e.currentTarget.id);    
    DeleteEvent.open(eventId, pos);
  },

  onCellMouseDown(e) {     
    if (e.which != this.data.ux.leftMouseBtn) 
      return;
    
    let container = e.target;
    let selector = GUID();
    let eventId = GUID();        

    let hour = $(container).find('input').val();

    let timeStart = moment(new Date(sessionStorage.getItem('currentDate')))
                    .hour(+hour)
                    .startOf('hour')
                    .toDate();
          
    let timeFinish = moment(timeStart).add(1, 'hours').toDate();

    this.renderEvent(container, selector, eventId, '(No title)', timeStart, timeFinish);

    let targetCoords = this.getCoords(container);
    this.data.cache.c_targetCellShift = Math.abs(e.pageY - targetCoords.y);
    this.data.ux.pos_mouseStart = e.pageY;
    this.data.cache.timeStart = timeStart;
    this.data.cache.timeFinish = timeFinish;
    this.data.cache.state = 'create';
  },

  onOpenCreateForm(e) {  
    if (e.which != this.data.ux.leftMouseBtn) 
      return;

    let s = this.data.selectors;

    EventForm.openCreate(      
      this.data.cache.timeStart,
      this.data.cache.timeFinish,      
    );

    $(s.s_table).unbind('mousemove');
    this.data.cache.state = '';
  },
  
  onStretchEvent(e) {
    if (e.which != this.data.ux.leftMouseBtn) 
      return;

    let cache = this.data.cache;
    
    let mouseStart = this.data.ux.pos_mouseStart;
    let mouseEnd = e.pageY;
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
    let el =  '<div class="cell" id="{{cell-id}}">' +
                `<input type="hidden" value="{{time}}">`
              '</div>';

    let time = new Date();
    time.setHours(0);

    let ampm = 'AM';
    let hour = 0;

    for (let i = 0; i < 24; ++i) {            
      ampm = hour < 12 ? 'AM' : 'PM';
      hour = hour > 12 ? hour - 12 : hour;      

      let dataContent = `${hour} ${ampm}`;
      let dataTime = dataContent;
      this.renderCell(el, time, dataContent, dataTime);

      time.setHours(time.getHours() + 1);      
      hour = time.getHours();
    }
  },

  renderCell(el, time, dataContent, dataTime) {
    let s = this.data.selectors;
    let hour = time.getHours();
    
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
    let m_date = moment(date);

    $(s.s_dayOfWeek).text(m_date.format('ddd'));
    $(s.s_day).text(m_date.format('D'));
  },

  renderEvents(events) {    
    events.forEach(event => {              
      let start = new Date(event.start);
      let finish = new Date(event.finish);
      let selector = GUID();     
      let container = $(`#cell-${start.getHours()}`);

      if (event.isAllDay) {
        this.renderAllDayEvent(selector, event.id, event.title);
      } else {
        this.renderEvent(container, selector, event.id, event.title, start, finish);
      }      
    });
  },

  renderEvent(container, selector, id, title, start, finish) {
    if (!title || title.trim() === '') {
      title = '(No title)';
    }
    
    var eventEl =   `<div class="daily-event" id="${selector}">` +
                        `<div class="${this.data.css.s_eventContentWrapper}">` +
                          `<input type="hidden" name="id" value="${id}">` +
                          `<h6 class="title">${title}</h4>` +
                          '<div class="time">' +
                              `<span class="start">${moment(start).format('LT')}</span>` +
                              '<span> &mdash; </span>' +
                              `<span class="end">${moment(finish).format('LT')}</span>` +
                          '</div>' +
                        '</div>' +
                        `<div class="${this.data.css.c_loadDeleteEvent}"></div>` +
                    '</div>';      

    $(container).append(eventEl);

    this.calcEventPosition(selector, start, finish);   
    this.cacheEvent(selector); 
    $(`#${selector}`).mousedown(e => this.onDeleteEvent(e));
    $(`#${selector}`).mouseup(e => {
      if (this.data.cache.state == 'create')
        this.onOpenCreateForm(e);
      else 
        this.onEditEvent(e)
    });
  },

  renderAllDayEvent(selector, id, title) {
    let s = this.data.selectors;    
    
    if (!title || title.trim() === '') {
      title = '(No title)';
    }

    let el = `<div class="all-day-event" id="${selector}">
                <input type="hidden" name="id" value="${id}">
                <span class="title">${title}</span>
                <div class="${this.data.css.c_loadDeleteEvent}"></div>
              </div>`;

    $(s.s_allDayEvents).append(el);

    this.cacheEvent(selector); 
    $(`#${selector}`).mousedown(e => this.onDeleteEvent(e));
    $(`#${selector}`).mouseup(e => this.onEditEvent(e));
  },

  hideEventsByCalendarId(calendarId) {
    // TODO: get events by calendarId

    // There will be an events array    
    let calendar = null;

    calendar.events.forEach(event => {                    
      let root = this.findRootByEventId(event.id);

      $(root).remove();
    });
  },

  showEventsByCalendarId(calendarId) {    
    let events = new EventRepository().getListByCalendarId(calendarId);;
    this.renderEvents(events);
  },

  findRootByEventId(id) {
    let daily = this.data.selectors.s_dailyEvent;
    let events = $(daily);

    if (events.length != 0) {
      for(let event of events) {
        let _id = $(event).find('input[name="id"]').val();
        if (_id == id) return event;
      }
    }

    return null;
  },

  async calcEventPosition(selector, start, finish) {
    let s = this.data.selectors;
    let c = this.data.css;    

    let $event = $(`#${selector}`);     
    let $wrapper = $(`#${selector} ${s.s_eventContentWrapper}`);  

    let minutesDiff = Math.abs(start.getTime() - finish.getTime()) / 1000.0 / 60.0;
    let factor = (this.data.ux.cellHeight * minutesDiff) / 60.0 - this.data.ux.cellHeight;
    let margin = (this.data.ux.cellHeight * start.getMinutes()) / 60.0;
    
    let height = this.data.ux.eventHeight + factor;

    if (height < this.data.ux.eventHeight) {
      $wrapper.addClass(c.s_eventWrapperTiny);
    } else {
      $wrapper.removeClass(c.s_eventWrapperTiny);
    }
    
    $event.css('top', `${margin}px`);    
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
    this.renderEvent(container, selector, id, title, start, finish);
  },

  setEventTitle(title, id) {
    if (!title || title.trim() === "") 
      title = "(No title)";

    let selector = `#${id} .title`;
    $(selector).text(title);
  },

  setEventTime(start, finish, guid) {
    let s_start = `#${guid} .start`;
    let s_finish = `#${guid} .end`;

    $(s_start).text(start);
    $(s_finish).text(finish);
  },
  
  cacheEvent(selector) {
    this.data.cache.cachedEvent = selector;
  },

  getCachedEvent() {
    return this.data.cache.cachedEvent;
  }  
}; 