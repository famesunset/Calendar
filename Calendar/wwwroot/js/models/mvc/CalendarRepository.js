import { Repository } from "./Repository.js";

export class CalendarRepository extends Repository {
  delete(id) {
    let promise = super.delete({ id }, '/Home/DeleteCalendar');    
    promise.then();
  }
}