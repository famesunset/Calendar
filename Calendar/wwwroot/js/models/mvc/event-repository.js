import { Repository } from './repository.js';

export class EventRepository extends Repository {
  async get(id) {
    let event = {};
    
    let promise = super.get({ id }, '/Event/GetEvent');    
    await promise.then(data => {
      event = data;
    });

    return event;
  }
  
  async getList(date, calendars) {
    let timeOffset = new Date().getTimezoneOffset();
    let list = [];

    let promise = super.getList({ date: date.toDateString(), calendars: calendars, timeOffset }, '/Event/GetEventList');    
    await promise.then(data => {
      list = data;
    });

    return list;
  }  

  async insert(event) {
    let id;
    
    let promise = super.insert(event, '/Event/CreateEvent');    
    await promise.then((data) => {
        id = data
    }).catch(err => {
        alert(err);
    });

    return id;
  }

  async generateLink(id) {
    let link = {};
    
    let promise = super.get({ id }, '/Event/GenerateEventLink');    
    await promise.then(data => {
      link = data;
    });

    return link;
  }

  update(item) {
    let promise = super.update(item, '/Event/EditEvent');    
    promise.then();
  }

  delete(id) {
    let promise = super.delete({ id }, '/Event/DeleteEvent');    
    promise.then();
  }
}