class Event {
  constructor(title, description, start, finish, color, isAllDay, notify) {
    this.data = {
      Title: title,
      Description: description,
      Start: start,
      Finish: finish,
      Color: color,
      IsAllDay: isAllDay,
      Notify: notify
    };
  }
  
  sendToMVC(action) {
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
  }
}

export { Event };

