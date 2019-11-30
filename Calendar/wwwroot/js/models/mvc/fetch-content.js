export class FetchContent {
  /**
   * 
   * @param {string} url 
   * @param {function} callback 
   */
  static get(url, callback) {
    fetch(url)
    .then(resp => resp.text())
    .then(content => callback(content));
  }

  /**
   * 
   * @param {json} data should always be in json format
   * @param {string} url 
   * @param {function} callback 
   */
  static post(data, url, callback) {
    fetch(url, {
      method: 'POST',
      body: data,
      headers: {
        'Content-Type': 'application/json'
      }
    })
    .then(resp => resp.text())
    .then(content => callback(content));
  }
}