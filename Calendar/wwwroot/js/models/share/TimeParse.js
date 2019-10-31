class TimeParse {
  constructor(date) {
    this.hours = date.getHours();
    this.minutes = date.getMinutes();
    this.ampm = `${date.getHours() < 12 ? 'AM' : 'PM'}`;
  }

  getTime() {
    if (this.hours > 12)
      this.hours -= 12;

    let showMinutes = this.minutes < 10 ? `0${this.minutes}` : this.minutes;

    return `${this.hours}:${showMinutes} ${this.ampm}`;
  }

  getDate() {
    let hours = this.ampm == 'PM' ? this.hours + 12 : this.hours;
    let minutes = this.minutes;

    let date = new Date();
    date.setHours(hours, minutes);

    return date;
  }
}

export { TimeParse };