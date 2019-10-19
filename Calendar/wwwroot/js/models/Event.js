var Event = function(title, description, dateStart, dateFinish, timeStart, timeFinish, isAllDay, repeat, notify) {    
    this.data = {
        Title: title,
        Description: description,
        DateStart: dateStart,
        DateFinish: dateFinish,
        TimeStart: timeStart,
        timeFinish: timeFinish,
        IsAllDay: isAllDay,
        Repeat: repeat,
        Notify: notify
    };
};

Event.prototype.sendToMVC = function(action) {
    var eventJson = JSON.stringify(this.data);
    
    $.ajax({
        type: 'POST',
        url: action,
        data: eventJson,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data);
        }
    });
};

export { Event };

