import { EventRepository } from '../models/mvc/event-repository.js';

export class Event {
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
  
  sendToMVC() {
    new EventRepository().insert(this.data);
  }
}
