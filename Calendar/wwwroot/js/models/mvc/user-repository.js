import { Repository } from "./repository.js";

export class UserRepository extends Repository {  
  async validEmail(email) {
    let result;

    let promise = super.get({ email }, '/User/ValidateEmail');
    await promise.then(data => {
      result = data;
    });

    // boolean
    return result;
  }  
}