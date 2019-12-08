export let Toast = {
  display(text) {
    this.dismissAll();
    M.toast({ html: text });    
  },  
  
  dismissAll() {
    $('.toast').remove();
  }
}
