var Time = function(date) {
    this.hours = date.getHours();
    this.minutes = date.getMinutes();
    this.ampm = `${date.getHours() < 12 ? 'AM' : 'PM'}`;
};

var TimePicker = function(date, options, inputSelector) {    
    this.time = new Time(date);
    this.selector = inputSelector;
    this.options = options;    
};

TimePicker.prototype.setDefaultInputValue = function() {    
    let $input = $(this.selector);

    $input.val(this.time.getTime());    
};

TimePicker.prototype.runTimePicker = function() {
    $(this.selector).timepicker(this.options);
};

Time.prototype.getTime = function() {
    if (this.hours > 12)
        this.hours -= 12;

    let showMinutes = this.minutes < 10 ? `0${this.minutes}` : this.minutes;
    
    return `${this.hours}:${showMinutes} ${this.ampm}`;
}

export { TimePicker };