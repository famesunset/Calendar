class TimeParse {
  constructor(date) {
    this.hours = date.getHours();
    this.minutes = date.getMinutes();
    this.ampm = `${date.getHours() < 12 ? 'AM' : 'PM'}`;
  }

  getTime() {
    if (this.hours > 12)
      this.hours -= 12;

    let showHours = this.hours < 10 ? `0${this.hours}` : this.hours;
    let showMinutes = this.minutes < 10 ? `0${this.minutes}` : this.minutes;

    return `${showHours}:${showMinutes} ${this.ampm}`;
  }

  getDate() {
    let hours = this.ampm == 'PM' ? this.hours + 12 : this.hours;
    let minutes = this.minutes;

    let date = new Date();
    date.setHours(hours, minutes);

    return date;
  }

  static parse(time) {
    let hours = /^\d+/.exec(time)[0].replace(/^0/, '');
    let minutes = /:(\d+)/.exec(time)[1].replace(/^0/, '');
    let ampm = /AM|PM/.exec(time)[0];

    if (ampm === 'PM' && hours < 12) 
      hours = +hours + 12;

    if (ampm === 'AM' && hours == 12) 
      hours = 0;

    let date = new Date();
    date.setHours(hours, minutes);

    return date;
  }
}

export { TimeParse };