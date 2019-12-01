import { Repository } from './repository.js';

export class EventRepository extends Repository {
  get(id, callback) {
    let timeOffset = new Date().getTimezoneOffset();
    let url = `/Event/GetEvent?id=${id}&timeOffset=${timeOffset}`;    
    
    super.get(url, callback);        
  }
  
  getList(date, calendars, callback) {
    let data = {
      date: date.toDateString(),
      timeOffset: new Date().getTimezoneOffset(),
      calendars
    };    

    let url = '/Event/GetEventList';
    super.post(data, url, callback);        
  }  

  insert(event, callback) {
    let url = '/Event/CreateEvent';
    super.post(event, url, callback);
  }

  generateLink(id, callback) {
    let timeOffset = new Date().getTimezoneOffset();
    let url = `/Event/GenerateEventLink?id=${id}&timeOffset=${timeOffset}`;    
    
    super.get(url, callback);    
  }

  update(item, callback) {
    let url = '/Event/EditEvent';
    super.post(item, url, callback);        
  }

  delete(id, callback) {
    let url = `/Event/DeleteEvent?id=${id}`;
    super.get(url, callback);        
  }
}