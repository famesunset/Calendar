import { Repository } from "./repository.js";

export class CalendarRepository extends Repository {  
  async get(id) {
    let calendar = {};

    let promise = super.get({ id }, '/Calendar/GetCalendar');
    await promise.then(data => {
      calendar = data;
    });

    return calendar;
  }
  
  async getList() {
    let list = [];

    let promise = super.getList({ date: date.toISOString() }, '/Calendar/GetCalendarList');    
    await promise.then(data => {
      list = data;
    });

    return list;
  }

  async insert(name, colorId) {
    let calendarId = 0;
    
    let promise = super.get({ name, colorId }, '/Calendar/CreateCalendar');
    await promise.then(data => {
      calendarId = data;
    });

    return calendarId;
  }

  delete(id) {
    let promise = super.delete({ id }, '/Calendar/DeleteCalendar');    
    promise.then();
  }

}