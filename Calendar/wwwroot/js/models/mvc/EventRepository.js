import { Repository } from './Repository.js';

class EventRepository extends Repository {
  async getList(date) {
    let list = [];

    let promise = super.getList({ date: date.toISOString() }, '/Home/GetEventList');    
    await promise.then(data => {
      list = data;
    });

    return list;
  }

  delete(id) {
    let promise = super.delete({ id }, '/Home/DeleteEvent');    
    promise.then();
  }
}

export { EventRepository };