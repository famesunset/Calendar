import { RepeatDropdown } from '../models/RepeatDropdown.js';
import { TimePicker } from '../models/TimePicker.js';
import { Event } from '../models/Event.js';
import { datePickers, timePickers } from './main.js';

$(function () {
    $('.more-options-btn').click(() => {
        // Options are already open
        if (document.getElementById("options") != null) 
            return;        

        // Load more options to container
        $('#more-options').load('LoadView/EventMoreOptions', () => {
            // TODO: переколхозить
            // Тянуть значения для dropdown из backend
            var dropdown = new RepeatDropdown(new Date(), {
                constrainWidth: false
            });

            dropdown.setEveryWeekText(`#every-week`);
            dropdown.setEveryMonthText('#every-month');
            dropdown.setEveryYearText('#every-year');

            dropdown.runDropDown('.dropdown-trigger');

            $('.dropdown-content a').click((event) => {
                dropdown.clickHandler(event);
            });

            $('#custom-repeat').modal();        
        });
    });

    $('#btn-save').click((event) => {
        let title = $('#title').val();
        let description = $('#description').val();
        let isAllDay = $('#all-day').is(":checked");
        let dateStart = datePickers['date-start'].getDate();
        let dateFinish = datePickers['date-finish'].getDate();
        let timeStart = timePickers['time-start'].getDate();
        let timeFinish = timePickers['time-finish'].getDate();

        var event = new Event(
            title,
            description,
            dateStart,
            dateFinish,
            timeStart,
            timeFinish,
            isAllDay,
            null,
            null
        );

        event.sendToMVC();        
    });
});