import { TimeParse } from './share/TimeParse.js';

class TimePicker {
  constructor(date, options, selector) {
    this.time = new TimeParse(date);
    this.selector = selector;
    this.options = options;
    this.instance = null;
  }

  setDefaultInputValue() {
    let $input = $(this.selector);    
    $input.val(this.time.getTime());
  }

  runTimePicker() {
    let instance = $(this.selector).timepicker(this.options);

    this.instance = instance[0].M_Timepicker;
    this.instance.amOrPm = this.time.ampm;
  }

  getInstance() {    
    return this.instance;
  }

  getDate() {
    let time = $(this.selector).val();

    // delete AM or PM and split hours from minutes
    let [hours, minutes] = time.slice(0, -3).split(':');
    let ampm = this.instance.amOrPm;
    hours = ampm === 'PM' ? +hours + 12 : hours;
    
    let date = new Date();
    date.setHours(hours, minutes);

    return date;
  }
}

export { TimePicker };