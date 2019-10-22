import { Component } from './Component.js'

var DayViewCell = function(container) {
    this.container = container;
};

DayViewCell.prototype.renderCells = function() {
    var el = '<div class="cell"></div>';
    var component = new Component(el);

    var offset = (new Date().getTimezoneOffset() / 60) * -1;
    var moduleOffset = Math.abs(offset);

    let timeZone = `GMT ${offset > 0 ? '+' : '-'}${moduleOffset}`;

    component.setDataContent(timeZone);
    component.appentTo(this.container);

    var hours = 23;
    var hour = 1;
    var ampm = "AM";    

    for(let i = 0; i < hours; ++i) {
        if (hour > 12) {
            hour = 1;
            ampm = 'PM';
        }
        
        component = new Component(el);   
        component.setDataContent(`${hour++} ${ampm}`);
        component.appentTo(this.container);
    } 
};

export { DayViewCell };