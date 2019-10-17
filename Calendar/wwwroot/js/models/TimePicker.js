import { Time } from './Time.js';

var TimePicker = function(date, options, selector) {    
    this.time = new Time(date);
    this.selector = selector;
    this.options = options;    
    this.instance = null;
};

TimePicker.prototype.setDefaultInputValue = function() {    
    let $input = $(this.selector);

    $input.val(this.time.getTime());    
};

TimePicker.prototype.runTimePicker = function() {
    this.instance = $(this.selector).timepicker(this.options);

    let instance = this.getInstance();
    instance.amOrPm = this.time.ampm;
};

TimePicker.prototype.getInstance = function() {    
    return this.instance[0].M_Timepicker;
};

TimePicker.prototype.getDate = function() {    
    let time = $(this.selector).val();
    // delete AM or PM and split hours from minutes
    let [hours, minutes] = time.slice(0, -3).split(':');
    let ampm = this.getInstance().amOrPm;

    hours = ampm === 'PM' ? +hours + 12 : hours;

    let date = new Date();
    date.setHours(hours, minutes);

    return date;
};

export { TimePicker };