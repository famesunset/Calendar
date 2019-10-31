class Dropdown {
  constructor(trigger, options) {
    this.trigger = trigger;
    this.options = options;
  }

  runDropdown() {
    $(this.trigger).dropdown(this.options);
  }

  clickHandler(event) {
    var $option = $(event.target);
    var $trigger = $(this.trigger);

    // set the text of the selected option
    $trigger.text($option.text());
  }
}

export { Dropdown };