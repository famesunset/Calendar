var DateHelper = function(date) {
    this.date = date;
};

DateHelper.prototype.getDayOfWeek = function() {
    let dayNum = this.date.getDay();    
    let weekDays = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];

    return weekDays[dayNum];
};

DateHelper.prototype.getMonthOfYear = function() {
    let monthNum = this.date.getMonth();
    var yearMonths = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
                      "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

    return yearMonths[monthNum];   
};

DateHelper.prototype.getDayOfMonth = function() {
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
};

export { DateHelper };