export let Toast = {
  display(text) {
    this.dismissAll();
    M.toast({ html: text });
    this.align();
  },  
  
  dismissAll() {
    $('.toast').remove();
  },

  align() {
    let $toast = $('#toast-container');
    let width = $toast.width();
    let left = ($(document).width() / 2) - (width / 2);
    
    $toast.css('left', `${left}px`);
  }
}