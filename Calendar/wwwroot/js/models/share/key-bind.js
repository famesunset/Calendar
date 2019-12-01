export let Key = {
  code: {
    enter: 13,
    esc: 27
  },

  bind(keyCode, callback) {
    $(document).keyup(e => {      
      if (e.keyCode == keyCode) {
        $(document).unbind('keyup');
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