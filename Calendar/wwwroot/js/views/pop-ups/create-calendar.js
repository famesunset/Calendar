import { Modal } from '../pop-ups/modal.js';
import { CalendarList } from '../calendar-list.js';

export let CreateCalendar = {
  data: {
    selectors: {
      s_loadContainer: '#create-calendar',
      s_createCalendarForm: '.create-calendar',
      s_createCalendarSubmit: '#create-calendar-submit',
      s_selectedColor: '.color-selected',
      s_colors: '#colors',
      s_color: '.color'
    },

    html: {
      colorSelected: '<i class="material-icons tiny color-selected">check</i>',
    },

    css: {
      c_color: 'color',
      c_colorSelected: 'color-selected'
    },

    url: {
      loadForm: '/CalendarView/GetCreateCalendarForm'
    }
  },  

  setUpListeners() {
    let s = this.data.selectors;

    $(s.s_color).click(e => this.onColorSelected(e));
    $(s.s_createCalendarSubmit).click(() => this.onCreateCalendar());
  },

  open() {
    let url = this.data.url.loadForm;
    let container = this.data.selectors.s_loadContainer;    
    
    $(container).load(url, () => {
      Modal.open(this.close);
      this.setUpListeners();
    });
  },

  close() {
    let _this = CreateCalendar;
    let form = _this.data.selectors.s_createCalendarForm;
    $(form).remove();
    Modal.close();
  },

  onColorSelected(e) {
    let color = e.currentTarget;
    let prevSelected = this.data.selectors.s_selectedColor;
    let htmlSelected = this.data.html.colorSelected;

    $(prevSelected).remove();
    $(color).append(htmlSelected);
  },

  onCreateCalendar() {
    let form = this.data.selectors.s_createCalendarForm;
    let selectedColor = $(this.data.selectors.s_selectedColor)[0].parentElement;

    let colorId = $(selectedColor).find('input[name="colorId"]').val();   
    let name = $(form).find('input[name="calendarName"]').val();    

    CalendarList.addCalendar(name, colorId);
    this.close();
  }, 
};