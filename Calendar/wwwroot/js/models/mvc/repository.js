export class Repository {
  get(url, callback) {
    fetch(url)
    .then(resp => resp.text())
    .then(json => JSON.parse(json))
    .then(data => callback(data));
  }

  post(data, url, callback) {
    let _data = JSON.stringify(data);

    fetch(url, {
      method: 'POST',
      body: _data,
      headers: {
        'Content-Type': 'application/json'
      }
    })
    .then(resp => resp.text())
    .then(json => JSON.parse(json))
    .then(data => callback(data));
  }  
}