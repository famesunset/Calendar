import { EventRepository } from '../../models/mvc/event-repository.js';
import { ViewMode } from '../view-mode.js';
import { Modal } from '../pop-ups/modal.js';
import { FetchContent } from '../../models/mvc/fetch-content.js';
import { Key } from '../../models/share/key-bind.js';
import { Toast } from '../../models/toast.js';

export let DeleteEvent = {
  data: {
    cache: {
      eventId: 0
    },

    selectors: {
      s_loadDeleteEvent: '#load-delete-event',
      s_deleteEvent: '.delete-event',
      s_deleteEventBtn: '.delete-event-btn'
    },

    url: {
      u_windowLoad: '/EventView/DeleteEventPopUp'
    }
  },

  setUpListeners() {
    let s = this.data.selectors;

    $(s.s_deleteEvent).click(() => this.onDeleteEvent());
    Key.ecs(() => this.close());
  },

  onClose() {
    let _this = DeleteEvent;
    _this.close();    
  },  

  onDeleteEvent() {
    this.deleteEvent();
    this.close();
    Modal.close();    
  },

  open(id, pos) {    
    if (isNaN(id))
      return;

    let container = this.data.selectors.s_loadDeleteEvent;
    let url = this.data.url.u_windowLoad;

    FetchContent.get(url, content => {
      $(container).html(content);

      this.data.cache.eventId = id;          
      this.setUpListeners();   
      this.openAnimation(pos);
      Modal.open(this.onClose);
    });
  },

  close() {    
    this.closeAnimation();
    Key.unbind();
  },

  openAnimation(pos) {
    var s = this.data.selectors;
    var $wrapper = $(s.s_loadDeleteEvent);
    
    $wrapper.css('top', `${pos.y}px`);
    $wrapper.css('left', `${pos.x}px`);
    $wrapper.animate({
        opacity: 1
    }, 50);
  },

  closeAnimation() {
    var s = this.data.selectors;
    var $wrapper = $(s.s_loadDeleteEvent);
    var $content = $(s.s_deleteEvent);
    
    setTimeout(() => {
      $wrapper.animate({
        opacity: 0
      }, 100, () => $content.remove());
    }, 100);
  },

  deleteEvent() {
    Toast.display('Deleting...');
    new EventRepository().delete(this.data.cache.eventId,
    () => {
      let selector = ViewMode.getCachedEvent();
      ViewMode.deleteEvent(selector);
      Toast.display('Event deleted');
    });    
  }
};