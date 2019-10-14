import { DatePicker } from "../models/DatePicker.js";
import { TimePicker } from "../models/TimePicker.js";

const HOUR = 1000 * 60 * 60; // hour in ms

$(function() {         
    let now = new Date();
    let nextHour = new Date();
    nextHour.setHours(now.getHours() + 1);

    let datePicker = new DatePicker(new Date());
    let leftTimePicker = new TimePicker(now, { defaultTime: "now" }, '.timepicker.left-range');
    let rightTimePicker = new TimePicker(nextHour, { defaultTime: `now`, fromNow: HOUR }, '.timepicker.right-range');

    leftTimePicker.setDefaultInputValue();
    rightTimePicker.setDefaultInputValue();

    datePicker.runDatePickers();
    leftTimePicker.runTimePicker();
    rightTimePicker.runTimePicker();
});