class Repository {
  get(id, url) {
    
  }

  async getList(url) {
    return new Promise((resolve, reject) => {
      $.ajax({
        url: url,
        type: 'GET',      
        success: data => resolve(data),
        error: () => reject('error')
      }); 
    });    
  }
  
  update(item, url) {}
  
  insert(item, url) {}
  
  delete(id, url) {}
}

export { Repository };