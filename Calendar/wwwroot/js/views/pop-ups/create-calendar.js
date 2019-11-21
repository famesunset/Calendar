import { CalendarRepository } from '../../models/mvc/calendar-repository.js';

// Тянуть форму с backend
export let CreateCalendar = {
  data: {
    selectors: {
      s_colors: '#colors'
    },

    css: {
      c_color: 'color',
      c_colorSelected: 'color-selected'
    }
  },  

  setUpListeners() {

  },

  open() {
    this.initialzeColors();
    // this.openModal();
  },

  close() {

  },

  async initialzeColors() {
    let uiColors = $(this.data.selectors.s_colors);
    let colors = await new CalendarRepository().getColorList();

    for(let color of colors) {
      let el = 
      `<div class="color">
        <div class="value"></div>        
        <input type="hidden" value="#9E69AF">
      </div>`;
      
      $(el).css('background-color', color.hex);
      $(uiColors).append(el);
    }    
  }
};