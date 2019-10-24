import { DayViewCell } from '../../models/DayViewCell.js';
import { DateHelper } from '../../models/DateHelper.js';

$(function() {
    new DayViewCell('#table-events').renderCells();
    
    let currentDate = sessionStorage.getItem('currentDate');    
    var dateHelper = new DateHelper(new Date(currentDate));
    
    $('.date .day-of-week').text(dateHelper.getDayOfWeek());
    $('.date .day').text(dateHelper.date.getDate());

});