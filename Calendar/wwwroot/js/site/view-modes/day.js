import { DayViewCell } from '../../models/DayViewCell.js';
import { DateHelper } from '../../models/DateHelper.js';

$(function() {
    new DayViewCell('#table-events').renderCells();
    
    var dateHelper = new DateHelper(new Date());
    
    $('.date .day-of-week').text(dateHelper.getDayOfWeek());
    $('.date .day').text(dateHelper.date.getDate());

});