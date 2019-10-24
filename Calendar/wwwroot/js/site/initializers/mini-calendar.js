import { DatePicker } from '../../models/DatePicker.js';
import { DayView } from '../../models/DayView.js';

var miniCalendar;

$(function() {    
    let viewMode = new DayView(null, '.header-date');

    miniCalendar = new DatePicker('#mini-calendar', {
        container: '.mini-calendar',
        setDefaultDate: true,
        defaultDate: new Date(),
        firstDay: 1,
        animation: false,
        onSelect: (date) => {
            viewMode.updateDate(new Date(date));
        }
    });

    miniCalendar.runDatePicker();
    var instance = miniCalendar.getInstance();
    instance.open();
});

export { miniCalendar };