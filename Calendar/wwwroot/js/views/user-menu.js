export let UserMenu = {
  data: {
    selectors: {
      s_logout: '.account-logout'
    }
  },

  run() {
    this.setUpListeners();
  },

  setUpListeners() {
    let s = this.data.selectors;

    $(s.s_logout).click(this.logout);
  },

  logout(e) {
    e.preventDefault();    
    let browserId = window.localStorage.getItem('sentFirebaseMessagingToken');    
    let url = this.href + `&borwserId=${browserId}`;    

    window.location.replace(url);
  }
}