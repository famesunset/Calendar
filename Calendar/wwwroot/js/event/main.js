import { DatePicker } from "../models/DatePicker.js";
import { TimePicker } from "../models/TimePicker.js";

const HOUR = 1000 * 60 * 60; // hour in ms

var datePickers = { };
var timePickers = { };

$(function () {    
    let now = new Date();
    let nextHour = new Date();
    nextHour.setHours(now.getHours() + 1);

    datePickers = {
        "date-start": new DatePicker('#date-start', {
            setDefaultDate: true,
            defaultDate: new Date(),
            firstDay: 1
        }),

        "date-finish": new DatePicker('#date-finish', {
            setDefaultDate: true,
            defaultDate: new Date(),
            firstDay: 1
        })
    };

    timePickers = {
        'time-start': new TimePicker(now, {
            defaultTime: "now"
        }, '#time-start'),

        'time-finish': new TimePicker(nextHour, {
            defaultTime: `now`,
            fromNow: HOUR
        }, '#time-finish')
    };

    for (let key in datePickers) {        
        datePickers[key].runDatePicker();
    }

    for (let key in timePickers) {
        timePickers[key].setDefaultInputValue();
        timePickers[key].runTimePicker();
    }    
});

export {
    datePickers,
    timePickers
};