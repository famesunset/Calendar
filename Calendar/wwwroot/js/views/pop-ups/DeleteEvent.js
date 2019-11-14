import { EventRepository } from '../../models/mvc/EventRepository.js';
import { ViewMode } from '../ViewMode.js';

let DeleteEvent = {
  data: {
    cache: {
      eventId: 0
    },

    selectors: {
      s_loadDeleteEvent: '#load-delete-event',
      s_deleteEvent: '.delete-event',
      s_deleteEventBtn: '.delete-event-btn',
      s_modal: '#main-modal',
    },

    css: {
      c_modal: 'main-modal'
    },

    url: {
      u_windowLoad: '/LoadView/DeleteEventPopUp'
    }
  },

  setUpListeners() {
    let s = this.data.selectors;

    $(s.s_modal).click(() => this.onClose());
    $(s.s_deleteEvent).click(() => this.onDeleteEvent());
  },

  onClose() {
    this.close();
  },  

  onDeleteEvent() {
    this.deleteEvent();
    this.close();
  },

  open(eventId, pos) {    
    let container = this.data.selectors.s_loadDeleteEvent;
    let url = this.data.url.u_windowLoad;

    $(container).load(url, () => {
      this.data.cache.eventId = eventId;
      this.openModal();      
      this.setUpListeners();   
      this.openAnimation(pos);
    });
  },

  close() {    
    this.closeAnimation();    
    this.closeModal();
  },

  openModal(container) {
    let s = this.data.selectors;
    let c = this.data.css;

    if ($(s.s_modal)[0] != undefined)
      return;

    let modal = `<div id="${c.c_modal}"></div>`;
    $('body').prepend(modal);
  },

  closeModal() {
    let modal = this.data.selectors.s_modal;
    $(modal).remove();
  },

  openAnimation(pos) {
    var s = this.data.selectors;
    var $wrapper = $(s.s_loadDeleteEvent);
    
    $wrapper.css('top', `${pos.y}px`);
    $wrapper.css('left', `${pos.x}px`);
    $wrapper.animate({
        opacity: 1
    }, 100);
  },

  closeAnimation() {
    var s = this.data.selectors;
    var $wrapper = $(s.s_loadDeleteEvent);
    var $content = $(s.s_deleteEvent);
    
    setTimeout(() => {
      $wrapper.animate({
        opacity: 0
      }, 100, () => $content.remove());
    }, 150);
  },

  deleteEvent() {
    new EventRepository().delete(this.data.cache.eventId);
    let selector = ViewMode.getCachedEvent();
    ViewMode.deleteEvent(selector);
  }
}

export { DeleteEvent };