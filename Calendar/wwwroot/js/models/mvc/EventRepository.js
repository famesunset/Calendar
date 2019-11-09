import { Repository } from './Repository.js';

class EventRepository extends Repository {
  async getList(date) {
    let list = [];

    let promise = super.getList('/Home/GetEventList', { date: date.toISOString() });    
    await promise.then(data => {
      list = data;
    });

    return list;
  }
}

export { EventRepository };