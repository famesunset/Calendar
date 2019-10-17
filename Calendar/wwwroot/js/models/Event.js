var Event = function(title, description, dateStart, dateFinish, timeStart, timeFinish, isAllDay, repeat, notify) {
    this.title = title;
    this.description = description;
    this.dateStart = dateStart;
    this.dateFinish = dateFinish;
    this.timeStart = timeStart;
    this.timeFinish = timeFinish;
    this.isAllDay = isAllDay;
    this.repeat = repeat;
    this.rotify = notify;
};

Event.prototype.sendToMVC = function(action) {
    var eventJson = JSON.stringify({
        Title: this.title,
        Description: this.description,
        DateStart: this.dateStart,
        DateFinish: this.dateFinish,
        TimeStart: this.timeStart,
        timeFinish: this.timeFinish,
        IsAllDay: this.isAllDay,
        Repeat: this.repeat,
        Notify: this.notify
    });
    
    $.ajax({
        type: 'POST',
        url: 'Home/CreateEvent',
        data: eventJson,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data);
        }
    });
};

export { Event };

