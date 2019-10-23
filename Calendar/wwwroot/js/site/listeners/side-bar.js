import { AddEventAnimation } from '../../animations/AddEventAnimation.js';

$(function() {
    $('.create-event-btn').click(() => 
        new AddEventAnimation(
            '#load-create-event-form', 
            '.create-event-form')
        .open());
});