import { DateParse } from '../../models/share/DateParse.js';
import { TimeParse } from '../../models/share/TimeParse.js';
import { EventForm } from '../EventForm.js';
import { GUID } from '../../models/share/GUID.js';
import { EventRepository } from '../../models/mvc/EventRepository.js';

let Daily = {
  data: {

    ux: {
      cellHeight: 50,
      eventHeight: 46
    },

    cash: {
      c_lastEventId: ''
    },

    selectors: {
      s_table: '#table-events',
      s_dayOfWeek: '.date .day-of-week',
      s_eventContentWrapper: '.event-content-wrapper',
      s_eventWrapperTiny: '.daily-event-tiny',
      s_day: '.date .day', 
      s_cell: '.cell'     
    },

    classes: {
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

    $(s.s_cell).click(e => this.onCreateEvent(e.target));
  },

  onCreateEvent(container) {    
    let guid = GUID();
    let id = GUID();

    let hour = $(container).find('input').val();
    let start = new Date(sessionStorage.getItem('currentDate'));
        start.setHours(hour, 0);
          
    let end = new Date(start);
        end.setHours(start.getHours() + 1);

    EventForm.open(start);       

    this.renderEvent(container, guid, id, '(No title)', start, end);
    this.cashLastEvent(guid);
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
                        `<div class="${this.data.classes.s_eventContentWrapper}">` +
                          `<input type="hidden" name="id" value="${id}">` +
                          `<h6 class="title">${title}</h4>` +
                          '<div class="time">' +
                              `<span class="start">${timeStart}</span>` +
                              '<span> &mdash; </span>' +
                              `<span class="end">${timeEnd}</span>` +
                          '</div>' +
                        '</div>' +
                    '</div>';      

    this.displayEvent(eventEl, container, guid, start, end);    
  },

  displayEvent(el, container, id, start, end) {
    let s = this.data.selectors;
    let c = this.data.classes;

    $(container).append(el);
    let $event = $(`#${id}`);     
    let $wrapper = $(`#${id} ${s.s_eventContentWrapper}`);  

    let minutesDiff = Math.abs(start.getTime() - end.getTime()) / 1000.0 / 60.0;
    let factor = (this.data.ux.cellHeight * minutesDiff) / 60.0 - 50.0;
    let margin = (this.data.ux.cellHeight * start.getMinutes()) / 60.0;
    
    let height = this.data.ux.eventHeight + factor;

    if (height < 46) {
      $wrapper.addClass(c.s_eventWrapperTiny);
    } else {
      $wrapper.removeClass(c.s_eventWrapperTiny);
    }

    $event.css('height', `${height}px`);   
    $event.css('top', `${margin}px`);

    this.cashLastEvent(id);
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