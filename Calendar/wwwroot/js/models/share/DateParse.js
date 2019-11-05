class DateParse {
  constructor(date) {
    this.date = date;
  }

  getDayOfWeek() {
    let dayNum = this.date.getDay();
    let weekDays = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];

    return weekDays[dayNum];
  }

  getMonthOfYear() {
    let monthNum = this.date.getMonth();
    var yearMonths = ["January", "February", "March", "April", "May", "June",
      "July", "August", "September", "October", "November", "December"];

    return yearMonths[monthNum];
  }

  getDayOfMonth() {
    let resultDay = "";
    let monthDay = `${this.date.getDate()}`;
    let lastNum = monthDay.charAt(monthDay.length - 1);
    
    if (+lastNum <= 3 &&
      (+monthDay < 10 || +monthDay > 20)) {
      resultDay = monthDay + ["st", "nd", "d"][+lastNum - 1];
    } else {
      resultDay = monthDay + "th";
    }

    return resultDay;
  }

  getTimeZone() {
    var offset = (this.date.getTimezoneOffset() / 60) * -1;
    var moduleOffset = Math.abs(offset);

    return `GMT ${offset > 0 ? '+' : '-'}${moduleOffset}`;
  }

  hoursOffset(offset) {
    let date = new Date(this.date);
    date.setHours(date.getHours() + offset);

    return date;
  }
}

export { DateParse };