import { dateOptions } from "./time-moduls/datepicker.js";
import { optionsLeftRange, optionsRightRange, setDefaultInputSettings } from "./time-moduls/timepicker.js";

$(function() {
    let $datepicker = $(".datepicker");
    let $timepickerLeft = $(".timepicker.left-range");
    let $timepickerRight = $(".timepicker.right-range");

    $datepicker.datepicker(dateOptions);    

    setDefaultInputSettings($timepickerLeft, $timepickerRight);
    $timepickerLeft.timepicker(optionsLeftRange);
    $timepickerRight.timepicker(optionsRightRange);
});