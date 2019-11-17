export class Repository {
  get(id, url) {
    return new Promise((resolve, reject) => {
      $.ajax({
        url: url,
        type: 'GET',     
        data: id,
        dataType: 'json',
        contentType: "application/json; charset=utf-8", 
        success: data => resolve(data),
        error: () => reject('error')
      }); 
    });
  }

  async getList(_data, url) {
    return new Promise((resolve, reject) => {
      $.ajax({
        url: url,
        type: 'GET',     
        data: _data,
        dataType: 'json',
        contentType: "application/json; charset=utf-8", 
        success: data => resolve(data),
        error: () => reject('error')
      }); 
    });    
  }
  
  update(item, url) {
    let _data = JSON.stringify(item);

    return new Promise((resolve, reject) => {
      $.ajax({
        url: url,
        type: 'POST',     
        data: _data,
        dataType: 'json',
        contentType: "application/json; charset=utf-8", 
        success: data => resolve(data),
        error: () => reject('error')
      }); 
    }); 
  }
  
  insert(item, url) {
    let _data = JSON.stringify(item);

    return new Promise((resolve, reject) => {
      $.ajax({
        url: url,
        type: 'POST',     
        data: _data,
        dataType: 'json',
        contentType: "application/json; charset=utf-8", 
        success: data => resolve(data),
        error: () => reject('error')
      }); 
    }); 
  }
  
  delete(id, url) {
    return new Promise((resolve, reject) => {
      $.ajax({
        url: url,
        type: 'GET',     
        data: id,
        dataType: 'json',
        contentType: "application/json; charset=utf-8", 
        success: data => resolve(data),
        error: () => reject('error')
      }); 
    }); 
  }
}