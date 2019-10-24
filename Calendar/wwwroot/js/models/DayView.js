import { DateHelper } from './DateHelper.js';

var DayView = function(date, headerDateSelector) {
    this.date = date;
    this.headerDateSelector = headerDateSelector;
};

DayView.prototype.updateHeaderDate = function(date) {
    var dateHelper = new DateHelper(date);

    let year = date.getYear() + 1900;
    let month = dateHelper.getMonthOfYear();

    $(this.headerDateSelector).text(`${month} ${year}`);
}

DayView.prototype.updateDate = function(date) {
    var dateHelper = new DateHelper(date);

    $('.date .day-of-week').text(dateHelper.getDayOfWeek());
    $('.date .day').text(dateHelper.date.getDate());

    this.date = date;
    this.updateHeaderDate(this.date);

    sessionStorage.setItem('currentDate', this.date);
};

DayView.prototype.setDate = function(date) {
    var elem = $('#mini-calendar');
    var instance = M.Datepicker.getInstance(elem);

    instance.setDate(date);
}

DayView.prototype.next = function() {   
    this.date = new Date(sessionStorage.getItem('currentDate'));
    
    var nextDay = this.date;
    nextDay.setDate(this.date.getDate() + 1);    

    this.setDate(nextDay);
};

DayView.prototype.prev = function() {
    this.date = new Date(sessionStorage.getItem('currentDate'));

    var prevDay = this.date;
    prevDay.setDate(this.date.getDate() - 1);

    this.setDate(prevDay);
};

export { DayView };