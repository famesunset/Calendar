import { Modal } from './modal.js';
import { EventForm } from '../event-form.js';
import { EventRepository } from '../../models/mvc/event-repository.js';
import { ViewMode } from '../view-mode.js';

export let EventInfo = {
  data: {
    selectors: {
      s_info: '.event-info',
      s_eventId: '.event-info input[name="eventId"]',
      s_shareTrigger: '.event-info .share-event',
      s_editTrigger: '.event-info .edit-event',
      s_deleteTrigger: '.event-info .delete-event',
      s_closeTrigger: '.event-info .close-form'
    },

    url: {
      u_loadView: '/EventView/EventInfo',
      u_generateLink: '/Event/GenerateLink'
    }
  },
  
  setUpListeners() {
    let s = this.data.selectors;

    $(s.s_shareTrigger).click(() => this.onShare());
    $(s.s_editTrigger).click(() => this.onEdit());
    $(s.s_deleteTrigger).click(() => this.onDelete());
    $(s.s_closeTrigger).click(() => this.close());
    $('.tooltipped').tooltip({
      inDuration: 0, 
      outDuration: 0,  
      enterDelay: 300,
      margin: 0,
      transitionMovement: 5
    });
  },

  open(id) {
    if (isNaN(id))
      return;

    let container = 'body';
    let url = this.data.url.u_loadView + `?id=${id}`;

    $.get(url, (content) => {
      $(container).prepend(content);
      Modal.open(this.close);
      this.setUpListeners();
    });
  },

  close() {
    let _this = EventInfo;
    let info = _this.data.selectors.s_info;

    $(info).remove();
    Modal.close();
  },

  async onShare() {
    let s = this.data.selectors;
    let id = $(s.s_eventId).val();

    let link = await new EventRepository().generateLink(id);

    console.log(link);
  },

  onEdit() {
    let s = this.data.selectors;
    let id = $(s.s_eventId).val();

    this.close();
    EventForm.openEdit(id);
  },

  onDelete() {
    let s = this.data.selectors;
    let id = $(s.s_eventId).val();
    let selector = ViewMode.getCachedEvent();

    this.close();
    ViewMode.deleteEvent(selector);
    new EventRepository().delete(id);
    M.toast({html: 'Event deleted'});
  }
}