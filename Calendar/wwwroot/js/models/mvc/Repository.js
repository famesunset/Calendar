class Repository {
  get(id, url) {
    
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
  
  update(item, url) {}
  
  insert(item, url) {}
  
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

export { Repository };