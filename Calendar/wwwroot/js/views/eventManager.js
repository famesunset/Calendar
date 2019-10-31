import { DatePicker } from '../models/DatePicker.js';
import { TimePicker } from '../models/TimePicker.js';
import { Dropdown } from '../models/Dropdown.js';
import { Event } from '../models/Event.js';
import { TimeParse } from '../models/share/TimeParse.js';

const HOUR = 1000 * 60 * 60; // hour in ms

$(function() {
  let eventManager = {
    data: {
      datePickers: [],
      timePickers: [],
  
      selectors: {
        s_loadForm: '#load-create-event-form',
        s_loadOptions: '#more-options',
        s_wrapper: '.create-event-wrapper',
        s_form: '.create-event-form',      
        s_saveBtn: '#btn-save',
        s_createEventBtn: '.create-event-btn',
        s_closeTrigger: '.create-event-form .close',
        s_optionsTrigger: '.more-options-btn', 
        s_optionsWrapper: '#more-options',
        s_optionsContent: '.options',
        s_repeatDropdownTrigger: '#repeat-dropdown-trigger',      
        s_eventCell: '.cell'
      },
      
      url: {
        u_loadForm: 'LoadView/CreateEventForm',
        u_loadOptions: 'LoadView/EventMoreOptions',
        u_createEvent: 'Home/CreateEvent'
      }      
    },
  
    run: function() {
      this.setUpListeners();
    },
  
    setUpListeners: function() {
      let s = this.data.selectors;    

      $(s.s_createEventBtn).click(() => this.onCreateEventBtn());
      $(s.s_eventCell).click((e) => this.onCreateEventCell(e));         
    },
  
    setUpDynamicListeners: function() {
      let s = this.data.selectors;

      $(s.s_closeTrigger).click(() => this.onCloseForm());     
      $(s.s_optionsTrigger).click(() => this.onLoadOptionsTrigger());
      $(s.s_saveBtn).click(() => this.onSave());
    },
  
    // --- Listeners ---- //
  
    onCreateEventBtn: function() {
      let s = this.data.selectors;
      let url = this.data.url.u_loadForm;
      let currentDate = new Date(sessionStorage.getItem('currentDate'));
  
      $(s.s_loadForm).load(url, () => {
        let dateStart = new Date(currentDate);
        dateStart.setMinutes(0);
    
        let dateEnd = new Date(currentDate);
        dateEnd.setHours(dateStart.getHours() + 1, 0);
    
        this.initializeDatePickers(dateStart, dateEnd);
        this.initializeTimePickers(dateStart, dateEnd);
        this.openAnimation();
  
        this.setUpDynamicListeners();
      });
    },
  
    onCreateEventCell: function(e) {
      let container = this.data.selectors.s_loadForm;    
      let url = this.data.url.u_loadForm;
      let target = e.target;
      let hour = $(e.target).find('input').val();    
  
      $(container).load(url, () => {
        let dateStart = new Date(sessionStorage.getItem('currentDate'));
        dateStart.setHours(hour, 0);
    
        let dateEnd = new Date(sessionStorage.getItem('currentDate'));
        dateEnd.setHours(dateStart.getHours() + 1, 0);
          
        this.initializeDatePickers(dateStart, dateEnd);
        this.initializeTimePickers(dateStart, dateEnd);
    
        this.displayEvent(target, hour);
        this.openAnimation();
  
        this.setUpDynamicListeners();        
      });
    },

    onLoadOptionsTrigger: function() {    
      // Options are already open
      if (document.getElementById("options") != null) 
        return;     
        
      let container = this.data.selectors.s_loadOptions;
      let url = this.data.url.u_loadOptions;

      // Load more options to container
      $(container).load(url, () => {    
        let s = this.data.selectors;
        
        this.openOptionsAnimation();

        var dropdown = new Dropdown(s.s_repeatDropdownTrigger, { constrainWidth: false });
        dropdown.runDropdown();
        $('#repeat-dropdown-content a').click(event => dropdown.clickHandler(event));
        
        $('#custom-repeat').modal();        
      });
    },
  
    onSave: function() {
      let url = this.data.url.u_createEvent;
      let datePickers = this.data.datePickers;
      let timePickers = this.data.timePickers;

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
      
      this.onCloseForm();
    },

    onCloseForm: function() {
      let el = this.data.selectors.s_wrapper;
  
      this.closeAnimation();
      $(el).remove();    
    },
  
  
    // --- Initializers ---- //
  
    initializeDatePickers: function(dateStart, dateEnd) {
      this.data.datePickers = {
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
  
      for (let key in this.data.datePickers) {
        this.data.datePickers[key].runDatePicker();
      }
    },
  
    initializeTimePickers: function(dateStart, dateEnd) {      
      let timeStart = `${dateStart.getHours()}:${dateStart.getMinutes()}`;
      let timeEnd = `${dateEnd.getHours()}:${dateEnd.getMinutes()}`;
  
      this.data.timePickers = {
        'time-start': new TimePicker(dateStart, {            
          defaultTime: timeStart,
        }, '#time-start'),
  
        'time-finish': new TimePicker(dateEnd, {
          defaultTime: timeEnd,
          fromNow: HOUR
        }, '#time-finish')
      };
  
      for (let key in this.data.timePickers) {
        this.data.timePickers[key].setDefaultInputValue();
        this.data.timePickers[key].runTimePicker();
      }
    },
  
  
    // --- Animations --- //
  
    displayEvent: function(target, hour) {
      let start = new Date();
      start.setHours(hour, 0, 0);
  
      let end = new Date(start);
      end.setHours(start.getHours() + 1);
  
      var timeStart = new TimeParse(start);
      var timeEnd = new TimeParse(end);
  
      var eventEl =   '<div class="daily-event">' +
                          '<h6 class="title">(No title)</h4>' +
                          '<div class="time">' +
                              `<span class="start">${timeStart.getTime()}</span>` +
                              '<span> &mdash; </span>' +
                              `<span class="end">${timeEnd.getTime()}</span>` +
                          '</div>' +
                      '</div>';
  
      $(target).append(eventEl);
    },
  
    openAnimation: function() {
      var s = this.data.selectors;
      var $content = $(s.s_form);
          
      $content.css('display', 'block');
      $content.animate({
          opacity: 1
      }, 50);
    },
  
    closeAnimation: function() {
      var s = this.data.selectors;
      var $content = $(s.s_form);
  
      $content.animate({
        opacity: 0
      }, 50, () => $content.css('display', 'none'));
    },

    openOptionsAnimation: function() {
      var $wrapper = $(this.data.selectors.s_optionsWrapper);
      var $content = $(this.data.selectors.s_optionsContent);

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
  
  eventManager.run();
});