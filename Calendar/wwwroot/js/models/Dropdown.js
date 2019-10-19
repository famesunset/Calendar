var Dropdown = function(trigger, options) {
    this.trigger = trigger;
    this.options = options;
}

Dropdown.prototype.runDropDown = function() {
    $(this.trigger).dropdown(this.options);
};

Dropdown.prototype.clickHandler = function(event) {
    var $option = $(event.target);   
    var $trigger = $(this.trigger);
    
    // set the text of the selected option
    $trigger.text($option.text());
};

export { Dropdown };