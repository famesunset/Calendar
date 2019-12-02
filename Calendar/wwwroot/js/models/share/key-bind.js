export let Key = {
  code: {
    enter: 13,
    esc: 27
  },

  unbind() {
    $(document).unbind('keyup');
  },

  bind(keyCode, callback) {
    $(document).keyup(e => {      
      if (e.keyCode == keyCode) {
        this.unbind();
        callback();        
      }
    });
  },

  enter(callback) {
    let enter = this.code.enter;
    this.bind(enter, callback);
  },

  ecs(callback) {
    let ecs = this.code.esc;
    this.bind(ecs, callback);
  }  
}