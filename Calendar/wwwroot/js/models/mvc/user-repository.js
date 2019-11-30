import { Repository } from "./repository.js";

export class UserRepository extends Repository {  
  validEmail(email, callback) {    
    let url = '/User/ValidateEmail' + `?email=${email}`;
    super.get(url, callback);    
  }  
}