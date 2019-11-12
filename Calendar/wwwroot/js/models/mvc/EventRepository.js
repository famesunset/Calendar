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

  insert(item) {
    let promise = super.insert(item, '/Home/CreateEvent');    
    promise.then();
  }

  update(item) {
    let promise = super.update(item, '/Home/UpdateEvent');    
    promise.then();
  }

  delete(id) {
    let promise = super.delete({ id }, '/Home/DeleteEvent');    
    promise.then();
  }
}

export { EventRepository };