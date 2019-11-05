import { Repository } from './Repository.js';

class EventRepository extends Repository {
  async getList() {
    let list = [];

    let promise = super.getList('/Home/GetEventList');    
    await promise.then(data => {
      list = data;
    });

    return list;
  }
}

export { EventRepository };