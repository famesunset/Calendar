import { DateHelper } from './DateHelper.js';

var RepeatDropdown = function(date, options) {
    this.dateHelper = new DateHelper(date);
    this.options = options;
}

RepeatDropdown.prototype.runDropDown = function(selector) {
    $(selector).dropdown(this.options);
};

RepeatDropdown.prototype.setEveryWeekText = function(selector) {
    $(selector).text(`Every week on ${this.dateHelper.getDayOfWeek()}`);
};

RepeatDropdown.prototype.setEveryMonthText = function(selector) {
    $(selector).text(`Every month of the ${this.dateHelper.getDayOfMonth()}`);
};

RepeatDropdown.prototype.setEveryYearText = function(selector) {
    $(selector).text(`Every year on ${this.dateHelper.getMonthOfYear()} ${this.dateHelper.date.getDate()}`);
};

RepeatDropdown.prototype.clickHandler = function(event) {
    var $option = $(event.target);   
    var $trigger = $(".dropdown-trigger");
    
    // set the text of the selected option
    $trigger.text($option.text());

    if ($option.hasClass('custom-repeat')) {
        console.log("Custom");
    }
};

export { RepeatDropdown };