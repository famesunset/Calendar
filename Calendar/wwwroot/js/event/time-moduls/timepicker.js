const HOUR = 1000 * 60 * 60; // hour in ms

var Time = function(date) {
    this.hours = date.getHours(),
    this.minutes = date.getMinutes(),
    this.ampm = `${date.getHours() < 12 ? 'AM' : 'PM'}`
}

Time.prototype.getTime = function() {
    if (this.hours > 12)
        this.hours -= 12;
    
    return `${this.hours}:${this.minutes} ${this.ampm}`;
}

var setDefaultInputSettings = function($leftRange, $rightRange) {
    let now = new Date();
    let nextHour = new Date();
    nextHour.setHours(now.getHours() + 1);
    
    let timeNow = new Time(now);
    let timeNextHour = new Time(nextHour);

    $leftRange.val(timeNow.getTime());    
    $rightRange.val(timeNextHour.getTime());
}

var optionsLeftRange = {
    defaultTime: "now"
}

var optionsRightRange = {
    defaultTime: `now`,
    fromNow: HOUR
}

export { optionsLeftRange, optionsRightRange, setDefaultInputSettings };