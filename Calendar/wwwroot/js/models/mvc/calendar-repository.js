import { Repository } from "./repository.js";

export class CalendarRepository extends Repository {  
  get(id, callback) {
    let url = '/Calendar/GetCalendar' + `?id=${id}`;    
    super.get(url, callback);
  }
  
  getList(callback) {    
    let url = '/Calendar/GetCalendarList';
    super.get(url, callback);        
  }

  insert(name, colorId, callback) {    
    let url = '/Calendar/CreateCalendar';
    super.post({ name, colorId }, url, callback);
  }

  delete(id, callback) {
    let url = '/Calendar/DeleteCalendar' + `?id=${id}`;
    super.get(url, callback);        
  }

  subscribe(email, calendarId, callback) {
    let url = '/Calendar/SubscribeCalendar';
    super.post({ email, calendarId }, url, callback);    
  }

  unsubscribe(id, callback) {
    let url = '/Calendar/UnsubscribeCalendar' + `?id=${id}`;
    super.get(url, callback);        
  }
}