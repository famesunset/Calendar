import { DateHelper } from './DateHelper.js';

var DayView = function(date) {
    this.date = date;
};

DayView.prototype.next = function() {    
    var nextDay = new Date();
    nextDay.setDate(this.date.getDate() + 1);

    var dateHelper = new DateHelper(nextDay);

    $('.date .day-of-week').text(dateHelper.getDayOfWeek());
    $('.date .day').text(dateHelper.date.getDate());

    this.date = nextDay;
};

DayView.prototype.prev = function() {
    var prevDay = new Date();
    prevDay.setDate(this.date.getDate() - 1);

    var dateHelper = new DateHelper(prevDay);    

    $('.date .day-of-week').text(dateHelper.getDayOfWeek());
    $('.date .day').text(dateHelper.date.getDate());

    this.date = prevDay;
};

export { DayView };