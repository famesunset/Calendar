import { Dropdown } from '../models/Dropdown.js';
import { dailyViewMode } from './viewModes/dailyViewMode.js';

$(function() {
  let header = {
    data: {
      dropdown: null,

      viewModes: {
        daily: dailyViewMode
      },

      selectors: {
        s_switchNext: '.switch-date #next',
        s_switchPrev: '.switch-date #prev',
        s_todayBtn: '#today',
        s_dropdownTrigger: '.view-mode',
        s_dropdownItem: '#view-mode a'
      }
    },

    run: function() {
      this.data.viewModes.daily.run();
      this.initializeDropdown();
      this.setUpListeners();
    },

    setUpListeners: function() {
      let s = this.data.selectors;

      $(s.s_dropdownItem).click((e) => this.onSelectDropdownItem(e));
      $(s.s_switchNext).click(() => this.onSwitchDateNext());
      $(s.s_switchPrev).click(() => this.onSwitchDatePrev());
      $(s.s_todayBtn).click(() => this.onSwitchDateToday());
    },

    // --- Listeners ---- //
    onSelectDropdownItem: function(e) {
      this.data.dropdown.clickHandler(e);
    },

    onSwitchDateNext: function() {
      // TODO смотреть на текущий выбранный mode 
      // и изменять значение viewMode
      let viewMode = this.data.viewModes.daily;

      viewMode.onNext();
    },

    onSwitchDatePrev: function() {
      let viewMode = this.data.viewModes.daily;

      viewMode.onPrev();
    },

    onSwitchDateToday: function() {
      this.data.viewModes.daily.onToday();
    },
    

    // --- Initializers ---- //
    initializeDropdown: function() {
      let s = this.data.selectors;

      this.data.dropdown = new Dropdown(s.s_dropdownTrigger, { constrainWidth: false });
      this.data.dropdown.runDropdown();
    }
  };

  header.run();
});