import { Dropdown } from '../../models/Dropdown.js';
import { Event } from '../../models/Event.js';
import { MoreOptionsAnimation } from '../../animations/MoreOptionsAnimation.js';
import { AddEventAnimation } from '../../animations/AddEventAnimation.js'
import { datePickers, timePickers } from '../initializers/add-event-form.js';

$(function () {    
    var addEventAnimation = new AddEventAnimation('', '.create-event-form');

    $('.create-event-form .close').click(() => addEventAnimation.close());

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

        event.sendToMVC('Home/CreateEvent');  
        addEventAnimation.close();
    });

    $('.more-options-btn').click(() => {
        // Options are already open
        if (document.getElementById("options") != null) 
            return;        

        // Load more options to container
        $('#more-options').load('LoadView/EventMoreOptions', () => {              
            var optionsAnimation = new MoreOptionsAnimation('#more-options', '.options');
            optionsAnimation.runAnimation();

            var dropdown = new Dropdown('#repeat-dropdown-trigger', { constrainWidth: false });
            dropdown.runDropDown();
            $('#repeat-dropdown-content a').click(event => dropdown.clickHandler(event));
            
            $('#custom-repeat').modal();        
        });
    });
});