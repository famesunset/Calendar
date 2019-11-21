export let Modal = {
  data: {
    selectors: {
      s_modal: '#main-modal',
    },

    css: {
      c_modal: 'main-modal'
    }
  },

  setUpListeners(callback) {
    let modal = this.data.selectors.s_modal;

    $(modal).click(() => callback());
  },

  open(callback) {
    let s = this.data.selectors;
    let c = this.data.css;

    if ($(s.s_modal)[0] != undefined)
      return;

    let modal = `<div id="${c.c_modal}"></div>`;
    $('body').prepend(modal);

    this.setUpListeners(callback);
  },

  close() {
    let modal = this.data.selectors.s_modal;
    $(modal).remove();
  }
};