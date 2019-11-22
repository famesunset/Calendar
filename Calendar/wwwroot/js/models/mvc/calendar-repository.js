import { Repository } from "./repository.js";

export class CalendarRepository extends Repository {
  delete(id) {
    let promise = super.delete({ id }, '/Calendar/DeleteCalendar');    
    promise.then();
  }

  async getList() {
    let list = [];

    let promise = super.getList({ date: date.toISOString() }, '/Calendar/GetCalendarList');    
    await promise.then(data => {
      list = data;
    });

    return list;
  }
}