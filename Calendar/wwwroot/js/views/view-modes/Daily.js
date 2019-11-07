import { DateParse } from '../../models/share/DateParse.js';
import { TimeParse } from '../../models/share/TimeParse.js';
import { EventForm } from '../EventForm.js';
import { GUID } from '../../models/share/GUID.js';
import { EventRepository } from '../../models/mvc/EventRepository.js';

let Daily = {
  data: {
    cash: {
      c_lastEventId: '',
      c_targetCellShift: {},
      timeStart: {},
      timeEnd: {}
    },

    selectors: {
      s_table: '#table-events',
      s_dayOfWeek: '.date .day-of-week',
      s_eventContentWrapper: '.event-content-wrapper',
      s_eventWrapperTiny: '.daily-event-tiny',
      s_day: '.date .day', 
      s_cell: '.cell'     
    },

    ux: {
      cellHeight: 50,
      eventHeight: 46,
      pos_mouseStart: 0    
    },

    css: {
      s_eventContentWrapper: 'event-content-wrapper',
      s_eventWrapperTiny: 'daily-event-tiny',
    }
  },

  async run() {
    let repo = new EventRepository(); 
    let events = await repo.getList();

    this.renderDate(new Date(sessionStorage.getItem('currentDate')));
    this.renderTable();
    this.renderEvents(events);
    this.setUpListeners();
  },

  setUpListeners() {
    let s = this.data.selectors;

    $(s.s_cell).mousedown(e => {
      this.onCellMouseDown(e);
      $(s.s_table).mousemove((e) => this.onTableMouseMove(e));
      $(s.s_table).mouseup((e) => this.onCellMouseUp(e));      
    });        
  },

  onCreateEvent(container) {    
    let guid = GUID();
    let id = GUID();

    let hour = $(container).find('input').val();
    let start = new Date(sessionStorage.getItem('currentDate'));
        start.setHours(hour, 0);
          
    let end = new Date(start);
        end.setHours(start.getHours() + 1);

    EventForm.open(start, end);       

    this.renderEvent(container, guid, id, '(No title)', start, end);
    this.cashLastEvent(guid);
  },

  onCellMouseDown(e) {        
    let container = e.target;
    let eventWrapperId = GUID();
    let eventId = GUID();        

    let hour = $(container).find('input').val();
    let start = new Date(sessionStorage.getItem('currentDate'));
        start.setHours(hour, 0);
          
    let end = new Date(start);
        end.setHours(start.getHours() + 1);

    this.renderEvent(container, eventWrapperId, eventId, '(No title)', start, end);

    let targetCoords = this.getCoords(container);
    this.data.cash.c_targetCellShift = Math.abs(e.pageY - targetCoords.y);
    this.data.ux.pos_mouseStart = e.pageY;
    this.data.cash.timeStart = start;
    this.data.cash.timeEnd = end;
  },

  onCellMouseUp(e) {  
    let s = this.data.selectors;

    EventForm.open(
      this.data.cash.timeStart,
      this.data.cash.timeEnd,      
    );

    $(s.s_table).unbind('mousemove');
  },
  
  onTableMouseMove(e) {
    let mouseStart = this.data.ux.pos_mouseStart;
    let mouseEnd = e.pageY;
    let mouseOffset = mouseEnd - mouseStart + this.data.cash.c_targetCellShift;

    if (mouseOffset >= 28) {
      let minutesOffset = (mouseOffset * 60) / this.data.ux.cellHeight;      

      if (minutesOffset % 30 == 0) {
          console.log("+");
        let timeStart = this.data.cash.timeStart;
        let timeEnd = new Date(timeStart);
            timeEnd.setMinutes(timeStart.getMinutes() + minutesOffset);
    
        this.setEventTime(
          new TimeParse(timeStart).getTime(),
          new TimeParse(timeEnd).getTime(),
          this.data.cash.c_lastEventId
        );
        
        this.calcEventPosition(this.data.cash.c_lastEventId, timeStart, timeEnd); 
    
        this.data.cash.timeStart = timeStart;
        this.data.cash.timeEnd = timeEnd;
      }
    }
  },

  getCoords(elem) {
    var box = elem.getBoundingClientRect();
    
    return {
      x: box.x,
      y: box.y     
    };
  },

  renderTable() {  
    let time = new Date();
    time.setHours(0);

    let el =  '<div class="cell" id="{{cell-id}}">' +
                `<input type="hidden" value="{{time}}">`
              '</div>';

    let timeZone = new DateParse(new Date()).getTimeZone();
    this.renderCell(el, time, timeZone, '0 AM');

    let ampm = 'AM';
    let hour = 0;
    for (let i = 0; i < 23; ++i) {  
      time.setHours(time.getHours() + 1);      
      hour = time.getHours();
      
      ampm = hour < 12 ? 'AM' : 'PM';
      hour = hour > 12 ? hour - 12 : hour;      

      let dataContent = `${hour} ${ampm}`;
      let dataTime = dataContent;

      this.renderCell(el, time, dataContent, dataTime);
    }
  },

  renderEvents(events) {    
    events.forEach(event => {      
      let start = new Date(event.start);
      let end = new Date(event.finish);
      let guid = GUID();
      
      let container = $(`#cell-${start.getHours()}`);
      this.renderEvent(container, guid, event.id, event.title, start, end);
    });
  },

  renderCell(el, time, dataContent, dataTime) {
    let s = this.data.selectors;
    let hour = time.getHours();
    
    let html = el.replace('{{time}}', hour);
    html = html.replace('{{cell-id}}', `cell-${hour}`);
    html = $(html).attr('data-content', dataContent);
    html = $(html).attr('data-time', dataTime);

    $(s.s_table).append(html);
  },

  renderDate(date) {
    let s = this.data.selectors;
    var dateParse = new DateParse(date);

    $(s.s_dayOfWeek).text(dateParse.getDayOfWeek());
    $(s.s_day).text(dateParse.date.getDate());
  },

  renderEvent(container, guid, id, title, start, end) {
    if (!title || title.trim() === '') {
      title = '(No title)';
    }

    var timeStart = new TimeParse(start).getTime();
    var timeEnd = new TimeParse(end).getTime();
    
    var eventEl =   `<div class="daily-event" id="${guid}">` +
                        `<div class="${this.data.css.s_eventContentWrapper}">` +
                          `<input type="hidden" name="id" value="${id}">` +
                          `<h6 class="title">${title}</h4>` +
                          '<div class="time">' +
                              `<span class="start">${timeStart}</span>` +
                              '<span> &mdash; </span>' +
                              `<span class="end">${timeEnd}</span>` +
                          '</div>' +
                        '</div>' +
                    '</div>';      

    $(container).append(eventEl);
    this.calcEventPosition(guid, start, end);   
    this.cashLastEvent(guid); 
  },

  calcEventPosition(id, start, end) {
    let s = this.data.selectors;
    let c = this.data.css;    

    let $event = $(`#${id}`);     
    let $wrapper = $(`#${id} ${s.s_eventContentWrapper}`);  

    let minutesDiff = Math.abs(start.getTime() - end.getTime()) / 1000.0 / 60.0;
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

  changeEventPosition(guid, title, start, end) {
    let id = $(`#${guid}`).find('input[name="id"]').val();
    let cell = this.data.selectors.s_cell;
    let hours = start.getHours();

    let dataTime = hours > 12 ? +hours - 12 : +hours; 
        dataTime += hours < 12 ? ' AM' : ' PM';

    let container = $(`${cell}[data-time='${dataTime}']`)[0];
    
    $(`#${guid}`).remove();
    this.renderEvent(container, guid, id, title, start, end);
  },

  setEventTitle(title, guid) {
    if (!title || title.trim() === "") 
      title = "(No title)";

    let selector = `#${guid} .title`;
    $(selector).text(title);
  },

  setEventTime(start, end, guid) {
    let s_start = `#${guid} .start`;
    let s_end = `#${guid} .end`;

    $(s_start).text(start);
    $(s_end).text(end);
  },

  cashLastEvent(guid) {
    this.data.cash.c_lastEventId = guid;
  },

  lastEventId() {
    return this.data.cash.c_lastEventId;
  }  
}; 

export { Daily };