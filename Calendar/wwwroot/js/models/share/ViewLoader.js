class ViewLoader {
  static load(id, container, url, callback) {
    let el = `<div id="${id}"></div>`;
    $(container).append(el);
    
    $(`#${id}`).load(url, () => callback());
  }
}

export { ViewLoader };