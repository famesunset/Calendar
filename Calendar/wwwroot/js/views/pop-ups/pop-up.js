import { FetchContent } from "../../models/mvc/fetch-content.js";
import { Key } from "../../models/share/key-bind.js";

export let PopUp = {
  data: {
    response: {
      CANCEL: 0,
      SUBMIT: 1
    },

    selectors: {
      s_popUp: '#pop-up-container',
      s_content: '#pop-up-content',
      s_cancel: '#pop-up-cancel',
      s_submit: '#pop-up-submit'
    },

    url: {
      u_popUpLoad: '/PopUp/PopUpWindow'
    },

    cache: {
      callback: null
    }
  },

  setUpListeners() {
    let s = this.data.selectors;
    
    $(s.s_submit).click(() => this.onSubmit());
    $(s.s_cancel).click(() => this.onCancel());
    Key.enter(() => this.onSubmit());
    Key.ecs(() => this.onCancel());
  },

  open(content, callback) {
    if (!this.canOpen()) 
      return;
    
    let url = this.data.url.u_popUpLoad;
    let container = $('body');    

    FetchContent.get(url, window => {
      container.prepend(window);

      let contentContainer = $(this.data.selectors.s_content);
      contentContainer.append(content);      

      this.cacheCallback(callback);
      this.setUpListeners();      
    });
  },

  close() {
    let popUp = this.data.selectors.s_popUp;
    $(popUp).remove();
  },

  onSubmit() {
    let callback = this.data.cache.callback;
    let response = this.data.response;

    callback(response.SUBMIT);
    this.close();
  },

  onCancel() {
    let callback = this.data.cache.callback;
    let response = this.data.response;

    callback(response.CANCEL);
    this.close();
    Key.unbind();
  },

  cacheCallback(callback) {
    this.data.cache.callback = callback;
  },

  canOpen() {
    let s = this.data.selectors;
    return $(s.s_popUp)[0] == null;
  }
}