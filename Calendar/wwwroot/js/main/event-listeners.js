import { Dropdown } from "../models/Dropdown.js";
import { AddEventAnimation } from '../animations/AddEventAnimation.js';

$(function() {
    let dropdownViewMode = new Dropdown('.view-mode', { constrainWidth: false });
    dropdownViewMode.runDropDown();
    $('#view-mode a').click(event => dropdownViewMode.clickHandler(event));

    $('.create-event-btn').click(() => 
        new AddEventAnimation(
            '#load-create-event-form', 
            '.create-event-form')
        .open());
});