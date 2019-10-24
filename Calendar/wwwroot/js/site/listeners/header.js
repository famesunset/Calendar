import { Dropdown } from "../../models/Dropdown.js";
import { DayView } from "../../models/DayView.js";

$(function() {
    var dropdownViewMode = new Dropdown('.view-mode', { constrainWidth: false });

    let currentDate = sessionStorage.getItem('currentDate');
    var viewMode = new DayView(new Date(currentDate), '.header-date');
    
    dropdownViewMode.runDropDown();
    $('#view-mode a').click(event => dropdownViewMode.clickHandler(event));    

    $('.swich-date #next').click(() => viewMode.next());
    $('.swich-date #prev').click(() => viewMode.prev());

    $('#today').click(() => viewMode.setDate(new Date()));
});