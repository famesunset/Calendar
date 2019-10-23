import { Dropdown } from "../../models/Dropdown.js";
import { DayView } from "../../models/DayView.js";

$(function() {
    var dropdownViewMode = new Dropdown('.view-mode', { constrainWidth: false });
    var viewMode = new DayView(new Date());
    
    dropdownViewMode.runDropDown();
    $('#view-mode a').click(event => dropdownViewMode.clickHandler(event));    

    $('.swich-date #next').click(() => viewMode.next());
    $('.swich-date #prev').click(() => viewMode.prev());
});