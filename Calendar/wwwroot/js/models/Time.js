var Time = function(date) {
    this.hours = date.getHours();
    this.minutes = date.getMinutes();
    this.ampm = `${date.getHours() < 12 ? 'AM' : 'PM'}`;
};

Time.prototype.getTime = function() {
    if (this.hours > 12)
        this.hours -= 12;

    let showMinutes = this.minutes < 10 ? `0${this.minutes}` : this.minutes;
    
    return `${this.hours}:${showMinutes} ${this.ampm}`;
};

Time.prototype.getDate = function() {
    let hours = this.ampm == 'PM' ? this.hours + 12 : this.hours;
    let minutes = this.minutes;

    let date = new Date();
    date.setHours(hours, minutes);

    return date;
};

export { Time };